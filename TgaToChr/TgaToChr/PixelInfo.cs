using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgaToChr
{
    struct PixelInfo:IComparable<PixelInfo>
    {
        public byte red;
        public byte green;
        public byte blue;
        public byte GrayScale
        {
            get
            {
                return (byte)((red + green + blue)/3);
            }
        }
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
        public static bool operator !=(PixelInfo p1, PixelInfo p2)
        {
            return !(p1 == p2);
        }
        public override string ToString()
        {
            return "r" + red + "g" + green + "b" + blue;
        }
        public int CompareTo(PixelInfo other)
        {
            if(other==null)
            {
                return -1;
            }
            if(other.GrayScale>GrayScale)
            {
                return 1;
            }
            if(other.GrayScale==GrayScale)
            {
                return 0;
            }
            return -1;
        }
        /// <summary>
        /// The hashing function of this method is purely based on grayscale.. meaning there will! be collision on for example RGB 15-10-10 and 10-10-15
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (int)GrayScale;
        }
        public override bool Equals(object obj)
        {
            return (((PixelInfo)obj).GrayScale == GrayScale);
        }
    }
}
