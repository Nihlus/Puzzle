//
//  LuminosityLevel.cs
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

namespace Puzzle
{
    /// <summary>
    /// Enumerates relative luminosity levels.
    /// </summary>
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
