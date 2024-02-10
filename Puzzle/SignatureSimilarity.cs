//
//  SPDX-FileName: SignatureSimilarity.cs
//  SPDX-FileCopyrightText: Copyright (c) Jarl Gullberg
//  SPDX-License-Identifier: AGPL-3.0-or-later
//

using JetBrains.Annotations;

namespace Puzzle;

/// <summary>
/// Enumerates the various similarity levels between two images.
/// </summary>
[PublicAPI]
public enum SignatureSimilarity
{
    /// <summary>
    /// The images are identical.
    /// </summary>
    Identical,

    /// <summary>
    /// The image is the same image.
    /// </summary>
    Same,

    /// <summary>
    /// The images are similar.
    /// </summary>
    Similar,

    /// <summary>
    /// The images are dissimilar.
    /// </summary>
    Dissimilar,

    /// <summary>
    /// The images are different images.
    /// </summary>
    Different
}
