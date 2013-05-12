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
        private byte[] imageData;
        private string tgaPath;
        public tgaHeader Header { get { return header; } }
        public List<PixelInfo> patternValues;
        public List<List<byte>> patternPixels;
        public TgaFile(String tgaPath)
        {
            this.tgaPath = tgaPath;
            patternPixels = new List<List<byte>>();
            patternValues = new List<PixelInfo>(4);

        }
        //returns 255 if color is not defined
        private byte colorIsDefined(PixelInfo pi)
        {
            byte returnCode =255;
            for(byte i=0; i<patternValues.Count; i++)
            {
                if(patternValues[i]==pi)
                {
                    returnCode=i;
                    break;
                }
            }
            return returnCode;
        }
        public void ReadHeader()
        {
            //read header
            byte[] fileBytes = File.ReadAllBytes(tgaPath);

            header.idLength = fileBytes[0];
            header.colorMapType = fileBytes[1];
            header.dataTypeCode = fileBytes[2];

            header.colorMapStart = Util.littleEndToShort(fileBytes[4], fileBytes[3]);
            header.colorMapLength = Util.littleEndToShort(fileBytes[6], fileBytes[5]);
            header.colorMapDepth = fileBytes[7];

            header.x_origin = Util.littleEndToShort(fileBytes[9], fileBytes[8]);
            header.y_origin = Util.littleEndToShort(fileBytes[11], fileBytes[10]);
            header.width = Util.littleEndToShort(fileBytes[13], fileBytes[12]);
            header.height = Util.littleEndToShort(fileBytes[15], fileBytes[14]);

            header.bitsPerPixel = fileBytes[16];
            header.descriptor = fileBytes[17];

        }
        public void ReadImageData()
        {
            int test = 0;
            byte[] fileBytes = File.ReadAllBytes(tgaPath);
            Console.WriteLine("First pixel r"+fileBytes[20]+"g"+fileBytes[19]+"b"+fileBytes[18]);
            int prevLN = 0;
            

            for (int y = 0; y < header.height; y+=1)
            {
                List<byte> linePixels = new List<byte>();
                patternPixels.Add(linePixels);
                for (int x = 0; x < Header.width; x += 1)
                {
                    int currentPixelnumber = (y * Header.width) + x;

                    byte[] colors = new byte[3];
                    for (int b = 0; b < 3; b++)
                    {
                        colors[b] = fileBytes[currentPixelnumber + b + headerOffset];
                    }

                    PixelInfo currentPixel = new PixelInfo(colors[0], colors[1], colors[2]);

                    //this pixel does allready exists add it to the list

                    if(colorIsDefined(currentPixel)!=255)
                    {
                        patternPixels[y].Add(colorIsDefined(currentPixel));
                    }
                    else //a color we has not read/discovered yet
                    {
                        if (patternValues.Count < 4) //are there room to define new colors?
                        {
                            Console.WriteLine("found new color: " + currentPixel.ToString());
                            patternPixels[y].Add((byte)(patternValues.Count - 1));        
                            patternValues.Add(currentPixel);//add a new color to our "Palette" or more precisely possible pattern values
                        }
                        else //no there is not
                        {
                             //Console.WriteLine("tired to define to full list: " + currentPixel.ToString());
                             /*Console.WriteLine("experimental: at coord " + i / header.width + "x" + i % header.width);
                             throw new Exception("More than 4 colors used in image");*/
                        }
                    }
                }
                
            }
            //Console.WriteLine("last pixel " + currentPixel.ToString());
           // Console.WriteLine("number of pixels"+header.width * header.height+"calc lines"+test);
            Console.WriteLine(patternPixels);
            Console.WriteLine(patternValues);
        }
    }
}
