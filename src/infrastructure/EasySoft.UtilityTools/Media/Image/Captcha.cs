using System;
using System.Collections.Generic;
using System.Globalization;
using EasySoft.UtilityTools.Assists;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace EasySoft.UtilityTools.Media.Image
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class Captcha
    {
        public List<SKColor> Colors { get; set; }

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

        /// <summary>
        /// 图片弯曲配置
        /// </summary>
        public bool Bending { get; set; }

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
            Bending = false;

            Colors = new List<SKColor>
            {
            };
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
        /// 创建画笔
        /// </summary>
        /// <param name="color"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        private SKPaint CreatePaint(SKColor color, float fontSize)
        {
            var font = SKTypeface.FromFamilyName(
                null,
                SKFontStyleWeight.SemiBold,
                SKFontStyleWidth.ExtraCondensed,
                SKFontStyleSlant.Upright
            );

            var paint = new SKPaint();

            paint.IsAntialias = true;
            paint.Color = color;
            paint.Typeface = font;
            paint.TextSize = fontSize;

            return paint;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="lineNum">干扰线数量</param>
        /// <param name="lineStrokeWidth">干扰线宽度</param>
        /// <returns></returns>
        public byte[] GetCaptcha(int width, int height, int lineNum = 1, int lineStrokeWidth = 1)
        {
            // https://www.cnblogs.com/catcher1994/p/12897736.html

            var captchaText = GetRandomNumberString();

            var code = GenCode(num);
            var r = new Random();

            using var image = new Image<Rgba32>(Width, Height);

            // 字体
            var font = SystemFonts.CreateFont(SystemFonts.Families.First().Name, 25, FontStyle.Bold);

            image.Mutate(ctx =>
            {
                // 白底背景
                ctx.Fill(Color.White);

                // 画验证码
                for (int i = 0; i < code.Length; i++)
                {
                    ctx.DrawText(code[i].ToString()
                        , font
                        , Colors[r.Next(Colors.Length)]
                        , new PointF(20 * i + 10, r.Next(2, 12)));
                }

                // 画干扰线
                for (int i = 0; i < 10; i++)
                {
                    var pen = new Pen(Colors[r.Next(Colors.Length)], 1);
                    var p1 = new PointF(r.Next(Width), r.Next(Height));
                    var p2 = new PointF(r.Next(Width), r.Next(Height));

                    ctx.DrawLines(pen, p1, p2);
                }

                // 画噪点
                for (int i = 0; i < 80; i++)
                {
                    var pen = new Pen(Colors[r.Next(Colors.Length)], 1);
                    var p1 = new PointF(r.Next(Width), r.Next(Height));
                    var p2 = new PointF(p1.X + 1f, p1.Y + 1f);

                    ctx.DrawLines(pen, p1, p2);
                }
            });

            using var ms = new System.IO.MemoryStream();

            // gif 格式
            image.SaveAsGif(ms);
            return (code, ms.ToArray());
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

        private static string GenCode(int num)
        {
            var code = string.Empty;
            var r = new Random();

            for (int i = 0; i < num; i++)
            {
                code += Chars[r.Next(Chars.Length)].ToString();
            }

            return code;
        }
    }
}