using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgaToChr
{
    struct PixelInfo
    {
        public byte red;
        public byte green;
        public byte blue;
        public PixelInfo(byte r, byte g, byte b)
        {
            red = r;
            green = g;
            blue = b;
        }
        public static bool operator ==(PixelInfo p1, PixelInfo p2)
        {
            if (p1.red == p2.red && p1.green == p2.green && p1.blue == p2.blue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
