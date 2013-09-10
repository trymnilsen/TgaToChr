using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TgaToChr
{

    class TgaFile
    {
        private const byte headerOffset = 18;

        private tgaHeader header;
        private PixelInfo[,] imageData;
        private string tgaPath;
        public tgaHeader Header { get { return header; } }
        public PixelInfo[,] bitMap { get { return imageData; } }
        public TgaFile(String tgaPath)
        {
            this.tgaPath = tgaPath;
        }
        //returns 255 if color is not defined
        public void ReadHeader()
        {
            //read header
            byte[] fileBytes = File.ReadAllBytes(tgaPath);

            header.idLength = fileBytes[0];
            header.colorMapType = fileBytes[1];
            header.dataTypeCode = fileBytes[2];

            header.colorMapStart = Util.LittleEndToShort(fileBytes[4], fileBytes[3]);
            header.colorMapLength = Util.LittleEndToShort(fileBytes[6], fileBytes[5]);
            header.colorMapDepth = fileBytes[7];

            header.x_origin = Util.LittleEndToShort(fileBytes[9], fileBytes[8]);
            header.y_origin = Util.LittleEndToShort(fileBytes[11], fileBytes[10]);
            header.width = Util.LittleEndToShort(fileBytes[13], fileBytes[12]);
            header.height = Util.LittleEndToShort(fileBytes[15], fileBytes[14]);

            header.bitsPerPixel = fileBytes[16];
            header.descriptor = fileBytes[17];

        }
        //private byte indexOfColor(byte color)
        //{
        //    for (byte i = 0; i < patternPalette.Count; i++)
        //    {
        //        if (color == patternPalette[i])
        //        {
        //            return i;
        //        }
        //    }
        //    return 255;
        //}
        public PixelInfo[,] ReadImageData()
        {
            byte[] fileBytes = File.ReadAllBytes(tgaPath);
            Console.WriteLine("First pixel r"+fileBytes[20]+"g"+fileBytes[19]+"b"+fileBytes[18]);
            int currentPixelnumber=0;

            imageData = new PixelInfo[header.width, header.height];
            //List<PixelInfo> uniquePixels = new List<PixelInfo>();
            for (int y = header.height-1; y >= 0; y -= 1)
            {
                for (int x = 0; x < Header.width; x += 1)
                {
                    byte[] colors = new byte[3];
                    for (int b = 0; b < 3; b++)
                    {
                        colors[b] = fileBytes[currentPixelnumber + b + headerOffset];
                    }
                    currentPixelnumber += 3;
                    PixelInfo currentPixel = new PixelInfo(colors[2], colors[1], colors[0]);
                    imageData[x, y] = currentPixel;
                    //if(!uniquePixels.Any(pixel => pixel == currentPixel))
                    //{
                    //    uniquePixels.Add(currentPixel);
                    //    Console.WriteLine("New unique color "+currentPixel.ToString());
                    //}
                }

            }
            return imageData;


            //Console.WriteLine("Number of colors: " + uniquePixels.Count);
        }

    }
}
