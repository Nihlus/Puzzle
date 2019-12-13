//
//  Constructor.cs
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

using Xunit;

#pragma warning disable CS1591, SA1600

namespace Puzzle.Tests.Tests.SignatureGenerator
{
    public partial class SignatureGeneratorTests
    {
        public class Constructor
        {
            [Fact]
            public void GridSizeParameterCorrectlySetsGridSizeProperty()
            {
                uint expected = 10;

                var generator = new Puzzle.SignatureGenerator(gridSize: expected);
                Assert.Equal(expected, generator.GridSize);
            }

            [Fact]
            public void NoiseCutoffParameterCorrectlySetsNoiseCutoffProperty()
            {
                var expected = 20.0;

                var generator = new Puzzle.SignatureGenerator(noiseCutoff: expected);
                Assert.Equal(expected, generator.NoiseCutoff);
            }

            [Fact]
            public void SampleSizeRatioParameterCorrectlySetsSampleSizeRatioProperty()
            {
                var expected = 30.0;

                var generator = new Puzzle.SignatureGenerator(sampleSizeRatio: expected);
                Assert.Equal(expected, generator.SampleSizeRatio);
            }

            [Fact]
            public void EnableAutocropParameterCorrectlySetsEnableAutocropProperty()
            {
                var generator = new Puzzle.SignatureGenerator(enableAutocrop: false);
                Assert.False(generator.EnableAutocrop);

                generator = new Puzzle.SignatureGenerator(enableAutocrop: true);
                Assert.True(generator.EnableAutocrop);
            }
        }
    }
}
