using System;
using System.IO;

namespace ConvertIt
{
    //Holds references to the file paths in the program
    public static class DataPaths
    {
        /// <summary> The directory that the .exe runs from </summary>
        public static readonly string WORKING_DIRECTORY = Environment.CurrentDirectory;
        ///<summary> The directory that the .csproj is located </summary>
        public static readonly string PROJECT_DIRECTORY = Directory.GetParent(WORKING_DIRECTORY).Parent.Parent.FullName;
        ///<summary> The input folder directory </summary>
        public static readonly string INPUT_DIRECTORY = PROJECT_DIRECTORY + @"/ImageInput/";
        ///<summary> The output folder directory </summary>
        public static readonly string OUTPUT_DIRECTORY = PROJECT_DIRECTORY + @"/ImageOutput/";
    }
}