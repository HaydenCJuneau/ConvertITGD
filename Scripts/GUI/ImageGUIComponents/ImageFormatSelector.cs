using Godot;

using ConvertIt.Library;

namespace ConvertIt.Controls
{
    //This class is only here to initialize the format selector for images, this can be done in the  
    //Main GUI script, but it doesnt look good there so i cant be bothered lol cry about it XD
    public class ImageFormatSelector : OptionButton
    {
        public override void _Ready()
        {
            base._Ready();

            //Add format selection options to the drop down list
            foreach (var format in ImageExtLists.FormatSelections.Values) { AddItem(format); }
            Selected = 0;
        }
    }
}
