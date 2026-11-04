using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;
using SixLabors.ImageSharp.Drawing;
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
    /// 设置验证码位数
    /// </summary>
    /// <param name="letterCount"></param>
    /// <returns></returns>
    public Captcha SetLetterCount(int letterCount)
    {
        if (letterCount < 4 || letterCount > 8)
        {
            throw new UnknownException("letterCount must greater than 3 or less than 9");
        }

        LetterCount = letterCount;

        return this;
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
        {
            validateCode += _chars[random.Next(0, _chars.Length)].ToString(CultureInfo.InvariantCulture);
        }

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
        var font = SystemFonts.CreateFont(
            SystemFonts.Families.First().Name,
            20,
            FontStyle.Bold
        );

        image.Mutate(ctx =>
            {
                ctx.Paint(canvas =>
                    {
                        // ✅ 白底背景：填充整张画布
                        var fullRect = new RectanglePolygon(
                            0,
                            0,
                            imageWidth,
                            imageHeight
                        );

                        canvas.Fill(Brushes.Solid(Color.White), fullRect);

                        // 画验证码文字
                        for (var i = 0; i < code.Length; i++)
                        {
                            var text = code[i].ToString();

                            canvas.DrawText(
                                new RichTextOptions(font)
                                {
                                    Origin = new PointF(20 * i + 10, r.Next(2, 6))
                                },
                                text,
                                Brushes.Solid(ColorAssist.GetFontRandomColor()),
                                null
                            );
                        }

                        // 画干扰线
                        for (var i = 0; i < 10; i++)
                        {
                            var pen = Pens.Solid(ColorAssist.GetRandomColorRgba(), 1f);
                            var p1 = new PointF(r.Next(imageWidth), r.Next(imageHeight));
                            var p2 = new PointF(r.Next(imageWidth), r.Next(imageHeight));

                            canvas.DrawLine(
                                pen,
                                p1,
                                p2
                            );
                        }

                        // 画噪点
                        for (var i = 0; i < 80; i++)
                        {
                            var pen = Pens.Solid(ColorAssist.GetRandomColorRgba(), 1f);
                            var p1 = new PointF(r.Next(imageWidth), r.Next(imageHeight));
                            var p2 = new PointF(p1.X + 1f, p1.Y + 1f);
                            canvas.DrawLine(
                                pen,
                                p1,
                                p2
                            );
                        }
                    }
                );
            }
        );

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
        {
            validateCode += _numberChars[random.Next(0, _numberChars.Length)]
                .ToString(CultureInfo.InvariantCulture);
        }

        return validateCode;
    }

    #endregion
}