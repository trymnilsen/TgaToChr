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
        private tgaHeader header;
        private byte[] imageData;
        private string tgaPath;
        public tgaHeader Header { get { return header; } }
        
        public TgaFile(String tgaPath)
        {
            this.tgaPath = tgaPath;
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

            for (int i = 0; i < header.width * header.height; i++)
            {

            }
        }
    }
}
