using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TgaToChr
{
    class ChrEncoder
    {
        public ChrEncoder()
        {

        }
        public byte[] EncodeImageMap(PixelInfo[,] map)
        {
            //We need to traverse each 8x8 square of the map, we also only care about the pixels inside this tile to be unique
            // O(n^4) shit just got real yo!

            //iterate through all the tiles
            List<Byte> byteValues = new List<byte>();
            for(int ty=0; ty<16; ty++)
            {
                for(int tx=0; tx<16; tx++)
                {
                    //Tile Scope here
                    List<PixelInfo> uniqueColors = new List<PixelInfo>();
                    byte[,] PatternTable = new byte[8,8];
                    //Iterate on all pixes in one tile
                    for(int py=0; py<8; py++)
                    {
                        //Line scope
                        for(int px=0; px<8; px++)
                        {
                            PixelInfo currentPixel = map[tx * 8 + px, ty * 8 + py];
                            if(!uniqueColors.Any(p=>p==currentPixel))
                            {
                                if (uniqueColors.Count >= 4)
                                    throw new FormatException("More than 4 colors defined in image at tile{" + tx + "/" + ty + "}");

                                uniqueColors.Add(currentPixel);
                            }
                            PatternTable[px,py] = (byte)(uniqueColors.FindIndex(p => p == currentPixel));
                        }
                        byteValues.AddRange(createEncodedPatternTable(Util.getFromLine(PatternTable,py)));

                        //Linescope end
                    }
                    //Processing on tileAfterwards here
                }
            }

            return byteValues.ToArray();
        }
        private byte [] createEncodedPatternTable(byte[] bytes)
        {
            // set byte 1(only has the lsb 1 or 0) as bit 7 in first byte 
            // set byte 2 as bit 7 in first byte 
            // set byte 3(only has the lsb 1 or 0) as bit 6 in first byte 
            // set byte 4 as bit 6 in first byte 
            byte[] returnBytes = new byte[2];
            for (int i = 0; i < 8; i++)
            {
                if (bytes[i]==1 || bytes[i]==3)
                {
                    returnBytes[0] = Util.SetBit(7 - i, returnBytes[0]);
                }
                if (bytes[i] == 2 || bytes[i]==3)
                {
                    returnBytes[1] = Util.SetBit(7 - i, returnBytes[1]);
                }

            }
            return returnBytes;
        }
        
    }
}
