//
//  SPDX-FileName: EnumerableExtensions.cs
//  SPDX-FileCopyrightText: Copyright (c) Jarl Gullberg
//  SPDX-License-Identifier: AGPL-3.0-or-later
//

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Puzzle.Extensions;

/// <summary>
/// Holds extension methods for the IEnumerable interface.
/// </summary>
internal static class EnumerableExtensions
{
    /// <summary>
    /// Calculates the median of a set of values.
    /// </summary>
    /// <param name="source">The source sequence.</param>
    /// <returns>The median value.</returns>
    [Pure]
    internal static double Median(this IEnumerable<double> source)
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
