using Godot;
using System;

namespace ConvertIt.GUI.Components
{
    //Image thumbnails are generated when images have been opened up into the converter GUI
    //They display a small thumbnail image and the title of the imagefile
    public class ImageThumbnail : TextureRect
    {
        //Nodes
        private Label TitleLabel { get; set; }

        // - - - Initialization - - -
        public void Initialize(string title, ImageTexture thumbnail)
        {
            //Get Nodes
            TitleLabel = GetNode<Label>("Title");
            //Apply data to nodes
            Texture = thumbnail;
            TitleLabel.Text = title;
        }
    }
}