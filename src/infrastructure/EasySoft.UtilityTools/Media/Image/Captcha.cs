using System;
using System.Drawing;
using System.Globalization;
using EasySoft.UtilityTools.Assists;

namespace EasySoft.UtilityTools.Media.Image
{
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

        /// <summary>
        /// 图片弯曲配置
        /// </summary>
        public bool Bending { get; set; }

        private readonly char[] _chars = "0123456789QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm".ToCharArray();
        private readonly char[] _numberchars = "0123456789".ToCharArray();

        private readonly string[] _fonts =
        {
            "Arial", "Georgia"
        };

        /// <summary>  
        /// 产生波形滤镜效果  
        /// </summary>  
        //private const double Pi = 3.1415926535897932384626433832795;
        private const double Pi2 = 6.283185307179586476925286766559;

        /// <summary>
        /// 
        /// </summary>
        public Captcha()
        {
            LetterCount = 5;
            LetterWidth = 15;
            LetterHeight = 27;
            Bending = false;
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
        /// 获取验证码图片
        /// </summary>
        /// <param name="checkCode">验证码文本</param>
        /// <returns></returns>
        public Bitmap GetImage(out string checkCode)
        {
            checkCode = GetRandomNumberString();
            var intImageWidth = checkCode.Length * LetterWidth + 5;
            var newRandom = new Random();
            var image = new Bitmap(intImageWidth, LetterHeight);
            var g = Graphics.FromImage(image);

            //生成随机生成器  
            var random = new Random();

            //白色背景  
            g.Clear(Color.White);

            //画图片的背景噪音线  
            for (var i = 0; i < 10; i++)
            {
                var x1 = random.Next(image.Width);
                var x2 = random.Next(image.Width);
                var y1 = random.Next(image.Height);
                var y2 = random.Next(image.Height);

                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }

            //画图片的前景噪音点  
            for (var i = 0; i < 10; i++)
            {
                var x = random.Next(image.Width);
                var y = random.Next(image.Height);

                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }

            //随机字体和颜色的验证码字符  
            for (var intIndex = 0; intIndex < checkCode.Length; intIndex++)
            {
                var fontIndex = newRandom.Next(_fonts.Length - 1);
                var strChar = checkCode.Substring(intIndex, 1);
                var newBrush = new SolidBrush(ColorAssist.GetRandomColor());
                var thePos = new Point(
                    intIndex * LetterWidth + 1 + newRandom.Next(3),
                    1 + newRandom.Next(3)
                ); //5+1+a+s+p+x

                g.DrawString(strChar, new Font(_fonts[fontIndex], 14, FontStyle.Bold), newBrush, thePos);
            }

            //灰色边框  
            g.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, intImageWidth - 1, (LetterHeight - 1));

            //图片扭曲  
            if (Bending)
            {
                image = TwistImage(image, true, 3, 4);
            }

            return image;
        }

        /// <summary>  
        /// 正弦曲线Wave扭曲图片  
        /// </summary>  
        /// <param name="srcBmp">图片路径</param>  
        /// <param name="bXDir">如果扭曲则选择为True</param>  
        /// <param name="dMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>  
        /// <returns></returns>  
        public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            var destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色  
            var graph = Graphics.FromImage(destBmp);

            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);

            graph.Dispose();

            var dBaseAxisLen = bXDir ? destBmp.Height : (double)destBmp.Width;

            for (var i = 0; i < destBmp.Width; i++)
            {
                for (var j = 0; j < destBmp.Height; j++)
                {
                    var dx = bXDir ? (Pi2 * j) / dBaseAxisLen : (Pi2 * i) / dBaseAxisLen;

                    dx += dPhase;

                    var dy = Math.Sin(dx);

                    // 取得当前点的颜色  
                    var nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    var nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    var color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
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
                validateCode += _numberchars[random.Next(0, _numberchars.Length)]
                    .ToString(CultureInfo.InvariantCulture);
            }

            return validateCode;
        }

        #endregion
    }
}