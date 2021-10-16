using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using ConvertIt.Library;

namespace ConvertIt
{
    public class ImageConverter
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

        //Data
        public int FilesConverted { get; private set; } = 0;

        //Constructor
        public ImageConverter() { }

        //Process a batch of images. The output directory gets assigned to a new folder in the parent of the first item.
        public void ConvertImageBatch(string[] files, ImageFormat outFormat)
        {
            if(files.Length < 1) { return; } //Do not process if there is nothing to convert
            var source = Directory.GetParent(files[0]);
            var outputDir = Directory.CreateDirectory(source + @"ConvertITOutput\");
            //Iterate through batch
            foreach(var image in files)
            {
                ConvertImage(image, outputDir.FullName, outFormat);
            }
        }

        //Process a single image into an output folder
        public void ConvertImage(string filePath, string outputDir, ImageFormat outFormat)
        {
            try
            {
                //Extract extension and name
                string ext = Path.GetExtension(filePath).ToLower();
                string name = Path.GetFileNameWithoutExtension(filePath);
                //Turn into image class format
                Image image = Image.FromFile(filePath);
                //Save image as the output type
                image.Save(outputDir + @"/" + name + 
                    ImageExtLists.ImageExtensions[outFormat], outFormat);
                //Release resource as to not create memory leak
                image.Dispose();
                //Increase number of files converted
                Godot.GD.Print($"Successfully converted {name}{ext}");
                FilesConverted++;             
            }
            catch(Exception ex)
            {
                Godot.GD.Print($"File at: {filePath} could not be converted.");
                Godot.GD.Print(ex);
            }
        }
    }
}
