using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgaToChr
{
    struct tgaHeader
    {
        public byte idLength;
        public byte colorMapType;
        public byte dataTypeCode;

        public short colorMapStart;
        public short colorMapLength;
        public byte colorMapDepth;

        public short x_origin;
        public short y_origin;
        public short width;
        public short height;

        public byte bitsPerPixel;
        public byte descriptor;
    }
}
