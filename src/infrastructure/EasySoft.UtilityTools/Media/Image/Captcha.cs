using System;
using System.Collections.Generic;
using System.Globalization;
using EasySoft.UtilityTools.Assists;
using SkiaSharp;

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
            var captchaText = GetRandomNumberString();

            //创建bitmap位图
            using var image2d = new SKBitmap(width, height, SKColorType.Bgra8888, SKAlphaType.Premul);

            //创建画笔
            using var canvas = new SKCanvas(image2d);

            //填充背景颜色为白色
            canvas.DrawColor(SKColors.White);

            //将文字写到画布上
            using (var drawStyle = CreatePaint(SKColors.Black, height))
            {
                canvas.DrawText(captchaText, 1, height - 1, drawStyle);
            }

            //画随机干扰线
            using (var drawStyle = new SKPaint())
            {
                var random = new Random();

                for (var i = 0; i < lineNum; i++)
                {
                    drawStyle.Color = ColorAssist.GetRandomColor();
                    drawStyle.StrokeWidth = lineStrokeWidth;

                    canvas.DrawLine(
                        random.Next(0, width),
                        random.Next(0, height),
                        random.Next(0, width),
                        random.Next(0, height),
                        drawStyle
                    );
                }
            }

            //返回图片byte
            using (var img = SKImage.FromBitmap(image2d))
            {
                using (var p = img.Encode(SKEncodedImageFormat.Png, 100))
                {
                    return p.ToArray();
                }
            }
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
}