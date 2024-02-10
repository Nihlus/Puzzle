//
//  SPDX-FileName: SampleImages.cs
//  SPDX-FileCopyrightText: Copyright (c) Jarl Gullberg
//  SPDX-License-Identifier: AGPL-3.0-or-later
//

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Puzzle.Tests.Data
{
    /// <summary>
    /// Contains bundled sample images.
    /// </summary>
    public static class SampleImages
    {
        /// <summary>
        /// Gets a set of slightly edited Mona Lisa images.
        /// </summary>
        public static IEnumerable<object[]> SlightlyEditedImages
        {
            get
            {
                yield return new object[] { Lisa9.Value };
                yield return new object[] { ChromaticMona.Value };
            }
        }

        /// <summary>
        /// Gets a set of significantly edited Mona Lisa images.
        /// </summary>
        public static IEnumerable<object[]> SignificantlyEditedImages
        {
            get
            {
                yield return new object[] { BritneySpearsMona.Value };
                yield return new object[] { GangstaMona.Value };
            }
        }

        /// <summary>
        /// Gets a set of recoloured Mona Lisa images.
        /// </summary>
        public static IEnumerable<object[]> RecolouredImages
        {
            get
            {
                yield return new object[] { BlueMona.Value };
                yield return new object[] { CyanMona.Value };
                yield return new object[] { GreenMona.Value };
                yield return new object[] { PinkMona.Value };
                yield return new object[] { RedMona.Value };
                yield return new object[] { YellowMona.Value };
            }
        }

        /// <summary>
        /// Gets a set of stylized Mona Lisa image copies.
        /// </summary>
        public static IEnumerable<object[]> StylizedCopies
        {
            get
            {
                yield return new object[] { HorrorMona.Value };
                yield return new object[] { MonaLizzle.Value };
                yield return new object[] { PointilistMona.Value };
            }
        }

        /// <summary>
        /// Gets a set of real-life Mona Lisa photographs, containing real people.
        /// </summary>
        public static IEnumerable<object[]> Photos
        {
            get
            {
                yield return new object[] { PhotoMona.Value };
            }
        }

        /// <summary>
        /// Gets a set of images that do not depict the Mona Lisa.
        /// </summary>
        public static IEnumerable<object[]> DifferentImages
        {
            get
            {
                yield return new object[] { SaintMatthew.Value };
                yield return new object[] { SickBacchus.Value };
                yield return new object[] { JudithBeheading.Value };
            }
        }

        /// <summary>
        /// Gets a set of small single-colour images.
        /// </summary>
        public static IEnumerable<object[]> SmallImages
        {
            get
            {
                yield return new object[] { Uniform1.Value };
                yield return new object[] { Uniform2.Value };
                yield return new object[] { Uniform4.Value };
                yield return new object[] { Uniform8.Value };
                yield return new object[] { Uniform16.Value };
            }
        }

        /// <summary>
        /// Gets the canonical Mona Lisa image.
        /// </summary>
        public static Lazy<Image<L8>> MonaLisa { get; } = GetLazyLoaderForImage("original-mona.jpg");

        /// <summary>
        /// Gets an edited Mona Lisa image.
        /// </summary>
        public static Lazy<Image<L8>> GangstaMona { get; } = GetLazyLoaderForImage("gangsta-mona.jpg");

        /// <summary>
        /// Gets an edited Mona Lisa image.
        /// </summary>
        public static Lazy<Image<L8>> BritneySpearsMona { get; } = GetLazyLoaderForImage("lisa-spears.jpg");

        /// <summary>
        /// Gets an edited Mona Lisa image.
        /// </summary>
        public static Lazy<Image<L8>> Lisa9 { get; } = GetLazyLoaderForImage("lisa9.jpg");

        /// <summary>
        /// Gets a stylized Mona Lisa image.
        /// </summary>
        public static Lazy<Image<L8>> HorrorMona { get; } = GetLazyLoaderForImage("mona-horror.jpg");

        /// <summary>
        /// Gets a stylized Mona Lisa image.
        /// </summary>
        public static Lazy<Image<L8>> MonaLizzle { get; } = GetLazyLoaderForImage("mona-lizzle.jpg");

        /// <summary>
        /// Gets a stylized Mona Lisa image.
        /// </summary>
        public static Lazy<Image<L8>> PointilistMona { get; } = GetLazyLoaderForImage("pointilist-mona.jpg");

        /// <summary>
        /// Gets an edited Mona Lisa image.
        /// </summary>
        public static Lazy<Image<L8>> ChromaticMona { get; } = GetLazyLoaderForImage("truth-lisa-chromatic.jpg");

        /// <summary>
        /// Gets a real-life photograph Mona Lisa image.
        /// </summary>
        public static Lazy<Image<L8>> PhotoMona { get; } = GetLazyLoaderForImage("truth-lisa-photo.jpg");

        /// <summary>
        /// Gets a recoloured Mona Lisa image.
        /// </summary>
        public static Lazy<Image<L8>> BlueMona { get; } = GetLazyLoaderForImage("recoloured-mona-blue.jpg");

        /// <summary>
        /// Gets a recoloured Mona Lisa image.
        /// </summary>
        public static Lazy<Image<L8>> CyanMona { get; } = GetLazyLoaderForImage("recoloured-mona-cyan.jpg");

        /// <summary>
        /// Gets a recoloured Mona Lisa image.
        /// </summary>
        public static Lazy<Image<L8>> GreenMona { get; } = GetLazyLoaderForImage("recoloured-mona-green.jpg");

        /// <summary>
        /// Gets a recoloured Mona Lisa image.
        /// </summary>
        public static Lazy<Image<L8>> PinkMona { get; } = GetLazyLoaderForImage("recoloured-mona-pink.jpg");

        /// <summary>
        /// Gets a recoloured Mona Lisa image.
        /// </summary>
        public static Lazy<Image<L8>> RedMona { get; } = GetLazyLoaderForImage("recoloured-mona-red.jpg");

        /// <summary>
        /// Gets a recoloured Mona Lisa image.
        /// </summary>
        public static Lazy<Image<L8>> YellowMona { get; } = GetLazyLoaderForImage("recoloured-mona-yellow.jpg");

        /// <summary>
        /// Gets an original Caravaggio painting.
        /// </summary>
        public static Lazy<Image<L8>> SaintMatthew { get; } = GetLazyLoaderForImage("saint-matthew.jpg");

        /// <summary>
        /// Gets an original Caravaggio painting.
        /// </summary>
        public static Lazy<Image<L8>> JudithBeheading { get; } = GetLazyLoaderForImage("judith-beheading.jpg");

        /// <summary>
        /// Gets an original Caravaggio painting.
        /// </summary>
        public static Lazy<Image<L8>> SickBacchus { get; } = GetLazyLoaderForImage("sick-bacchus.jpg");

        /// <summary>
        /// Gets a single-colour 1x1 image.
        /// </summary>
        public static Lazy<Image<L8>> Uniform1 { get; } = GetLazyLoaderForImage("1x1.png");

        /// <summary>
        /// Gets a single-colour 2x2 image.
        /// </summary>
        public static Lazy<Image<L8>> Uniform2 { get; } = GetLazyLoaderForImage("2x2.png");

        /// <summary>
        /// Gets a single-colour 4x4 image.
        /// </summary>
        public static Lazy<Image<L8>> Uniform4 { get; } = GetLazyLoaderForImage("4x4.png");

        /// <summary>
        /// Gets a single-colour 8x8 image.
        /// </summary>
        public static Lazy<Image<L8>> Uniform8 { get; } = GetLazyLoaderForImage("8x8.png");

        /// <summary>
        /// Gets a single-colour 16x16 image.
        /// </summary>
        public static Lazy<Image<L8>> Uniform16 { get; } = GetLazyLoaderForImage("16x16.png");

        /// <summary>
        /// Gets a single-colour 8k image.
        /// </summary>
        public static Lazy<Image<L8>> Uniform8192 { get; } = GetLazyLoaderForImage("8192x8192.png");

        private static Lazy<Image<L8>> GetLazyLoaderForImage(string image)
        {
            return new Lazy<Image<L8>>
            (
                () =>
                {
                    using var resourceStream = Assembly
                        .GetExecutingAssembly()
                        .GetManifestResourceStream($"Puzzle.Tests.Images.{image}");

                    return Image.Load<L8>(resourceStream);
                },
                LazyThreadSafetyMode.ExecutionAndPublication
            );
        }
    }
}
