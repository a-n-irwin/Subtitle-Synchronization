using System;
using SRTFileEditor;


namespace Example_SRTFileEditor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SRTFileReader reader = new SRTFileReader();
            int milliseconds;


            ShowHeaderInterface();


            // prompt the user for the subtitle file
            Console.Write("\n\nPath to subtitle(.srt) file: ");

            string path = Console.ReadLine();


            // Error message to handle empty path
            if (path == String.Empty)
            {
                ShowErrorMessage("Path can not be empty!");
                return;
            }


            // if the file could not be found from the path, an exception is thrown when creating the reader
            try
            {
                reader.Path = path;
            } catch (System.IO.FileNotFoundException ex)
            {
                ShowErrorMessage("Could not find file!");
                return;
            }

            Console.Write("Enter adjustment time in milliseconds: ");


            // Error message to handle invalid milliseconds input
            if (!int.TryParse(Console.ReadLine(), out milliseconds))
            {
                ShowErrorMessage("Invalid integer input!");
                return;
            }


            reader.AdjustTime(milliseconds);
            WriteColor($"SUCCESS: Subtitle adjusted by {milliseconds} milliseconds", ConsoleColor.Green);

            Console.Read();
        }


        public static void ShowHeaderInterface()
        {
            WriteColor("\n\t\t---------- Welcome to SRT subtitle file editor! ----------\n\n", ConsoleColor.Green);

            WriteColor("\t\t\t\t Version: ", ConsoleColor.Green);
            Console.WriteLine("1.0");

            WriteColor("\t\t\t\tDeveloper: ", ConsoleColor.Green);
            Console.WriteLine("ANRI");

            WriteColor("\t\t\tEmail: ", ConsoleColor.Green);
            Console.WriteLine("yours.truly.anri@gmail.com");


            WriteColor("\n\t\tUpdate info: ", ConsoleColor.Green);
            Console.WriteLine("This version is only capable of adjusting the\n\t\twhole time for the subtitle file by a specified milliseconds.");

            WriteColor("\t\t----------------------------------------------------------\n", ConsoleColor.Green);
        }


        public static void WriteColor(string message,ConsoleColor color)
        {
            ConsoleColor fgc = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.Write(message);

            Console.ForegroundColor = fgc;
        }



         public static void ShowErrorMessage(string message)
        {
            WriteColor("ERROR: ", ConsoleColor.Red);
            Console.WriteLine(message);

            Console.WriteLine("Please restart the program.\n");
            Console.Read();
        }
    }
}