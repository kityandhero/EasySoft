using QRCoder;
using System;
using System.Drawing;
using System.IO;

namespace UtilityTools.Assists
{
    public static class QrCodeAssist
    {
        public static string GetRCode(string text, int size = 4)
        {
            var sizeAdjust = size;

            if (sizeAdjust > 10)
            {
                sizeAdjust = 4;
            }

            var bytesQrCode = PngByteQRCodeHelper.GetQRCode(text, QRCodeGenerator.ECCLevel.M, sizeAdjust);

            using (var ms = new MemoryStream(bytesQrCode))
            {
                var image = Image.FromStream(ms);

                // var g = Graphics.FromImage(image);
                //
                // g.Clear(Color.White); //背景设为白色  
                //
                // var bitmap = new Bitmap(520, 520, g);

                //保存为PNG到内存流
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                //输出二维码图片
                var bytes = ms.ToArray();

                ms.Flush();

                return $"data:image/png;base64,{Convert.ToBase64String(bytes)}";
            }
        }
    }
}