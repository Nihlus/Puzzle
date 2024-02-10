//
//  SPDX-FileName: CompareTo.cs
//  SPDX-FileCopyrightText: Copyright (c) Jarl Gullberg
//  SPDX-License-Identifier: AGPL-3.0-or-later
//

using Puzzle.Tests.Data;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Xunit;

#pragma warning disable CS1591, SA1600

namespace Puzzle.Tests.Tests.SignatureComparisonTests
{
    public class CompareTo
    {
        private readonly SignatureGenerator _generator;

        public CompareTo()
        {
            _generator = new SignatureGenerator();
        }

        [Fact]
        public void IdenticalImagesCompareAsIdentical()
        {
            var firstSignature = _generator.GenerateSignature(SampleImages.MonaLisa.Value);
            var secondSignature = _generator.GenerateSignature(SampleImages.MonaLisa.Value);

            var result = firstSignature.CompareTo(secondSignature);

            Assert.Equal(SignatureSimilarity.Identical, result);
        }

        [Fact]
        public void DownscaledImagesCompareAsIdenticalOrSame()
        {
            var firstSignature = _generator.GenerateSignature(SampleImages.MonaLisa.Value);
            var secondSignature = _generator.GenerateSignature
            (
                SampleImages.MonaLisa.Value.Clone
                (
                    o => o.Resize
                    (
                        SampleImages.MonaLisa.Value.Width / 2,
                        SampleImages.MonaLisa.Value.Height / 2
                    )
                )
            );

            var result = firstSignature.CompareTo(secondSignature);

            Assert.True(result == SignatureSimilarity.Identical || result == SignatureSimilarity.Same);
        }

        [Fact]
        public void UpscaledImagesCompareAsIdenticalOrSame()
        {
            var firstSignature = _generator.GenerateSignature(SampleImages.MonaLisa.Value);
            var secondSignature = _generator.GenerateSignature
            (
                SampleImages.MonaLisa.Value.Clone
                (
                    o => o.Resize
                    (
                        SampleImages.MonaLisa.Value.Width * 2,
                        SampleImages.MonaLisa.Value.Height * 2
                    )
                )
            );

            var result = firstSignature.CompareTo(secondSignature);

            Assert.True(result == SignatureSimilarity.Identical || result == SignatureSimilarity.Same);
        }

        [Fact]
        public void DistortedImagesCompareAsIdenticalOrSame()
        {
            var firstSignature = _generator.GenerateSignature(SampleImages.MonaLisa.Value);
            var secondSignature = _generator.GenerateSignature
            (
                SampleImages.MonaLisa.Value.Clone
                (
                    o => o.Resize
                    (
                        SampleImages.MonaLisa.Value.Width * 2,
                        SampleImages.MonaLisa.Value.Height
                    )
                )
            );

            var result = firstSignature.CompareTo(secondSignature);

            Assert.True(result == SignatureSimilarity.Identical || result == SignatureSimilarity.Same);
        }

        [Theory]
        [MemberData(nameof(SampleImages.RecolouredImages), MemberType = typeof(SampleImages))]
        public void RecolouredImagesCompareAsSame(Image<L8> image)
        {
            var firstSignature = _generator.GenerateSignature(SampleImages.MonaLisa.Value);
            var secondSignature = _generator.GenerateSignature(image);

            var result = firstSignature.CompareTo(secondSignature);

            Assert.Equal(SignatureSimilarity.Same, result);
        }

        [Theory]
        [MemberData(nameof(SampleImages.SlightlyEditedImages), MemberType = typeof(SampleImages))]
        public void SlightlyEditedImagesCompareAsSimilar(Image<L8> image)
        {
            var firstSignature = _generator.GenerateSignature(SampleImages.MonaLisa.Value);
            var secondSignature = _generator.GenerateSignature(image);

            var result = firstSignature.CompareTo(secondSignature);

            Assert.Equal(SignatureSimilarity.Similar, result);
        }

        [Theory]
        [MemberData(nameof(SampleImages.SignificantlyEditedImages), MemberType = typeof(SampleImages))]
        public void SignificantlyEditedImagesCompareAsSimilar(Image<L8> image)
        {
            var firstSignature = _generator.GenerateSignature(SampleImages.MonaLisa.Value);
            var secondSignature = _generator.GenerateSignature(image);

            var result = firstSignature.CompareTo(secondSignature);

            Assert.Equal(SignatureSimilarity.Similar, result);
        }

        [Theory]
        [MemberData(nameof(SampleImages.StylizedCopies), MemberType = typeof(SampleImages))]
        public void StylizedCopiesCompareAsDissimilar(Image<L8> image)
        {
            var firstSignature = _generator.GenerateSignature(SampleImages.MonaLisa.Value);
            var secondSignature = _generator.GenerateSignature(image);

            var result = firstSignature.CompareTo(secondSignature);

            Assert.Equal(SignatureSimilarity.Dissimilar, result);
        }

        [Theory]
        [MemberData(nameof(SampleImages.Photos), MemberType = typeof(SampleImages))]
        public void ArrangedPhotoCopiesCompareAsDissimilar(Image<L8> image)
        {
            var firstSignature = _generator.GenerateSignature(SampleImages.MonaLisa.Value);
            var secondSignature = _generator.GenerateSignature(image);

            var result = firstSignature.CompareTo(secondSignature);

            Assert.Equal(SignatureSimilarity.Dissimilar, result);
        }

        [Theory]
        [MemberData(nameof(SampleImages.DifferentImages), MemberType = typeof(SampleImages))]
        public void DifferentImagesCompareAsDifferent(Image<L8> image)
        {
            var firstSignature = _generator.GenerateSignature(SampleImages.MonaLisa.Value);
            var secondSignature = _generator.GenerateSignature(image);

            var result = firstSignature.CompareTo(secondSignature);

            Assert.Equal(SignatureSimilarity.Different, result);
        }
    }
}
