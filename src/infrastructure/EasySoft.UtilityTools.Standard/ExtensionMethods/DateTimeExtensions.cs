using System;
using System.Globalization;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods
{
    public static class DateTimeExtensions
    {
        public static string ToYearMonthDayHourMinuteSecond(this DateTime source)
        {
            return source.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ToYearMonthDayHourMinute(this DateTime source)
        {
            return source.ToString("yyyy-MM-dd HH:mm");
        }

        public static string ToYearMonthDay(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        #region BeginningOf

        /// <summary>
        /// Beginning of a specific time frame
        /// </summary>
        /// <param name="date">Date to base off of</param>
        /// <param name="timeFrame">Time frame to use</param>
        /// <param name="culture">Culture to use for calculating (defaults to the current culture)</param>
        /// <returns>The beginning of a specific time frame</returns>
        public static DateTime BeginningOf(this DateTime date, TimeFrame timeFrame, CultureInfo? culture = null)
        {
            var cultureAdjust = culture ?? CultureInfo.CurrentCulture;

            if (timeFrame == TimeFrame.Week)
            {
                return date.AddDays(cultureAdjust.DateTimeFormat.FirstDayOfWeek - date.DayOfWeek).Date;
            }

            if (timeFrame == TimeFrame.Month)
            {
                return new DateTime(date.Year, date.Month, 1);
            }

            if (timeFrame == TimeFrame.Quarter)
            {
                return date.BeginningOf(
                    TimeFrame.Quarter,
                    date.BeginningOf(TimeFrame.Year, cultureAdjust),
                    cultureAdjust
                );
            }

            return new DateTime(date.Year, 1, 1);
        }

        /// <summary>
        /// Beginning of a specific time frame
        /// </summary>
        /// <param name="date">Date to base off of</param>
        /// <param name="timeFrame">Time frame to use</param>
        /// <param name="culture">Culture to use for calculating (defaults to the current culture)</param>
        /// <param name="startOfQuarter1">Start of the first quarter</param>
        /// <returns>The beginning of a specific time frame</returns>
        public static DateTime BeginningOf(
            this DateTime date,
            TimeFrame timeFrame,
            DateTime startOfQuarter1,
            CultureInfo? culture = null
        )
        {
            var cultureAdjust = culture ?? CultureInfo.CurrentCulture;

            if (timeFrame != TimeFrame.Quarter)
            {
                return date.BeginningOf(timeFrame, cultureAdjust);
            }

            if (date.Between(
                    startOfQuarter1,
                    startOfQuarter1.AddMonths(3).AddDays(-1).EndOf(TimeFrame.Day, CultureInfo.CurrentCulture)
                ))
            {
                return startOfQuarter1.Date;
            }

            if (date.Between(startOfQuarter1.AddMonths(3),
                    startOfQuarter1.AddMonths(6).AddDays(-1).EndOf(TimeFrame.Day, CultureInfo.CurrentCulture)))
            {
                return startOfQuarter1.AddMonths(3).Date;
            }

            if (date.Between(startOfQuarter1.AddMonths(6),
                    startOfQuarter1.AddMonths(9).AddDays(-1).EndOf(TimeFrame.Day, CultureInfo.CurrentCulture)))
            {
                return startOfQuarter1.AddMonths(6).Date;
            }

            return startOfQuarter1.AddMonths(9).Date;
        }

        #endregion

        #region UNIX时间戳

        /// <summary>
        /// 将时间转换成UNIX时间戳
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns>UNIX时间戳</returns>
        public static long ToUnixTime(this DateTime dt)
        {
            return ConvertAssist.ConvertToUnixTime(dt);
        }

        #endregion

        #region As

        /// <summary>
        /// Ensures that local times are converted to UTC times.  Unspecified kinds are recast to UTC with no conversion.
        /// </summary>
        /// <param name="source">The date-time to convert.</param>
        /// <returns>The date-time in UTC time.</returns>
        public static DateTime AsUtc(this DateTime source)
        {
            return source.Kind == DateTimeKind.Unspecified
                ? new DateTime(source.Ticks, DateTimeKind.Utc)
                : source.ToUniversalTime();
        }

        #endregion

        #region Get

        /// <summary>
        /// 返回当前所在月份的第一天和最后一天
        /// </summary>
        /// <param name="date">当前日期</param>
        /// <param name="firstDay"></param>
        /// <param name="lastDay"></param>
        public static void GetFirstAndLastDay(this DateTime date, out DateTime firstDay, out DateTime lastDay)
        {
            GetFirstAndLastDay(out firstDay, out lastDay, date);
        }

        /// <summary>
        /// 返回指定月的第一天和最后一天
        /// </summary>
        /// <param name="firstDay"></param>
        /// <param name="lastDay"></param>
        /// <param name="date"></param>
        public static void GetFirstAndLastDay(
            out DateTime firstDay,
            out DateTime lastDay,
            DateTime date = default
        )
        {
            var month = date.Month;
            var year = date.Year;

            if (month != 12)
            {
                month = month % 12;
            }

            string first;
            string last;

            switch (month)
            {
                case 1:
                    first = date.ToString(year + "-0" + month + "-01");
                    last = date.ToString(year + "-0" + month + "-31");
                    break;
                case 2:
                    first = date.ToString(year + "-0" + month + "-01");
                    last = DateTime.IsLeapYear(date.Year)
                        ? date.ToString(year + "-0" + month + "-29")
                        : date.ToString(year + "-0" + month + "-28");
                    break;
                case 3:
                    first = date.ToString(year + "-0" + month + "-01");
                    last = date.ToString("yyyy-0" + month + "-31");
                    break;
                case 4:
                    first = date.ToString(year + "-0" + month + "-01");
                    last = date.ToString(year + "-0" + month + "-30");
                    break;
                case 5:
                    first = date.ToString(year + "-0" + month + "-01");
                    last = date.ToString(year + "-0" + month + "-31");
                    break;
                case 6:
                    first = date.ToString(year + "-0" + month + "-01");
                    last = date.ToString(year + "-0" + month + "-30");
                    break;
                case 7:
                    first = date.ToString(year + "-0" + month + "-01");
                    last = date.ToString(year + "-0" + month + "-31");
                    break;
                case 8:
                    first = date.ToString(year + "-0" + month + "-01");
                    last = date.ToString(year + "-0" + month + "-31");
                    break;
                case 9:
                    first = date.ToString(year + "-0" + month + "-01");
                    last = date.ToString(year + "-0" + month + "-30");
                    break;
                case 10:
                    first = date.ToString(year + "-" + month + "-01");
                    last = date.ToString(year + "-" + month + "-31");
                    break;
                case 11:
                    first = date.ToString(year + "-" + month + "-01");
                    last = date.ToString(year + "-" + month + "-30");
                    break;
                default:
                    first = date.ToString(year + "-" + month + "-01");
                    last = date.ToString(year + "-" + month + "-31");
                    break;
            }

            firstDay = Convert.ToDateTime(first);
            lastDay = Convert.ToDateTime(last);
        }

        #endregion

        #region AddWeeks

        /// <summary>
        /// Adds the number of weeks to the date
        /// </summary>
        /// <param name="date">Date input</param>
        /// <param name="numberOfWeeks">Number of weeks to add</param>
        /// <returns>The date after the number of weeks are added</returns>
        public static DateTime AddWeeks(this DateTime date, int numberOfWeeks)
        {
            return date.AddDays(numberOfWeeks * 7);
        }

        #endregion

        #region Age

        /// <summary>
        /// Calculates age based on date supplied
        /// </summary>
        /// <param name="date">Birth date</param>
        /// <param name="calculateFrom">Date to calculate from</param>
        /// <returns>The total age in years</returns>
        public static int Age(this DateTime date, DateTime calculateFrom = default)
        {
            if (calculateFrom == default)
            {
                calculateFrom = DateTime.Now;
            }

            return (calculateFrom - date).Years();
        }

        #endregion

        #region DaysIn

        /// <summary>
        /// Gets the number of days in the time frame specified based on the date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="timeFrame">Time frame to calculate the number of days from</param>
        /// <param name="culture">Culture to use for calculating (defaults to the current culture)</param>
        /// <returns>The number of days in the time frame</returns>
        public static int DaysIn(this DateTime date, TimeFrame timeFrame, CultureInfo? culture = null)
        {
            var cultureAdjust = culture ?? CultureInfo.CurrentCulture;

            if (cultureAdjust == null)
            {
                throw new Exception("culture is null");
            }

            if (timeFrame == TimeFrame.Day)
            {
                return 1;
            }

            if (timeFrame == TimeFrame.Week)
            {
                return 7;
            }

            if (timeFrame == TimeFrame.Month)
            {
                return cultureAdjust.Calendar.GetDaysInMonth(date.Year, date.Month);
            }

            if (timeFrame == TimeFrame.Quarter)
            {
                return date.EndOf(TimeFrame.Quarter, cultureAdjust).DayOfYear -
                       date.BeginningOf(TimeFrame.Quarter, cultureAdjust).DayOfYear;
            }

            return cultureAdjust.Calendar.GetDaysInYear(date.Year);
        }

        /// <summary>
        /// Gets the number of days in the time frame specified based on the date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="timeFrame">Time frame to calculate the number of days from</param>
        /// <param name="culture">Culture to use for calculating (defaults to the current culture)</param>
        /// <param name="startOfQuarter1">Start of the first quarter</param>
        /// <returns>The number of days in the time frame</returns>
        public static int DaysIn(
            this DateTime date,
            TimeFrame timeFrame,
            DateTime startOfQuarter1,
            CultureInfo? culture = null
        )
        {
            var cultureAdjust = culture ?? CultureInfo.CurrentCulture;

            if (timeFrame != TimeFrame.Quarter)
            {
                date.DaysIn(timeFrame, cultureAdjust);
            }

            return date.EndOf(TimeFrame.Quarter, cultureAdjust).DayOfYear - startOfQuarter1.DayOfYear;
        }

        #endregion

        #region DaysLeftIn

        /// <summary>
        /// Gets the number of days left in the time frame specified based on the date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="timeFrame">Time frame to calculate the number of days left</param>
        /// <param name="culture">Culture to use for calculating (defaults to the current culture)</param>
        /// <returns>The number of days left in the time frame</returns>
        public static int DaysLeftIn(this DateTime date, TimeFrame timeFrame, CultureInfo? culture = null)
        {
            var cultureAdjust = culture ?? CultureInfo.CurrentCulture;

            if (cultureAdjust == null)
            {
                throw new Exception("culture is null");
            }

            if (timeFrame == TimeFrame.Day)
            {
                return 1;
            }

            if (timeFrame == TimeFrame.Week)
            {
                return 7 - ((int)date.DayOfWeek + 1);
            }

            if (timeFrame == TimeFrame.Month)
            {
                return date.DaysIn(TimeFrame.Month, cultureAdjust) - date.Day;
            }

            if (timeFrame == TimeFrame.Quarter)
            {
                return date.DaysIn(TimeFrame.Quarter, cultureAdjust) -
                       (date.DayOfYear - date.BeginningOf(TimeFrame.Quarter, cultureAdjust).DayOfYear);
            }

            return date.DaysIn(TimeFrame.Year, cultureAdjust) - date.DayOfYear;
        }

        /// <summary>
        /// Gets the number of days left in the time frame specified based on the date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="timeFrame">Time frame to calculate the number of days left</param>
        /// <param name="culture">Culture to use for calculating (defaults to the current culture)</param>
        /// <param name="startOfQuarter1">Start of the first quarter</param>
        /// <returns>The number of days left in the time frame</returns>
        public static int DaysLeftIn(
            this DateTime date,
            TimeFrame timeFrame,
            DateTime startOfQuarter1,
            CultureInfo? culture = null
        )
        {
            var cultureAdjust = culture ?? CultureInfo.CurrentCulture;

            if (cultureAdjust == null)
            {
                throw new Exception("culture is null");
            }

            if (timeFrame != TimeFrame.Quarter)
            {
                return date.DaysLeftIn(timeFrame, cultureAdjust);
            }

            return date.DaysIn(TimeFrame.Quarter, startOfQuarter1, cultureAdjust) -
                   (date.DayOfYear - startOfQuarter1.DayOfYear);
        }

        #endregion

        #region EndOf

        /// <summary>
        /// End of a specific time frame
        /// </summary>
        /// <param name="date">Date to base off of</param>
        /// <param name="timeFrame">Time frame to use</param>
        /// <param name="culture">Culture to use for calculating (defaults to the current culture)</param>
        /// <returns>The end of a specific time frame (TimeFrame.Day is the only one that sets the time to 12:59:59 PM, all else are the beginning of the day)</returns>
        public static DateTime EndOf(this DateTime date, TimeFrame timeFrame, CultureInfo? culture = null)
        {
            var cultureAdjust = culture ?? CultureInfo.CurrentCulture;

            if (cultureAdjust == null)
            {
                throw new Exception("culture is null");
            }

            if (timeFrame == TimeFrame.Day)
            {
                return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            }

            if (timeFrame == TimeFrame.Week)
            {
                return date.BeginningOf(TimeFrame.Week, culture).AddDays(6);
            }

            if (timeFrame == TimeFrame.Month)
            {
                return date.AddMonths(1).BeginningOf(TimeFrame.Month, culture).AddDays(-1).Date;
            }

            if (timeFrame == TimeFrame.Quarter)
            {
                return date.EndOf(TimeFrame.Quarter, date.BeginningOf(TimeFrame.Year, culture), cultureAdjust);
            }

            return new DateTime(date.Year, 12, 31);
        }

        /// <summary>
        /// End of a specific time frame
        /// </summary>
        /// <param name="date">Date to base off of</param>
        /// <param name="timeFrame">Time frame to use</param>
        /// <param name="culture">Culture to use for calculating (defaults to the current culture)</param>
        /// <param name="startOfQuarter1">Start of the first quarter</param>
        /// <returns>The end of a specific time frame (TimeFrame.Day is the only one that sets the time to 12:59:59 PM, all else are the beginning of the day)</returns>
        public static DateTime EndOf(
            this DateTime date,
            TimeFrame timeFrame,
            DateTime startOfQuarter1,
            CultureInfo? culture = null
        )
        {
            var cultureAdjust = culture ?? CultureInfo.CurrentCulture;

            if (timeFrame != TimeFrame.Quarter)
            {
                return date.EndOf(timeFrame, cultureAdjust);
            }

            if (date.Between(startOfQuarter1,
                    startOfQuarter1.AddMonths(3).AddDays(-1).EndOf(TimeFrame.Day, cultureAdjust)))
            {
                return startOfQuarter1.AddMonths(3).AddDays(-1).Date;
            }

            if (date.Between(startOfQuarter1.AddMonths(3),
                    startOfQuarter1.AddMonths(6).AddDays(-1).EndOf(TimeFrame.Day, cultureAdjust)))
            {
                return startOfQuarter1.AddMonths(6).AddDays(-1).Date;
            }

            if (date.Between(startOfQuarter1.AddMonths(6),
                    startOfQuarter1.AddMonths(9).AddDays(-1).EndOf(TimeFrame.Day, cultureAdjust)))
            {
                return startOfQuarter1.AddMonths(9).AddDays(-1).Date;
            }

            return startOfQuarter1.AddYears(1).AddDays(-1).Date;
        }

        #endregion

        #region Is

        /// <summary>
        /// Determines if the date fulfills the comparison
        /// </summary>
        /// <param name="date">Date to check</param>
        /// <param name="comparison">Comparison type (can be combined, so you can do weekday in the future, etc)</param>
        /// <returns>True if it is, false otherwise</returns>
        public static bool Is(this DateTime date, DateCompare comparison)
        {
            if (comparison.HasFlag(DateCompare.InFuture) && DateTime.Now >= date)
            {
                return false;
            }

            if (comparison.HasFlag(DateCompare.InPast) && DateTime.Now <= date)
            {
                return false;
            }

            if (comparison.HasFlag(DateCompare.Today) && DateTime.Today != date.Date)
            {
                return false;
            }

            if (comparison.HasFlag(DateCompare.WeekDay) && ((int)date.DayOfWeek == 6 || (int)date.DayOfWeek == 0))
            {
                return false;
            }

            if (comparison.HasFlag(DateCompare.WeekEnd) && (int)date.DayOfWeek != 6 && (int)date.DayOfWeek != 0)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region LocalTimeZone

        /// <summary>
        /// Gets the local time zone
        /// </summary>
        /// <param name="date">Date object</param>
        /// <returns>The local time zone</returns>
        public static TimeZoneInfo LocalTimeZone(this DateTime date)
        {
            return TimeZoneInfo.Local;
        }

        #endregion

        #region SetTime

        /// <summary>
        /// Sets the time portion of a specific date
        /// </summary>
        /// <param name="date">Date input</param>
        /// <param name="hour">Hour to set</param>
        /// <param name="minutes">Minutes to set</param>
        /// <param name="seconds">Seconds to set</param>
        /// <returns>Sets the time portion of the specified date</returns>
        public static DateTime SetTime(this DateTime date, int hour, int minutes, int seconds)
        {
            return date.SetTime(new TimeSpan(hour, minutes, seconds));
        }

        /// <summary>
        /// Sets the time portion of a specific date
        /// </summary>
        /// <param name="date">Date input</param>
        /// <param name="time">Time to set</param>
        /// <returns>Sets the time portion of the specified date</returns>
        public static DateTime SetTime(this DateTime date, TimeSpan time)
        {
            return date.Date.Add(time);
        }

        #endregion

        #region To

        /// <summary>
        /// Converts a DateTime to a specific time zone
        /// </summary>
        /// <param name="date">DateTime to convert</param>
        /// <param name="timeZone">Time zone to convert to</param>
        /// <returns>The converted DateTime</returns>
        public static DateTime To(this DateTime date, TimeZoneInfo timeZone)
        {
            if (timeZone.IsNull())
            {
                throw new ArgumentNullException(nameof(timeZone));
            }

            return TimeZoneInfo.ConvertTime(date, timeZone);
        }

        /// <summary>
        /// Returns the date in int format based on an Epoch (defaults to unix epoch of 1/1/1970)
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <param name="epoch">Epoch to use (defaults to unix epoch of 1/1/1970)</param>
        /// <returns>The date in Unix format</returns>
        public static int To(this DateTime date, DateTime epoch = default(DateTime))
        {
            epoch = epoch.Check(x => x != default(DateTime), () => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));

            return (int)((date.ToUniversalTime() - epoch).Ticks / TimeSpan.TicksPerSecond);
        }

        /// <summary>
        /// Returns the date in DateTime format based on an Epoch (defaults to unix epoch of 1/1/1970)
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <param name="epoch">Epoch to use (defaults to unix epoch of 1/1/1970)</param>
        /// <returns>The Unix Date in DateTime format</returns>
        public static DateTime To(this int date, DateTime epoch = default(DateTime))
        {
            epoch = epoch.Check(x => x != default(DateTime), () => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));

            return new DateTime((date * TimeSpan.TicksPerSecond) + epoch.Ticks, DateTimeKind.Utc);
        }

        /// <summary>
        /// Returns the date in DateTime format based on an Epoch (defaults to unix epoch of 1/1/1970)
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <param name="epoch">Epoch to use (defaults to unix epoch of 1/1/1970)</param>
        /// <returns>The Unix Date in DateTime format</returns>
        public static DateTime To(this long date, DateTime epoch = default(DateTime))
        {
            epoch = epoch.Check(x => x != default(DateTime), () => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));

            return new DateTime((date * TimeSpan.TicksPerSecond) + epoch.Ticks, DateTimeKind.Utc);
        }

        #endregion

        #region ToString

        /// <summary>
        /// Converts the DateTime object to string describing, relatively how long ago or how far in the future
        /// the input is based off of another DateTime object specified.
        /// ex: 
        /// Input=March 21, 2013
        /// Epoch=March 22, 2013
        /// returns "1 day ago"
        /// Input=March 22, 2013
        /// Epoch=March 21, 2013
        /// returns "1 day from now"
        /// </summary>
        /// <param name="input">Input</param>
        /// <param name="epoch">DateTime object that the input is comparred to</param>
        /// <returns>The difference between the input and epoch expressed as a string</returns>
        public static string ToString(this DateTime input, DateTime epoch)
        {
            if (epoch == input)
            {
                return "now";
            }

            return epoch > input
                ? (epoch - input).ToStringFull() + " ago"
                : (input - epoch).ToStringFull() + " from now";
        }

        #endregion

        #region UTCOffset

        /// <summary>
        /// Gets the UTC offset
        /// </summary>
        /// <param name="date">Date to get the offset of</param>
        /// <returns>UTC offset</returns>
        // ReSharper disable InconsistentNaming
        public static double UTCOffset(this DateTime date)
        {
            return (date - date.ToUniversalTime()).TotalHours;
        }

        #endregion
    }
}