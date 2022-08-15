using System;
using System.Drawing;

namespace EasySoft.UtilityTools.Assists
{
    public static class ColorAssist
    {
        /// <summary>
        /// 获取随机颜色
        /// </summary>
        /// <returns></returns>
        public static Color GetRandomColor()
        {
            var randomNumFirst = new Random((int)DateTime.Now.Ticks);

            System.Threading.Thread.Sleep(randomNumFirst.Next(50));

            var randomNumSecond = new Random((int)DateTime.Now.Ticks);
            var intRed = randomNumFirst.Next(210);
            var intGreen = randomNumSecond.Next(180);
            var intBlue = (intRed + intGreen > 300) ? 0 : 400 - intRed - intGreen;

            intBlue = (intBlue > 255) ? 255 : intBlue;

            return Color.FromArgb(intRed, intGreen, intBlue);
        }
    }
}