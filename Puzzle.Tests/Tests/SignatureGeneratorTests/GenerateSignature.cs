//
//  GenerateSignature.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2017 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Affero General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Affero General Public License for more details.
//
//  You should have received a copy of the GNU Affero General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using Puzzle.Tests.Data;
using Puzzle.Tests.Extensions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

#pragma warning disable CS1591, SA1600

namespace Puzzle.Tests.Tests.SignatureGeneratorTests
{
    public partial class SignatureGeneratorTests
    {
        public class GenerateSignature
        {
            private readonly SignatureGenerator _generator;

            public GenerateSignature()
            {
                _generator = new SignatureGenerator();
            }

            [Fact]
            public void GeneratedSignatureMatchesExpectedAlgorithmResults()
            {
                var expectedSignature = SampleData.MonaLisaSignature.Value;
                var actualSignature = _generator.GenerateSignature(SampleImages.MonaLisa.Value);

                AssertExtensions.SequenceEqual(expectedSignature.Span, actualSignature);
            }

            [Theory]
            [MemberData(nameof(SampleImages.SmallImages), MemberType = typeof(SampleImages))]
            public void CanGenerateSignatureForSmallImages(Image<Gray8> smallImage)
            {
                var signature = _generator.GenerateSignature(smallImage);
            }

            [Fact]
            public void CanGenerateSignatureForLargeImage()
            {
                var signature = _generator.GenerateSignature(SampleImages.Uniform8192.Value);
            }
        }
    }
}
