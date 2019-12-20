//
//  SampleData.cs
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Puzzle.Tests.Data
{
    /// <summary>
    /// Contains bundled sample images.
    /// </summary>
    public static class SampleData
    {
        /// <summary>
        /// Gets the canonical Mona Lisa image signature.
        /// </summary>
        public static Lazy<ReadOnlyMemory<LuminosityLevel>> MonaLisaSignature { get; } =
            GetLazyLoaderForSignature("original-mona.signature");

        private static Lazy<ReadOnlyMemory<LuminosityLevel>> GetLazyLoaderForSignature(string name)
        {
            return new Lazy<ReadOnlyMemory<LuminosityLevel>>
            (
                () =>
                {
                    using var resourceStream = Assembly
                        .GetExecutingAssembly()
                        .GetManifestResourceStream($"Puzzle.Tests.Signatures.{name}");

                    using var reader = new BinaryReader(resourceStream);

                    return new ReadOnlyMemory<LuminosityLevel>
                    (
                        reader.ReadBytes((int)reader.BaseStream.Length).Select(b => (LuminosityLevel)b).ToArray()
                    );
                },
                LazyThreadSafetyMode.ExecutionAndPublication
            );
        }
    }
}
