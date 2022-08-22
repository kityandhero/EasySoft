using System;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods
{
    public static class TimeSpanExtensions
    {
        public static int DaysRemainder(this TimeSpan timeSpan)
        {
            return (DateTime.MinValue + timeSpan).Day - 1;
        }

        public static int Months(this TimeSpan timeSpan)
        {
            return (DateTime.MinValue + timeSpan).Month - 1;
        }

        public static int Years(this TimeSpan timeSpan)
        {
            return (DateTime.MinValue + timeSpan).Year - 1;
        }

        public static int[] ToDateTimeArray(this TimeSpan input)
        {
            var array = new int[6];

            array[0] = input.Years();
            array[1] = input.Months();
            array[2] = input.DaysRemainder();
            array[3] = input.Hours;
            array[4] = input.Minutes;
            array[5] = input.Seconds;

            return array;
        }

        #region ToStringFull

        /// <summary>
        /// Converts the input to a string in this format:
        /// (Years) years, (Months) months, (DaysRemainder) days, (Hours) hours, (Minutes) minutes, (Seconds) seconds
        /// </summary>
        /// <param name="input">Input TimeSpan</param>
        /// <returns>The TimeSpan as a string</returns>
        public static string ToStringFull(this TimeSpan input)
        {
            var result = "";
            var splitter = "";

            if (input.Years() > 0)
            {
                result += input.Years() + " year" + (input.Years() > 1 ? "s" : "");
                splitter = ", ";
            }

            if (input.Months() > 0)
            {
                result += splitter + input.Months() + " month" + (input.Months() > 1 ? "s" : "");
                splitter = ", ";
            }

            if (input.DaysRemainder() > 0)
            {
                result += splitter + input.DaysRemainder() + " day" + (input.DaysRemainder() > 1 ? "s" : "");
                splitter = ", ";
            }

            if (input.Hours > 0)
            {
                result += splitter + input.Hours + " hour" + (input.Hours > 1 ? "s" : "");
                splitter = ", ";
            }

            if (input.Minutes > 0)
            {
                result += splitter + input.Minutes + " minute" + (input.Minutes > 1 ? "s" : "");
                splitter = ", ";
            }

            if (input.Seconds > 0)
            {
                result += splitter + input.Seconds + " second" + (input.Seconds > 1 ? "s" : "");
                splitter = ", ";
            }

            return result;
        }

        #endregion
    }
}