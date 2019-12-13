//
//  SampleImages.cs
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
using System.Reflection;
using System.Threading;
using SixLabors.ImageSharp;

namespace Puzzle.Tests.Data
{
    /// <summary>
    /// Contains bundled sample images.
    /// </summary>
    public static class SampleImages
    {
        /// <summary>
        /// Gets a set of slightly edited Mona Lisa images.
        /// </summary>
        public static IEnumerable<object[]> SlightlyEditedImages
        {
            get
            {
                yield return new object[] { Lisa9.Value };
                yield return new object[] { ChromaticMona.Value };
            }
        }

        /// <summary>
        /// Gets a set of significantly edited Mona Lisa images.
        /// </summary>
        public static IEnumerable<object[]> SignificantlyEditedImages
        {
            get
            {
                yield return new object[] { BritneySpearsMona.Value };
                yield return new object[] { GangstaMona.Value };
            }
        }

        /// <summary>
        /// Gets a set of recoloured Mona Lisa images.
        /// </summary>
        public static IEnumerable<object[]> RecolouredImages
        {
            get
            {
                yield return new object[] { BlueMona.Value };
                yield return new object[] { CyanMona.Value };
                yield return new object[] { GreenMona.Value };
                yield return new object[] { PinkMona.Value };
                yield return new object[] { RedMona.Value };
                yield return new object[] { YellowMona.Value };
            }
        }

        /// <summary>
        /// Gets a set of stylized Mona Lisa image copies.
        /// </summary>
        public static IEnumerable<object[]> StylizedCopies
        {
            get
            {
                yield return new object[] { HorrorMona.Value };
                yield return new object[] { MonaLizzle.Value };
                yield return new object[] { PointilistMona.Value };
            }
        }

        /// <summary>
        /// Gets a set of real-life Mona Lisa photographs, containing real people.
        /// </summary>
        public static IEnumerable<object[]> Photos
        {
            get
            {
                yield return new object[] { PhotoMona.Value };
            }
        }

        /// <summary>
        /// Gets a set of images that do not depict the Mona Lisa.
        /// </summary>
        public static IEnumerable<object[]> DifferentImages
        {
            get
            {
                yield return new object[] { SaintMatthew.Value };
                yield return new object[] { SickBacchus.Value };
                yield return new object[] { JudithBeheading.Value };
            }
        }

        /// <summary>
        /// Gets the canonical Mona Lisa image.
        /// </summary>
        public static Lazy<Image> MonaLisa { get; } = GetLazyLoaderForImage("original-mona.jpg");

        /// <summary>
        /// Gets an edited Mona Lisa image.
        /// </summary>
        public static Lazy<Image> GangstaMona { get; } = GetLazyLoaderForImage("gangsta-mona.jpg");

        /// <summary>
        /// Gets an edited Mona Lisa image.
        /// </summary>
        public static Lazy<Image> BritneySpearsMona { get; } = GetLazyLoaderForImage("lisa-spears.jpg");

        /// <summary>
        /// Gets an edited Mona Lisa image.
        /// </summary>
        public static Lazy<Image> Lisa9 { get; } = GetLazyLoaderForImage("lisa9.jpg");

        /// <summary>
        /// Gets a stylized Mona Lisa image.
        /// </summary>
        public static Lazy<Image> HorrorMona { get; } = GetLazyLoaderForImage("mona-horror.jpg");

        /// <summary>
        /// Gets a stylized Mona Lisa image.
        /// </summary>
        public static Lazy<Image> MonaLizzle { get; } = GetLazyLoaderForImage("mona-lizzle.jpg");

        /// <summary>
        /// Gets a stylized Mona Lisa image.
        /// </summary>
        public static Lazy<Image> PointilistMona { get; } = GetLazyLoaderForImage("pointilist-mona.jpg");

        /// <summary>
        /// Gets an edited Mona Lisa image.
        /// </summary>
        public static Lazy<Image> ChromaticMona { get; } = GetLazyLoaderForImage("truth-lisa-chromatic.jpg");

        /// <summary>
        /// Gets a real-life photograph Mona Lisa image.
        /// </summary>
        public static Lazy<Image> PhotoMona { get; } = GetLazyLoaderForImage("truth-lisa-photo.jpg");

        /// <summary>
        /// Gets a recoloured Mona Lisa image.
        /// </summary>
        public static Lazy<Image> BlueMona { get; } = GetLazyLoaderForImage("recoloured-mona-blue.jpg");

        /// <summary>
        /// Gets a recoloured Mona Lisa image.
        /// </summary>
        public static Lazy<Image> CyanMona { get; } = GetLazyLoaderForImage("recoloured-mona-cyan.jpg");

        /// <summary>
        /// Gets a recoloured Mona Lisa image.
        /// </summary>
        public static Lazy<Image> GreenMona { get; } = GetLazyLoaderForImage("recoloured-mona-green.jpg");

        /// <summary>
        /// Gets a recoloured Mona Lisa image.
        /// </summary>
        public static Lazy<Image> PinkMona { get; } = GetLazyLoaderForImage("recoloured-mona-pink.jpg");

        /// <summary>
        /// Gets a recoloured Mona Lisa image.
        /// </summary>
        public static Lazy<Image> RedMona { get; } = GetLazyLoaderForImage("recoloured-mona-red.jpg");

        /// <summary>
        /// Gets a recoloured Mona Lisa image.
        /// </summary>
        public static Lazy<Image> YellowMona { get; } = GetLazyLoaderForImage("recoloured-mona-yellow.jpg");

        /// <summary>
        /// Gets an original Caravaggio painting.
        /// </summary>
        public static Lazy<Image> SaintMatthew { get; } = GetLazyLoaderForImage("saint-matthew.jpg");

        /// <summary>
        /// Gets an original Caravaggio painting.
        /// </summary>
        public static Lazy<Image> JudithBeheading { get; } = GetLazyLoaderForImage("judith-beheading.jpg");

        /// <summary>
        /// Gets an original Caravaggio painting.
        /// </summary>
        public static Lazy<Image> SickBacchus { get; } = GetLazyLoaderForImage("sick-bacchus.jpg");

        private static Lazy<Image> GetLazyLoaderForImage(string image)
        {
            return new Lazy<Image>
            (
                () =>
                {
                    using var resourceStream = Assembly
                        .GetExecutingAssembly()
                        .GetManifestResourceStream($"Puzzle.Tests.Images.{image}");

                    return Image.Load(resourceStream);
                },
                LazyThreadSafetyMode.ExecutionAndPublication
            );
        }
    }
}
