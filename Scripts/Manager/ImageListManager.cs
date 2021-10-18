using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ConvertIt.Library
{
    /// <summary>
    /// List manager is a helper class that manages the lists and data generated and needed by the main GUI when running
    /// </summary>
    public class ImageListManager
    {
        //Collections
        public List<string> ImagePaths { get; private set; }

        public ImageListManager()
        {
            ImagePaths = new List<string>();
        }

        // - - - Working with and adding to the list - - - 
        public void ResetList()
        {
            ImagePaths = new List<string>();
        }
        //Appends a single file directory to the list, returns true if file is valid
        public bool AddFile(string path)
        {
            string ext = Path.GetExtension(path).ToLower();
            if (ImageExtLists.ImageValidExt.Contains(ext))
            {
                ImagePaths.Add(path);
                return true;
            }
            else { return false; }
        }
        //Parses a list of files and adds them to the list, returns number of files that were valid 
        public int AddMultipleFiles(string[] paths)
        {
            int filesSucceeded = 0;
            foreach(var path in paths)
            {
                if(AddFile(path))
                {
                    filesSucceeded++;
                }
            }
            return filesSucceeded;
        }
        //Parses a directory and adds valid files into the list, returns number of files that were valid 
        public int AddDirectory(string dir, bool scanSubDirs)
        {
            if(scanSubDirs)
            {
                return AddMultipleFiles(FindFilesRecursively(dir));
            }
            else
            {
                return AddMultipleFiles(Directory.GetFiles(dir));
            }
        }

        // - - - Translating lists - - - 
        public List<Image> GetImages()
        {
            var imageList = new List<Image>();
            foreach(var path in ImagePaths)
            {
                imageList.Add(Image.FromFile(path));
            }
            return imageList;
        }

        public List<Godot.ImageTexture> GetImageTextures()
        {
            var textureList = new List<Godot.ImageTexture>();
            foreach (var path in ImagePaths)
            {
                var texture = new Godot.ImageTexture();
                var image = new Godot.Image();
                image.Load(path);
                texture.CreateFromImage(image);
                textureList.Add(texture);
            }
            return textureList;
        }

        public List<string> GetImageTitles()
        {
            var titleList = new List<string>();

            foreach(var path in ImagePaths)
            {
                var title = path.Substring(path.LastIndexOf(@"\") + 1);
                titleList.Add(title);
            }

            return titleList;
        }
        
        // - - - Search methods - - - 
        /// <summary>
        /// Returns a list of all files under a directory, including those in subdirectories
        /// </summary>
        private string[] FindFilesRecursively(string root)
        {
            var foundFiles = new List<string>();
            var foundDirs = new List<string>() { root };
            while(foundDirs.Count > 0)
            {
                var thisDir = foundDirs[0];
                foundFiles.AddRange(Directory.GetFiles(thisDir));
                foundDirs.AddRange(Directory.GetDirectories(thisDir));
                foundDirs.Remove(thisDir);
            }

            return foundFiles.ToArray();
        }
    }
}