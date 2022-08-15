namespace UtilityTools.Enums
{
    public enum DateCompare
    {
        /// <summary>
        /// In the future
        /// </summary>
        InFuture = 1,

        /// <summary>
        /// In the past
        /// </summary>
        InPast = 2,

        /// <summary>
        /// Today
        /// </summary>
        Today = 4,

        /// <summary>
        /// Weekday
        /// </summary>
        WeekDay = 8,

        /// <summary>
        /// Weekend
        /// </summary>
        WeekEnd = 16
    }
}