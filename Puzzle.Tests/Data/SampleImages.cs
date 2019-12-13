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
