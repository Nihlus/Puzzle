//
//  EnumerableExtensions.cs
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

using System.Collections.Generic;
using System.Linq;

namespace Puzzle.Extensions
{
    /// <summary>
    /// Holds extension methods for the IEnumerable interface.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Calculates the median of a set of values.
        /// </summary>
        /// <param name="source">The source sequence.</param>
        /// <returns>The median value.</returns>
        public static double Median(this IEnumerable<double> source)
        {
            var sorted = source.OrderBy(x => x).ToList();

            if (sorted.Count == 1)
            {
                return sorted[0];
            }

            var halfwayIndex = sorted.Count / 2;

            if (sorted.Count % 2 == 0)
            {
                return sorted.ElementAt(halfwayIndex);
            }

            return (sorted.ElementAt(halfwayIndex) + sorted.ElementAt(halfwayIndex - 1)) / 2.0;
        }
    }
}
