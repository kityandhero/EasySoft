using System;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.UtilityTools.Standard.Assists
{
    /// <summary>
    /// 统计辅助类
    /// </summary>
    public class StatisticHelper
    {
        #region 预处理统计时间区间

        public static int PretreatmentRangeMode(
            int rangeMode,
            out DateTime rangeStartTime,
            out int timeCount,
            DateTime startTime,
            DateTime endTime
        )
        {
            var rangeModeAdjust = rangeMode;

            //小时 , 日，月，季，近若干年，今日，本周（以天展示，本月（以天展示），今年（以月展示）
            if (!rangeModeAdjust.In(0, 1, 2, 3, 4))
            {
                rangeModeAdjust = 0;
            }

            rangeStartTime = startTime;
            timeCount = 0;

            //检测时间范围，限制时间间隔，防止发范围数据统计
            if (rangeModeAdjust == 0 && endTime.Subtract(startTime).Hours > 12)
            {
                rangeStartTime = endTime.AddHours(-12);
                timeCount = Convert.ToInt32(endTime.Subtract(startTime).TotalHours);
            }

            if (rangeModeAdjust == 1 && endTime.Subtract(startTime).Days > 30)
            {
                rangeStartTime = endTime.AddDays(-30);
                timeCount = Convert.ToInt32(endTime.Subtract(startTime).TotalDays);
            }

            if (rangeModeAdjust == 2 && endTime.Subtract(startTime).Months() > 12)
            {
                rangeStartTime = endTime.AddMonths(-12);
                timeCount = Convert.ToInt32(endTime.Subtract(startTime).TotalDays / 30);
            }

            if (rangeModeAdjust == 3 && endTime.Subtract(startTime).Months() > 36)
            {
                rangeStartTime = endTime.AddMonths(-36);
                timeCount = Convert.ToInt32(endTime.Subtract(startTime).TotalDays / 90);
            }

            if (rangeModeAdjust == 4 && endTime.Subtract(startTime).Years() > 6)
            {
                rangeStartTime = endTime.AddYears(-6);
                timeCount = Convert.ToInt32(endTime.Subtract(startTime).TotalDays / 365);
            }

            return rangeModeAdjust;
        }

        private static void PretreatmentEveryStatistics(
            int rangeMode,
            out string title,
            out DateTime calculateStartTime,
            out DateTime calculateEndTime,
            DateTime rangeStartTime,
            int timeCount
        )
        {
            title = "";
            calculateStartTime = DateTime.Now;
            calculateEndTime = DateTime.Now;

            switch (rangeMode)
            {
                case 0:
                    title = rangeStartTime.Hour + "点";
                    calculateStartTime = rangeStartTime.SetTime(rangeStartTime.Hour, 0, 0);
                    calculateEndTime = rangeStartTime.SetTime(rangeStartTime.Hour + 1, 0, 0);
                    break;

                case 1:
                    title = timeCount >= 12 ? (rangeStartTime.Day + 1) + "日" : rangeStartTime.ToString("MM-dd");
                    calculateStartTime = rangeStartTime.Date;
                    calculateEndTime = calculateStartTime.AddDays(1).Date;
                    break;

                case 2:
                    title = timeCount >= 12 ? (rangeStartTime.Month + 1) + "月" : rangeStartTime.ToString("yyyy-MM");
                    calculateStartTime = rangeStartTime.AddDays(0 - rangeStartTime.Day + 1).Date;
                    calculateEndTime = calculateStartTime.AddMonths(1).Date;
                    break;

                case 3:
                    title = (rangeStartTime.Month / 3 + 1) + "季度";
                    calculateStartTime =
                        new DateTime(rangeStartTime.Year, rangeStartTime.Month / 3 * 3 + 1, 1, 0, 0, 0);
                    calculateEndTime = calculateStartTime.AddMonths(3).Date;
                    break;

                case 4:
                    title = rangeStartTime.ToString("yyyy");
                    calculateStartTime = new DateTime(rangeStartTime.Year, 1, 1, 0, 0, 0);
                    calculateEndTime = calculateStartTime.AddYears(1).Date;
                    break;
            }
        }

        private static DateTime PretreatmentEveryTimeIncrease(int rangeMode, DateTime timeTemp)
        {
            DateTime result;

            switch (rangeMode)
            {
                case 0:
                    result = timeTemp.AddHours(1);
                    break;

                case 1:
                    result = timeTemp.AddDays(1);
                    break;

                case 2:
                    result = timeTemp.AddMonths(1);
                    break;

                case 3:
                    result = timeTemp.AddMonths(1).AddMonths(1).AddMonths(1);
                    break;

                case 4:
                    result = timeTemp.AddYears(1);
                    break;

                default:
                    result = timeTemp.AddHours(1);
                    break;
            }

            return result;
        }

        #endregion 预处理统计时间区间
    }
}