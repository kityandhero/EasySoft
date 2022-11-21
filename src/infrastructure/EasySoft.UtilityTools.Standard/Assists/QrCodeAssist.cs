using QRCoder;

namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// QrCodeAssist
/// </summary>
public static class QrCodeAssist
{
    /// <summary>
    /// GetRCode
    /// </summary>
    /// <param name="text"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static string GetRCode(string text, int size = 4)
    {
        var sizeAdjust = size;

        if (sizeAdjust > 10) sizeAdjust = 4;

        var bytesQrCode = PngByteQRCodeHelper.GetQRCode(text, QRCodeGenerator.ECCLevel.M, sizeAdjust);

        using var ms = new MemoryStream(bytesQrCode);
        using var image = Image.Load(ms);

        // var g = Graphics.FromImage(image);  
        //
        // g.Clear(Color.White); //背景设为白色  
        //
        // var bitmap = new Bitmap(520, 520, g);

        //保存为PNG到内存流
        image.SaveAsPng(ms);

        //输出二维码图片
        var bytes = ms.ToArray();

        ms.Flush();

        return $"data:image/png;base64,{Convert.ToBase64String(bytes)}";
    }
}