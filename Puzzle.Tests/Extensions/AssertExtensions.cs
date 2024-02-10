//
//  SPDX-FileName: AssertExtensions.cs
//  SPDX-FileCopyrightText: Copyright (c) Jarl Gullberg
//  SPDX-License-Identifier: AGPL-3.0-or-later
//

using System;
using Xunit;

namespace Puzzle.Tests.Extensions;

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