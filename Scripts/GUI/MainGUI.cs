using Godot;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;

using ConvertIt.Manager;
using ConvertIt.Library;

namespace ConvertIt.GUI
{
    //The class that controls the main GUI of the application
    public class MainGUI : Control
    {
        //Helpers
        private ImageListManager ListMan { get; set; }
        private ImageConverter Converter { get; set; }
        //Data
        private ImageFormat SelectedFormat { get; set; } = null;
        private List<ImageTexture> SelectedImages { get; set; } //A list that contains the preview images that have been selected
        //Nodes
        private OptionButton FormatSelector { get; set; }
        //Files and folder dialogs need to be seperate because there is no one mode that handles both kinds of selections
        private FileDialog SelectFilesDialog { get; set; } 
        private FileDialog SelectFolderDialog { get; set; }
        private GridContainer PreviewGrid { get; set; }

        // - - - Godot Methods - - - 
        public override void _Ready()
        {
            base._Ready();
            //--Create helper classes
            ListMan = new ImageListManager();
            Converter = new ImageConverter();
            //--Get control nodes
            FormatSelector = GetNode<OptionButton>("formatSelector");
            SelectFilesDialog = GetNode<FileDialog>("MultiFileDialog");
            SelectFolderDialog = GetNode<FileDialog>("FolderDialog");
            PreviewGrid = GetNode<GridContainer>("PreviewScroll/PreviewGrid");
            //--Initialize collections
            SelectedImages = new List<ImageTexture>();
            //--Initialize controls
            //Add format selection options to the drop down list
            foreach (var format in ImageExtLists.FormatSelections.Values) { FormatSelector.AddItem(format); }
            FormatSelector.Selected = 0;
        }

        // - - - Updating UI components - - - 
        private void UpdateSelectionDisplay()
        {
            //Clear preview section
            foreach(var icon in PreviewGrid.GetChildren())
            {
                var node = icon as TextureRect;
                node.QueueFree();
            }
            //Get collection of new preview icons
            SelectedImages = ListMan.GetImageTextures();
            //Set icons
            foreach(var image in SelectedImages)
            {
                var textureRect = new TextureRect();
                textureRect.Texture = image;
                textureRect.Expand = true;
                textureRect.StretchMode = TextureRect.StretchModeEnum.Scale;
                textureRect.RectMinSize = new Vector2(128, 128);
                PreviewGrid.AddChild(textureRect);
            }
        }

        // - - - Events from UI components - - - 
        public void SelectFilesButtonPressed()
        {
            SelectFilesDialog.PopupCenteredRatio();
        }

        public void SelectFolderButtonPressed()
        {
            SelectFolderDialog.PopupCenteredRatio();
        }

        public void ConvertButtonPressed()
        {
            Converter.ConvertImageBatch(ListMan.ImagePaths.ToArray(), SelectedFormat);
            ResetButtonPressed(); //Reset the view and lists upon success
        }

        public void ResetButtonPressed()
        {
            ListMan.ResetList();
            UpdateSelectionDisplay();
        }

        public void FormatSelected(int index)
        {            
            SelectedFormat = ImageExtLists.FormatIDS[FormatSelector.Selected];
            GD.Print("Selected format: " + SelectedFormat.ToString());
        }

        //Events from the file selector dialog
        //When a directory is selected
        public void Selector_DirSelect(string path)
        {
            ListMan.AddDirectory(path, true); //TODO: add toggle button for recursive scanning
            UpdateSelectionDisplay();
        }
        //When a single file is selected
        public void Selector_FileSelect(string path)
        {
            ListMan.AddFile(path);
            UpdateSelectionDisplay();
        }
        //When multiple files are selected
        public void Selector_FilesSelect(string[] paths)
        {
            ListMan.AddMultipleFiles(paths);
            UpdateSelectionDisplay();
        }
    }
}