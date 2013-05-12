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
        public List<byte> patternPixels;
        public TgaFile(String tgaPath)
        {
            this.tgaPath = tgaPath;
            patternPixels = new List<byte>();
            patternValues = new List<PixelInfo>(4);

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
            byte[] fileBytes = File.ReadAllBytes(tgaPath);
            PixelInfo currentPixel = new PixelInfo(1, 3, 1);
            for (int i = headerOffset; i < (header.width * header.height)*3+headerOffset; i+=3)
            {
                currentPixel = new PixelInfo(fileBytes[i+2],fileBytes[i+1],fileBytes[i]);
                
                if (patternValues.Exists(x => x == currentPixel))
                {
                    patternPixels.Add((byte)patternValues.FindIndex(x => x == currentPixel));
                }
                else
                {
                    Console.WriteLine("found new color: " + currentPixel.ToString());
                    if (patternValues.Count < 4)
                    {
                        patternPixels.Add((byte)(patternValues.Count - 1));
                        patternValues.Add(currentPixel);
                    }
                    else
                    {
                       /* Console.WriteLine("tired to define to full list: " + currentPixel.ToString());
                        Console.WriteLine("experimental: at coord " + i / header.width + "x" + i % header.width);
                        throw new Exception("More than 4 colors used in image");*/
                    }
                }
            }
            Console.WriteLine("last pixel " + currentPixel.ToString());
            Console.WriteLine("number of pixels"+header.width * header.height);
            Console.WriteLine(patternPixels);
            Console.WriteLine(patternValues);
        }
    }
}
