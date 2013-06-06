using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TgaToChr
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = args[0];
            Console.WriteLine("Generating from file '"+filePath+"'");
            //Check if our file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("**Error, file does not exist**");
                Console.ReadKey();
                return;
            }
            //Check if file is valid
            TgaFile sourceImage = new TgaFile(filePath);
            try
            {
                sourceImage.ReadHeader();
                if (sourceImage.Header.width != 128 && sourceImage.Header.height != 128)
                {
                    Console.WriteLine("**Error, file dimentions not 128x128, was: " + sourceImage.Header.width + "x" + sourceImage.Header.height + "**");
                    Console.ReadKey();
                    return;
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                Console.WriteLine("**Error, file access permitted by system **");
            }
            catch (Exception ex)
            {
                Console.WriteLine("**Error, General error **");
            }
            //Try to read image data
            try
            {
                sourceImage.ReadImageData();
            }
            catch (Exception ex)
            {
                Console.WriteLine("** Error, error processing image data" + ex.ToString());
            }
            
            Console.ReadKey();
        }
    }
}
