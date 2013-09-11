using System;
using System.Collections.Generic;
using System.IO;
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
            Dictionary<PixelInfo, int> knownColors = new Dictionary<PixelInfo, int>();
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
                                if (uniqueColors.Count >= 4)
                                    throw new FormatException("More than 4 colors defined in image at tile{" + tx + "/" + ty + "}");
                                //do we know this color/has it been used in another tile? if so 
                                //give it the same pattern value
                                if (knownColors.ContainsKey(currentPixel))
                                {
                                    uniqueColors.Add(currentPixel);
                                }
                                else
                                {
                                    if(knownColors.Count<4)
                                    {
                                        
                                    }
                                    uniqueColors.Add();
                                }
                            PatternTable[px,py] = (byte)(uniqueColors.FindIndex(p => p == currentPixel));
                        }
                        //byteValues.AddRange(createEncodedPatternTable(Util.getFromLine(PatternTable,py)));

                        //Linescope end
                    }
                    //This part was written quickly in need of sleep it is indeed stupid to do two loops like this
                    //create tile with first two pattern bits
                    //then creat tile with second two patternbits
                    for (int py2 = 0; py2 < 8; py2++)
                    {
                        byteValues.Add(createEncodedPatternTable(Util.getFromLine(PatternTable, py2),0));
                    }
                    for (int py2 = 0; py2 < 8; py2++)
                    {
                        byteValues.Add(createEncodedPatternTable(Util.getFromLine(PatternTable, py2),1));
                    }
                    Console.WriteLine("Writing first tile in encoded format for debug to tile.txt");
                    FileStream fs2 = File.Create("out/binTile"+tx+"-"+ty+".txt", 2048);
                    BinaryWriter bw2 = new BinaryWriter(fs2);
                    for (int py2 = 0; py2 < 8; py2++)
                    {
                        bw2.Write(createEncodedPatternTable(Util.getFromLine(PatternTable, py2),0));
                    }
                    for (int py2 = 0; py2 < 8; py2++)
                    {
                        bw2.Write(createEncodedPatternTable(Util.getFromLine(PatternTable, py2),1));
                    }
                    bw2.Close();
                    fs2.Close();
                    Console.WriteLine("Yay sucessfully written");

                    Console.WriteLine("Writing first tile pattern table for debug to tile.txt");
                    FileStream fs = File.Create("out/tile" + tx + "-" + ty + ".txt", 2048);
                    StreamWriter bw = new StreamWriter(fs);
                    StringBuilder sb = new StringBuilder();
                    for (int py = 0; py < 8; py++)
                    {
                        //Line scope
                        for (int px = 0; px < 8; px++)
                        {
                            sb.Append(Convert.ToString(PatternTable[px, py], 16) + " ");
                        }
                        sb.AppendLine();
                    }
                    bw.Write(sb.ToString());
                    bw.Close();
                    fs.Close();
                    //Processing on tileAfterwards here
                }
            }

            return byteValues.ToArray();
        }
        private byte createEncodedPatternTable(byte[] bytes, int tilenr)
        {
            // set byte 1(only has the lsb 1 or 0) as bit 7 in first byte 
            // set byte 2 as bit 7 in first byte 
            // set byte 3(only has the lsb 1 or 0) as bit 6 in first byte 
            // set byte 4 as bit 6 in first byte 
            byte returnByte = 0;
            for (int i = 0; i < 8; i++)
            {
                if (bytes[i]==1+tilenr || bytes[i]==3)
                {
                    returnByte = Util.SetBit(7 - i, returnByte);
                }

            }
            return returnByte;
        }
        
    }
}
