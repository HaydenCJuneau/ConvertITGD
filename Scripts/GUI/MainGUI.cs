using Godot;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;

using ConvertIt.Library;
using ConvertIt.GUI.Components;

namespace ConvertIt.GUI
{
    //The class that controls the main GUI of the application
    public class MainGUI : Control
    {
        //--Helpers
        private ImageListManager ListManager { get; set; }
        private ImageConverter Converter { get; set; }
        //--Data
        private ImageFormat SelectedFormat { get; set; } = null;
        private List<ImageTexture> PreviewImages { get; set; } //A list that contains the preview images that have been selected
        //--Nodes
        private OptionButton FormatSelector { get; set; }
        private GridContainer PreviewGrid { get; set; }
        //--Outsourced components
        private PackedScene ThumbnailScene { get; set; }
        private PackedScene ProcessPopupScene { get; set; }
        //File dialogs are seperate because there isnt a mode for folers and multi-files
        private FileDialog SelectFilesDialog { get; set; } 
        private FileDialog SelectFolderDialog { get; set; }       

        // - - - Godot Methods - - - 
        public override void _Ready()
        {
            base._Ready();
            //--Create helper classes
            ListManager = new ImageListManager();
            Converter = new ImageConverter();
            //--Get control nodes
            FormatSelector = GetNode<OptionButton>("formatSelector");
            SelectFilesDialog = GetNode<FileDialog>("MultiFileDialog");
            SelectFolderDialog = GetNode<FileDialog>("FolderDialog");
            PreviewGrid = GetNode<GridContainer>("PreviewScroll/PreviewGrid");
            //--Load packed components
            ThumbnailScene = GD.Load<PackedScene>("res://GUI/ImageGUIComponents/ImageThumbnail.tscn");

            //--Initialize collections
            PreviewImages = new List<ImageTexture>();
        }

        // - - - Updating UI components - - - 
        private void UpdateSelectionDisplay()
        {
            //Clear preview section
            foreach(var thumbnail in PreviewGrid.GetChildren())
            {
                (thumbnail as Control).QueueFree();
            }
            //Get collection of new preview icons
            var previewImages = ListManager.GetImageTextures();
            var previewTitles = ListManager.GetImageTitles();
            //Set icons
            if(previewImages.Count != 0 && previewTitles.Count != 0)
            {
                for(var i = 0; i < previewImages.Count; i++)
                {
                    var thumbnailInst = ThumbnailScene.Instance() as ImageThumbnail;
                    thumbnailInst.Initialize(previewTitles[i], previewImages[i]);
                    PreviewGrid.AddChild(thumbnailInst);
                }
            }
        }

        // - - - Events from UI components - - - 
        public void SelectFilesButtonPressed() => SelectFilesDialog.PopupCenteredRatio();

        public void SelectFolderButtonPressed() => SelectFolderDialog.PopupCenteredRatio();

        public void ConvertButtonPressed()
        {
            Converter.ConvertImageBatch(ListManager.ImagePaths.ToArray(), SelectedFormat);
            ResetButtonPressed(); //Reset the view and lists upon success
        }

        public void ResetButtonPressed()
        {
            ListManager.ResetList();
            UpdateSelectionDisplay();
        }

        public void FormatSelected(int index) 
        {            
            SelectedFormat = ImageExtLists.FormatIDS[index];
            GD.Print("Selected format: " + SelectedFormat.ToString());
        }

        //Events from the file selector dialog
        //When a directory is selected
        public void Selector_DirSelect(string path)
        {
            ListManager.AddDirectory(path, true); //TODO: add toggle button for recursive scanning
            UpdateSelectionDisplay();
        }
        //When a single file is selected
        public void Selector_FileSelect(string path)
        {
            ListManager.AddFile(path);
            UpdateSelectionDisplay();
        }
        //When multiple files are selected
        public void Selector_FilesSelect(string[] paths)
        {
            ListManager.AddMultipleFiles(paths);
            UpdateSelectionDisplay();
        }
    }
}