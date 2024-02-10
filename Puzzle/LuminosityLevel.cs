//
//  SPDX-FileName: LuminosityLevel.cs
//  SPDX-FileCopyrightText: Copyright (c) Jarl Gullberg
//  SPDX-License-Identifier: AGPL-3.0-or-later
//

using JetBrains.Annotations;

namespace Puzzle
{
    /// <summary>
    /// Enumerates relative luminosity levels.
    /// </summary>
    [PublicAPI]
    public enum LuminosityLevel : sbyte
    {
        /// <summary>
        /// The neighbour is much darker than the base point.
        /// </summary>
        MuchDarker = -2,

        /// <summary>
        /// The neighbour is darker than the base point.
        /// </summary>
        Darker = -1,

        /// <summary>
        /// The neighbour is of same or similar luminosity as the base point.
        /// </summary>
        Same = 0,

        /// <summary>
        /// The neighbour is lighter than the base point.
        /// </summary>
        Lighter = 1,

        /// <summary>
        /// The neighbour is much lighter than the base point.
        /// </summary>
        MuchLighter = 2
    }
}
