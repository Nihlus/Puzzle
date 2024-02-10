//
//  SPDX-FileName: SampleData.cs
//  SPDX-FileCopyrightText: Copyright (c) Jarl Gullberg
//  SPDX-License-Identifier: AGPL-3.0-or-later
//

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Puzzle.Tests.Data;

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

                if (resourceStream is null)
                {
                    throw new FileNotFoundException
                    (
                        "Couldn't find the requested resource.",
                        $"Puzzle.Tests.Signatures.{name}"
                    );
                }

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
