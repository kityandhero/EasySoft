using EasySoft.UtilityTools.Standard.Assists;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace EasySoft.UtilityTools.Standard.Media.Image;

/// <summary>
/// 验证码
/// </summary>
public class Captcha
{
    #region 验证码

    /// <summary>
    /// 单个字体的宽度范围，默认15
    /// </summary>
    public int LetterWidth { get; set; }

    /// <summary>
    /// 单个字体的高度范围  ，默认27
    /// </summary>
    public int LetterHeight { get; set; }

    /// <summary>
    /// 验证码位数， 默认5位
    /// </summary>
    public int LetterCount { get; set; }

    private readonly char[] _chars = "0123456789QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm".ToCharArray();
    private readonly char[] _numberChars = "0123456789".ToCharArray();

    /// <summary>
    /// 
    /// </summary>
    public Captcha()
    {
        LetterCount = 5;
        LetterWidth = 15;
        LetterHeight = 27;
    }

    /// <summary>
    /// 生成随机数字字符串  
    /// </summary>
    /// <returns></returns>
    public string GetRandomString()
    {
        var random = new Random();
        var validateCode = string.Empty;

        for (var i = 0; i < LetterCount; i++)
            validateCode += _chars[random.Next(0, _chars.Length)].ToString(CultureInfo.InvariantCulture);

        return validateCode;
    }

    /// <summary>
    /// 获取验证码 https://www.cnblogs.com/catcher1994/p/12897736.html
    /// </summary>
    /// <returns></returns>
    public byte[] GetCaptcha()
    {
        var code = GetRandomNumberString();

        var imageWidth = code.Length * LetterWidth + 5;
        var imageHeight = LetterHeight;

        var r = new Random();

        using var image = new Image<Rgba32>(imageWidth, imageHeight);

        // 字体
        var font = SystemFonts.CreateFont(SystemFonts.Families.First().Name, 20, FontStyle.Bold);

        image.Mutate(ctx =>
        {
            // 白底背景
            ctx.Fill(Color.White);

            // 画验证码
            for (var i = 0; i < code.Length; i++)
                ctx.DrawText(
                    code[i].ToString(),
                    font,
                    ColorAssist.GetFontRandomColor(),
                    new PointF(20 * i + 10, r.Next(2, 6))
                );

            // 画干扰线
            for (var i = 0; i < 10; i++)
            {
                var pen = new Pen(ColorAssist.GetRandomColorRgba(), 1);
                var p1 = new PointF(r.Next(imageWidth), r.Next(imageHeight));
                var p2 = new PointF(r.Next(imageWidth), r.Next(imageHeight));

                ctx.DrawLines(pen, p1, p2);
            }

            // 画噪点
            for (var i = 0; i < 80; i++)
            {
                var pen = new Pen(ColorAssist.GetRandomColorRgba(), 1);
                var p1 = new PointF(r.Next(imageWidth), r.Next(imageHeight));
                var p2 = new PointF(p1.X + 1f, p1.Y + 1f);

                ctx.DrawLines(pen, p1, p2);
            }
        });

        using var ms = new MemoryStream();

        // gif 格式
        image.SaveAsGif(ms);

        return ms.ToArray();
    }

    /// <summary>
    /// 生成随机数字字符串  
    /// </summary>
    /// <returns></returns>
    public string GetRandomNumberString()
    {
        var random = new Random();
        var validateCode = string.Empty;

        for (var i = 0; i < LetterCount; i++)
            validateCode += _numberChars[random.Next(0, _numberChars.Length)]
                .ToString(CultureInfo.InvariantCulture);

        return validateCode;
    }

    #endregion
}