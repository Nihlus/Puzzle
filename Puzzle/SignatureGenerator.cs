//
//  SPDX-FileName: SignatureGenerator.cs
//  SPDX-FileCopyrightText: Copyright (c) Jarl Gullberg
//  SPDX-License-Identifier: AGPL-3.0-or-later
//

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Puzzle.Extensions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Puzzle;

/// <summary>
/// Represents an image signature generator.
/// </summary>
[PublicAPI]
public class SignatureGenerator
{
    /// <summary>
    /// Gets the grid size. This value indicates the grid size that the signature is composed from; that is,
    /// a value of 9 (the default) would decompose the image into a 9x9 grid of sample points (10x10 blocks), and
    /// then generate the signature from that grid.
    /// </summary>
    [PublicAPI]
    public uint GridSize { get; }

    /// <summary>
    /// Gets the noise cutoff level. This value indicates the distance between two luminosity values within which
    /// two luminosities can be considered equivalent.
    /// </summary>
    [PublicAPI]
    public double NoiseCutoff { get; }

    /// <summary>
    /// Gets the ratio for the sample point size. Typically, this is a value of 2, and serves as a multiplier
    /// against the number of squares the image is subdivided into.
    /// </summary>
    [PublicAPI]
    public double SampleSizeRatio { get; }

    /// <summary>
    /// Gets a value indicating whether the image will be automatically cropped when a signature is
    /// generated.
    /// </summary>
    [PublicAPI]
    public bool EnableAutocrop { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SignatureGenerator"/> class.
    /// </summary>
    /// <param name="gridSize">The number of grid lambdas to use.</param>
    /// <param name="noiseCutoff">The noise cutoff.</param>
    /// <param name="sampleSizeRatio">The P ratio.</param>
    /// <param name="enableAutocrop">Whether to enable autocrop or not.</param>
    [PublicAPI]
    public SignatureGenerator
    (
        uint gridSize = 9,
        double noiseCutoff = 2.0,
        double sampleSizeRatio = 2.0,
        bool enableAutocrop = true
    )
    {
        this.GridSize = gridSize;
        this.NoiseCutoff = noiseCutoff;
        this.SampleSizeRatio = sampleSizeRatio;
        this.EnableAutocrop = enableAutocrop;
    }

    /// <summary>
    /// Generates a signature from the given image. This overload copies the image once.
    /// </summary>
    /// <param name="image">The image to generate the signature from.</param>
    /// <returns>The signature.</returns>
    [PublicAPI]
    [Pure]
    public ReadOnlySpan<LuminosityLevel> GenerateSignature(Image image) =>
        GenerateSignature(image.CloneAs<L8>());

    /// <summary>
    /// Generates a signature from the given image.
    /// </summary>
    /// <param name="image">The image to generate the signature from.</param>
    /// <returns>The signature.</returns>
    [PublicAPI]
    [Pure]
    public LuminosityLevel[] GenerateSignature(Image<L8> image)
    {
        // Step 1: Generate a vector of double values representing the signature
        if (this.EnableAutocrop)
        {
            // Step 1.1: Crop the view to the relevant content
            image = AutocropImage(image);
        }

        // Step 1.2: Compute the average levels of points in the structure
        var sampledSquareAverages = ComputeAverageSampleLuminosities(image);

        var luminosityDifferences = ComputeNeighbourDifferences(sampledSquareAverages);

        // Step 2: Generate a vector of values representing the signature from the vector of double values
        return ComputeRelativeLuminosityLevels(luminosityDifferences);
    }

    /// <summary>
    /// Automatically crops the input image to the relevant content (removing uniform borders, for example).
    /// </summary>
    /// <param name="image">The image to crop.</param>
    /// <returns>The cropped image.</returns>
    [Pure]
    private Image<L8> AutocropImage(Image<L8> image)
    {
        image.Mutate(o => o.EntropyCrop());

        return image;
    }

    /// <summary>
    /// Computes a set of average values from a superimposed grid over the image, condensing it into a set of cells
    /// with level values.
    /// </summary>
    /// <param name="image">The image to compute the points of.</param>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the backing buffer for the image is not contiguous.
    /// </exception>
    /// <returns>The computed points.</returns>
    [Pure]
    private double[] ComputeAverageSampleLuminosities(Image<L8> image)
    {
        var squareSize = (int)Math.Max
        (
            2.0,
            Math.Round(Math.Min(image.Width, image.Height) / ((this.GridSize + 1) * this.SampleSizeRatio))
        );

        var squareCenters = ComputeSquareCenters(image).ToList();

        if (!image.TryGetSinglePixelSpan(out var pixels))
        {
            throw new InvalidOperationException("The backing buffer for the image was not contiguous.");
        }

        var sampleLuminosities = new double[squareCenters.Count];
        for (var i = 0; i < squareCenters.Count; i++)
        {
            var squareCenter = squareCenters[i];
            sampleLuminosities[i] = ComputeSquareAverage
            (
                pixels,
                image.Width,
                image.Height,
                squareCenter,
                squareSize
            );
        }

        return sampleLuminosities;
    }

    /// <summary>
    /// Computes the center coordinates of a set of grid squares, superimposed on the image.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <returns>The centers.</returns>
    [Pure]
    private IEnumerable<Point> ComputeSquareCenters(Image<L8> image)
    {
        var xOffset = image.Width / (double)(this.GridSize + 1);
        var yOffset = image.Height / (double)(this.GridSize + 1);

        for (var x = 0; x < this.GridSize; ++x)
        {
            for (var y = 0; y < this.GridSize; ++y)
            {
                yield return new Point
                {
                    X = (int)Math.Round(xOffset * (x + 1)),
                    Y = (int)Math.Round(yOffset * (y + 1))
                };
            }
        }
    }

    /// <summary>
    /// Computes the average gray level of a square in the image, centered at the given point, with the given size.
    /// </summary>
    /// <param name="pixels">The image to sample.</param>
    /// <param name="imageWidth">The width of the image.</param>
    /// <param name="imageHeight">The height of the image.</param>
    /// <param name="squareCenter">The center of the square.</param>
    /// <param name="squareSize">The size of the square.</param>
    /// <returns>The average level of the square.</returns>
    [Pure]
    private double ComputeSquareAverage
    (
        ReadOnlySpan<L8> pixels,
        int imageWidth,
        int imageHeight,
        Point squareCenter,
        int squareSize
    )
    {
        var (squareX, squareY) = squareCenter;
        var squareCorner = new Point
        {
            X = (int)Math.Round(squareX - (squareSize / 2.0)),
            Y = (int)Math.Round(squareY - (squareSize / 2.0))
        };

        var count = 0;
        var sum = 0.0;
        for (var y = squareCorner.Y; y < squareCorner.Y + squareSize; ++y)
        {
            if (y > imageHeight || y < 0)
            {
                continue;
            }

            for (var x = squareCorner.X; x < squareCorner.X + squareSize; ++x)
            {
                if (x > imageWidth || x < 0)
                {
                    continue;
                }

                sum += Sample3x3Point(pixels, imageWidth, imageHeight, new Point(x, y));
                ++count;
            }
        }

        return sum / count;
    }

    /// <summary>
    /// Samples the average luminosity of a 3x3 square, centered at the given coordinate. If any of the points of
    /// the square fall outside the image, no value is returned for that point.
    /// </summary>
    /// <param name="pixels">The image to sample.</param>
    /// <param name="imageWidth">The width of the image.</param>
    /// <param name="imageHeight">The height of the image.</param>
    /// <param name="point">The center of the point to sample.</param>
    /// <returns>The sampled values.</returns>
    [Pure]
    private double Sample3x3Point(ReadOnlySpan<L8> pixels, int imageWidth, int imageHeight, Point point)
    {
        var sum = 0.0;

        for (var yOffset = 0; yOffset < 3; ++yOffset)
        {
            var (pointX, pointY) = point;
            var y = (pointY - 1) + yOffset;

            if (y > imageHeight - 1 || y < 0)
            {
                continue;
            }

            for (var xOffset = 0; xOffset < 3; ++xOffset)
            {
                var x = (pointX - 1) + xOffset;

                if (x > imageWidth - 1 || x < 0)
                {
                    continue;
                }

                var spandex = x + (y * imageWidth);
                sum += pixels[spandex].PackedValue;
            }
        }

        return sum / 9;
    }

    /// <summary>
    /// Converts a set of baseline double values to a complete image signature.
    /// </summary>
    /// <param name="neighbourDifferences">The baseline values.</param>
    /// <returns>The image signature.</returns>
    [Pure]
    private LuminosityLevel[] ComputeRelativeLuminosityLevels
    (
        ReadOnlySpan<double> neighbourDifferences
    )
    {
        var darks = new List<double>(neighbourDifferences.Length);
        var lights = new List<double>(neighbourDifferences.Length);

        foreach (var difference in neighbourDifferences)
        {
            if (difference >= -this.NoiseCutoff && difference <= this.NoiseCutoff)
            {
                // This difference is considered a samey value.
                continue;
            }

            if (difference < this.NoiseCutoff)
            {
                darks.Add(difference);
                continue;
            }

            if (difference > this.NoiseCutoff)
            {
                lights.Add(difference);
            }
        }

        var muchDarkerCutoff = darks.Count > 0 ? darks.Median() : -this.NoiseCutoff;
        var muchLighterCutoff = lights.Count > 0 ? lights.Median() : this.NoiseCutoff;

        var luminosityLevels = new LuminosityLevel[neighbourDifferences.Length];
        for (var i = 0; i < neighbourDifferences.Length; i++)
        {
            var difference = neighbourDifferences[i];
            if (difference >= -this.NoiseCutoff && difference <= this.NoiseCutoff)
            {
                luminosityLevels[i] = LuminosityLevel.Same;
                continue;
            }

            if (difference < 0.0)
            {
                luminosityLevels[i] = difference < muchDarkerCutoff
                    ? LuminosityLevel.MuchDarker
                    : LuminosityLevel.Darker;

                continue;
            }

            luminosityLevels[i] = difference > muchLighterCutoff
                ? LuminosityLevel.MuchLighter
                : LuminosityLevel.Lighter;
        }

        return luminosityLevels;
    }

    /// <summary>
    /// Computes the absolute value differences between sampled neighbours. If no neighbour exists, the value
    /// returned is zero.
    /// </summary>
    /// <param name="luminosityAverages">The sampled neighbours.</param>
    /// <returns>The value differences.</returns>
    [Pure]
    private double[] ComputeNeighbourDifferences(ReadOnlySpan<double> luminosityAverages)
    {
        var neighbourCoordinateMap = new[]
        {
            new Point { X = -1, Y = -1 },
            new Point { X = 0, Y = -1, },
            new Point { X = 1, Y = -1 },
            new Point { X = 0, Y = -1 },
            new Point { X = 0, Y = 1 },
            new Point { X = -1, Y = 1 },
            new Point { X = 0, Y = 1 },
            new Point { X = 1, Y = 1 }
        };

        var neighbourDifferences = new double[this.GridSize * this.GridSize * 8];
        var spandex = 0;

        for (var x = 0; x < this.GridSize; ++x)
        {
            for (var y = 0; y < this.GridSize; ++y)
            {
                var index = x + (this.GridSize * y);

                var baseLuminosity = luminosityAverages[(int)index];

                for (var i = 0; i < 8; ++i)
                {
                    var (tileX, tileY) = neighbourCoordinateMap[i];
                    var (neighbourX, neighbourY) = new Point
                    {
                        X = x + tileX,
                        Y = y + tileY,
                    };

                    var neighbourIndex = neighbourX + (this.GridSize * neighbourY);
                    if (neighbourIndex < 0 || neighbourIndex >= luminosityAverages.Length)
                    {
                        neighbourDifferences[spandex] = 0.0;
                    }
                    else
                    {
                        neighbourDifferences[spandex] = baseLuminosity - luminosityAverages[(int)neighbourIndex];
                    }

                    ++spandex;
                }
            }
        }

        return neighbourDifferences;
    }
}
