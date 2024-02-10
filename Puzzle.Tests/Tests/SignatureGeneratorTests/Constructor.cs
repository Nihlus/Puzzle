//
//  SPDX-FileName: Constructor.cs
//  SPDX-FileCopyrightText: Copyright (c) Jarl Gullberg
//  SPDX-License-Identifier: AGPL-3.0-or-later
//

using Xunit;

#pragma warning disable CS1591, SA1600

namespace Puzzle.Tests.Tests.SignatureGeneratorTests
{
    public partial class SignatureGeneratorTests
    {
        public class Constructor
        {
            [Fact]
            public void GridSizeParameterCorrectlySetsGridSizeProperty()
            {
                uint expected = 10;

                var generator = new SignatureGenerator(gridSize: expected);
                Assert.Equal(expected, generator.GridSize);
            }

            [Fact]
            public void NoiseCutoffParameterCorrectlySetsNoiseCutoffProperty()
            {
                var expected = 20.0;

                var generator = new SignatureGenerator(noiseCutoff: expected);
                Assert.Equal(expected, generator.NoiseCutoff);
            }

            [Fact]
            public void SampleSizeRatioParameterCorrectlySetsSampleSizeRatioProperty()
            {
                var expected = 30.0;

                var generator = new SignatureGenerator(sampleSizeRatio: expected);
                Assert.Equal(expected, generator.SampleSizeRatio);
            }

            [Fact]
            public void EnableAutocropParameterCorrectlySetsEnableAutocropProperty()
            {
                var generator = new SignatureGenerator(enableAutocrop: false);
                Assert.False(generator.EnableAutocrop);

                generator = new SignatureGenerator();
                Assert.True(generator.EnableAutocrop);
            }
        }
    }
}
