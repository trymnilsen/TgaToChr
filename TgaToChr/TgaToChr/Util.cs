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
    }
}
