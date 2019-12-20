//
//  AssertExtensions.cs
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

using System;
using Xunit;

namespace Puzzle.Tests.Extensions
{
    /// <summary>
    /// Contains extra assertions.
    /// </summary>
    public static class AssertExtensions
    {
        /// <summary>
        /// Asserts that two spans are sequence equal.
        /// </summary>
        /// <param name="left">The first span.</param>
        /// <param name="right">The second span.</param>
        /// <typeparam name="T">The type in the span.</typeparam>
        public static void SequenceEqual<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        {
            Assert.Equal(left.Length, right.Length);

            for (var i = 0; i < left.Length; i++)
            {
                Assert.Equal(left[i], right[i]);
            }
        }
    }
}
