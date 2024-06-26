﻿namespace EasySoft.UtilityTools.Standard.Enums;

/// <summary>
/// ErrorLogExchangeType
/// </summary>
public enum ErrorLogType
{
    /// <summary>
    /// 数据错误
    /// </summary>
    [Description("数据错误")]
    DataError = 1000,

    /// <summary>
    /// 程序异常
    /// </summary>
    [Description("程序异常")]
    Exception = 2000
}