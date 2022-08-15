namespace NPOIEx
{
    public static class ConstCollection
    {
        public const string DefaultSheetName = "Sheet1";
        public const string DefaultFileDatetime = "yyyyMMdd_HHmm";
        public const string DatetimeFormat = "dd/MM/yyyy hh:mm:ss";
        public const string ExcelMediaType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public const string DispositionTypeAttachment = "attachment";

        #region DataType available for Excel Export

        public const string StringType = "string";
        public const string Int32Type = "int32";
        public const string Int64Type = "int64";
        public const string DecimalType = "decimal";
        public const string DoubleType = "double";
        public const string DatetimeType = "datetime";

        #endregion
    }
}