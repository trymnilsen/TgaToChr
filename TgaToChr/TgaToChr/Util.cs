using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgaToChr
{
    class Util
    {
        public static short littleEndToShort(byte loByte, byte hiByte)
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
        public static byte[] ConsecutiveBitsToOneInEachByte(byte firstByte, byte secondByte)
        {
            throw new NotImplementedException("Not implemented");
            return new byte[2];
        }
    }
}
