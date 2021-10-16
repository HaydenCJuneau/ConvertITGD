using System;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace ConvertIt.Library
{
    public static class ImageExtLists
    {
        /*
         * Possible types to convert to:
         * - PNG
         * - JPG
         * - TIFF
         * - BMP
         * - GIF
         * - ICO
        */

        /// <summary>
        /// Dictionary that maps formats to their proper extensions
        /// </summary>
        public static readonly Dictionary<ImageFormat, string> ImageExtensions =
            new Dictionary<ImageFormat, string>() 
            {
                {ImageFormat.Png, ".png" },
                {ImageFormat.Jpeg, ".jpg" },
                {ImageFormat.Gif, ".gif" },
                {ImageFormat.Tiff, ".tiff" },
                {ImageFormat.Icon, ".ico" },
                {ImageFormat.Bmp, ".bmp" }
            };

        ///<summary>
        /// Dictionary of human readable format selections, with their int IDs as pair-keys
        /// </summary>
        public static readonly Dictionary<int, string> FormatSelections =
            new Dictionary<int, string>()
            {
                {0, "PNG (.png)" }, 
                {1, "JPG (.jpg)" }, 
                {2, "Bitmap (.bmp)" }, 
                {3, "TIFF (.tiff)" }, 
                {4, "GIF (.gif)" }, 
                {5, "Icon (.ico)" } 
            };

        ///<summary>
        /// Dictionary that converts the int ID for a format into the ImageFormat Class accociated
        /// </summary>
        public static readonly Dictionary<int, ImageFormat> FormatIDS =
            new Dictionary<int, ImageFormat>() 
            {
                {0, ImageFormat.Png },
                {1, ImageFormat.Jpeg },
                {2, ImageFormat.Bmp },
                {3, ImageFormat.Tiff },
                {4, ImageFormat.Gif },
                {5, ImageFormat.Icon }
            };

        ///<summary>
        /// List of raw valid extensions for image files
        /// </summary>
        public static readonly List<String> ImageValidExt =
            new List<string>()
            { 
                ".png",
                ".jpg",
                ".jpeg",
                ".gif",
                ".tiff",
                ".ico",
                ".bmp"
            };

    }


}