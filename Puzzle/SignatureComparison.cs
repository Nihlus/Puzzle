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
using System.Runtime.InteropServices;
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
        private static double EuclideanLength(this ReadOnlySpan<sbyte> signature)
        {
            var sum = 0.0;
            foreach (var val in signature)
            {
                sum += Math.Pow(val, 2);
            }

            return Math.Sqrt(sum);
        }

        /// <summary>
        /// Subtracts one signature vector from another.
        /// </summary>
        /// <param name="left">The left signature.</param>
        /// <param name="right">The right signature.</param>
        /// <returns>The subtracted signature.</returns>
        [Pure]
        private static ReadOnlySpan<sbyte> Subtract
        (
            this ReadOnlySpan<LuminosityLevel> left,
            ReadOnlySpan<LuminosityLevel> right
        )
        {
            Span<sbyte> result = new sbyte[left.Length];

            for (var i = 0; i < left.Length; ++i)
            {
                var leftValue = (sbyte)left[i];

                if (i >= right.Length)
                {
                    result[i] = leftValue;
                    continue;
                }

                var rightValue = (sbyte)right[i];
                if ((leftValue == 0 && rightValue == -2) || (leftValue == -2 && rightValue == 0))
                {
                    result[i] = -3;
                    continue;
                }

                if ((leftValue == 0 && rightValue == 2) || (leftValue == 2 && rightValue == 0))
                {
                    result[i] = 3;
                    continue;
                }

                result[i] = (sbyte)(leftValue - rightValue);
            }

            return result;
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
            this ReadOnlySpan<LuminosityLevel> left,
            ReadOnlySpan<LuminosityLevel> right
        )
        {
            var subtractedVectors = left.Subtract(right);
            var subtractedLength = subtractedVectors.EuclideanLength();

            var combinedLength = MemoryMarshal.Cast<LuminosityLevel, sbyte>(left).EuclideanLength() +
                                 MemoryMarshal.Cast<LuminosityLevel, sbyte>(right).EuclideanLength();

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
            this ReadOnlySpan<LuminosityLevel> left,
            ReadOnlySpan<LuminosityLevel> right,
            double sameThreshold = 0.4,
            double similarityThreshold = 0.48,
            double dissimilarThreshold = 0.68,
            double differentThreshold = 0.7
        )
        {
            var distance = left.NormalizedDistance(right);

            return distance switch
            {
                <= 0.0 => SignatureSimilarity.Identical,
                var value when value <= sameThreshold => SignatureSimilarity.Same,
                var value when value <= similarityThreshold => SignatureSimilarity.Similar,
                var value when value <= dissimilarThreshold => SignatureSimilarity.Dissimilar,
                var value when value <= differentThreshold => SignatureSimilarity.Different,
                _ => SignatureSimilarity.Different
            };
        }
    }
}
