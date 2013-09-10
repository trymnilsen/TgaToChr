using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgaToChr
{
    class Util
    {
        public static short LittleEndToShort(byte loByte, byte hiByte)
        {
            short outputShort;
            short highByte=hiByte;
            outputShort=loByte;
            outputShort<<=8;
            outputShort|=highByte;
            return outputShort;
        }
        /// <summary>
        /// Converts to nes format, see comments for help
        /// </summary>
        /// <param name="firstByte">The first byte of the tile line</param>
        /// <param name="secondByte">The second byte of the tile line</param>
        /// <returns>A byte array containing two bytes, in the correct formatting</returns>
        /// <remarks>The first bit in the first byte and the first bit in the second byte is 
        /// one pixel, the second bit in the byte and the second bit in the second byte is the second pixel.. etc</remarks>
        /// <example>Here each letter represents a bit, but to show the switch they have
        /// names/letters, this is not hexnotation. Suppose we have the two bytes ABCDEFGH and 
        /// IJKLMNOP, this will return ACEGIKMO BDFHJLNP</example>
        public static byte[] BitpairToBytes(byte firstByte, byte secondByte)
        {
            byte[] returnVal = new byte[2];
            //First byte
            for(int i=0; i<8; i++)
            {
                if(isBitSet(i,firstByte))
                {
                    returnVal[0]=SetBit(i, returnVal[0]);
                }
                if(isBitSet(i,secondByte))
                {
                    returnVal[1]=SetBit(i, returnVal[1]);
                }
            }
            return returnVal;
        }
        /// <summary>
        /// Sets a bit in a byte
        /// </summary>
        /// <param name="nr">The bit nr, from the LSB</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte SetBit(int nr, byte data)
        {
            if(nr>7)
            {
                throw new ArgumentException("Nr of the the bit to set cannot be grater than that of a byte");
            }
            return (byte)(data | 1<<nr);
        }
        /// <summary>
        /// Checks if a bit is set in a byte
        /// </summary>
        /// <param name="nr">the bit nr</param>
        /// <param name="data">byte to check</param>
        /// <returns>true or false</returns>
        public static bool isBitSet(int nr, byte data)
        {
            if (nr > 7)
            {
                throw new ArgumentException("Nr of the the bit to set cannot be grater than that of a byte");
            } 
            return ((data & (1 << nr)) != 0);
        }
        /// <summary>
        /// Combines single bytes to consecutive bits in a byte
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        /// Bug here! not setting the setting th returnvalue of set bit to something
        public static byte[] singleBytesToCombined(byte[] bytes)
        {
            byte[] returnVal = new byte[2];
            int bitNrToSet = 0;
            for(int i=0; i<8; i++)
            {
                if(i / 4 > 0)
                {
                    bitNrToSet = 1;
                }
                if(isBitSet(0,bytes[i]))
                {
                    returnVal[bitNrToSet]=SetBit((i * 2) % 8, i / 4 > 0 ? returnVal[1] : returnVal[0]);
                }
                if (isBitSet(1, bytes[i]))
                {
                    returnVal[bitNrToSet]=SetBit(((i * 2) % 8)+1, i / 4 > 0 ? returnVal[1] : returnVal[0]);
                }
            }
            return returnVal;
        }
        public static byte[] singleBytesToNesFormat(byte[] bytes)
        {
            byte[] bytesCombined = singleBytesToCombined(bytes);
            return BitpairToBytes(bytesCombined[0], bytesCombined[1]);
        }
        public static byte [] getFromLine(byte[,] bytes, int line)
        {

            if (line > bytes.GetLength(0))
                throw new ArgumentException("Requested dimention larger than array rank");
            byte[] returnArray = new byte[bytes.GetLength(0)];
            for(int i=0; i<bytes.GetLength(0); i++)
            {
                returnArray[i] = bytes[i,line];
            }
            return returnArray;
        }
    }
}
