//
//  SignatureComparison.cs
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
using System.Linq;
using JetBrains.Annotations;

namespace Puzzle
{
    /// <summary>
    /// Computational class for comparing image signatures.
    /// </summary>
    [PublicAPI]
    public static class SignatureComparison
    {
        /// <summary>
        /// Calculates the euclidean length of an image signature.
        /// </summary>
        /// <param name="signature">The signature.</param>
        /// <returns>The euclidean length of the vector.</returns>
        [Pure]
        private static double EuclideanLength([NotNull] this IEnumerable<sbyte> signature)
        {
            return Math.Sqrt
            (
                signature.Sum
                (
                    l => Math.Pow(l, 2)
                )
            );
        }

        /// <summary>
        /// Subtracts one signature vector from another.
        /// </summary>
        /// <param name="left">The left signature.</param>
        /// <param name="right">The right signature.</param>
        /// <returns>The subtracted signature.</returns>
        [Pure]
        private static IEnumerable<sbyte> Subtract
        (
            [NotNull] this IEnumerable<LuminosityLevel> left,
            [NotNull] IEnumerable<LuminosityLevel> right
        )
        {
            var enumeratedLeft = left.ToList();
            var enumeratedRight = right.ToList();

            for (var i = 0; i < enumeratedLeft.Count; ++i)
            {
                var leftValue = (sbyte)enumeratedLeft[i];

                if (i >= enumeratedRight.Count)
                {
                    yield return leftValue;
                    continue;
                }

                var rightValue = (sbyte)enumeratedRight[i];
                if ((leftValue == 0 && rightValue == -2) || (leftValue == -2 && rightValue == 0))
                {
                    yield return -3;
                    continue;
                }

                if ((leftValue == 0 && rightValue == 2) || (leftValue == 2 && rightValue == 0))
                {
                    yield return 3;
                    continue;
                }

                yield return (sbyte)(leftValue - rightValue);
            }
        }

        /// <summary>
        /// Computes the normalized distance between two signatures.
        /// </summary>
        /// <param name="left">The left signature.</param>
        /// <param name="right">The right signature.</param>
        /// <returns>The normalized distance.</returns>
        [Pure, PublicAPI]
        public static double NormalizedDistance
        (
            [NotNull] this IEnumerable<LuminosityLevel> left,
            [NotNull] IEnumerable<LuminosityLevel> right
        )
        {
            var enumeratedLeft = left.ToList();
            var enumeratedRight = right.ToList();

            var subtractedVectors = enumeratedLeft.Subtract(enumeratedRight);
            var subtractedLength = subtractedVectors.EuclideanLength();

            var combinedLength = enumeratedLeft.Cast<sbyte>().EuclideanLength() +
                                 enumeratedRight.Cast<sbyte>().EuclideanLength();

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (combinedLength == 0.0)
            {
                return 0.0;
            }

            return subtractedLength / combinedLength;
        }

        /// <summary>
        /// Compares two image signatures, producing a simple similarity indication.
        /// </summary>
        /// <param name="left">The left signature.</param>
        /// <param name="right">The right signature.</param>
        /// <param name="sameThreshold">The threshold value for an image to be considered the same image. This typically
        /// means that the image has been scaled, resized, has artifacts, or is distorted in some way.</param>
        /// <param name="similarityThreshold">The threshold value for an image to be considered similar. This typically
        /// means that the image has been somewhat altered (such as having been recoloured, slightly edited, or
        /// watermarked). In testing, a value between 0.4 is usually a good fit.</param>
        /// <param name="dissimilarThreshold">The threshold value for an image to be considered dissimilar. This
        /// typically means that the image has been significantly altered, is of another character, or is transformed in
        /// some way. In testing, a value of 0.48 is usually a good fit.</param>
        /// <param name="differentThreshold">The threshold value for an image to be considered a different image. This
        /// typically means that the images have little to do with each other.</param>
        /// <returns>The similarity.</returns>
        [Pure]
        public static SignatureSimilarity CompareTo
        (
            [NotNull] this IEnumerable<LuminosityLevel> left,
            [NotNull] IEnumerable<LuminosityLevel> right,
            double sameThreshold = 0.4,
            double similarityThreshold = 0.48,
            double dissimilarThreshold = 0.68,
            double differentThreshold = 0.7
        )
        {
            var distance = left.NormalizedDistance(right);

            switch (distance)
            {
                case var value when value <= 0.0: return SignatureSimilarity.Identical;
                case var value when value <= sameThreshold: return SignatureSimilarity.Same;
                case var value when value <= similarityThreshold: return SignatureSimilarity.Similar;
                case var value when value <= dissimilarThreshold: return SignatureSimilarity.Dissimilar;
                case var value when value <= differentThreshold: return SignatureSimilarity.Different;
                default: return SignatureSimilarity.Different;
            }
        }
    }
}
