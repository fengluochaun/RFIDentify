using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDentify.Com
{
    public class ImgUtil
    {
        public static byte[] ImageToByte(Image Picture)
        {
            MemoryStream ms = new MemoryStream();
            if (Picture == null)
                return new byte[ms.Length];
            Picture.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] BPicture = new byte[ms.Length];
            BPicture = ms.GetBuffer();
            return BPicture;
        }

        //二进制流转为图片方法
        public static Image Byte2Image(object value)
        {
            byte[] picData = (byte[])value;
            MemoryStream ms = new MemoryStream(picData);
            Image img = Image.FromStream(ms);
            ms.Close();
            return img;
        }

        //将文件读取转换为二进制流文件
        public static byte[] GetFileBytes(string Filename)
        {

            if (Filename == "")
                return null;
            try
            {
                FileStream fileStream = new FileStream(Filename, FileMode.Open, FileAccess.Read);
                BinaryReader binaryReader = new BinaryReader(fileStream);
                byte[] fileBytes = binaryReader.ReadBytes((int)fileStream.Length);
                binaryReader.Close();
                return fileBytes;
            }
            catch
            {
                return null;
            }
        }
    }
}
