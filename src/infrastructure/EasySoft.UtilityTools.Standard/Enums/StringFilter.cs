﻿namespace EasySoft.UtilityTools.Standard.Enums;

/// <summary>
/// 文本筛选
/// </summary>
public enum StringFilter
{
    /// <summary>
    /// Alpha characters
    /// </summary>
    Alpha = 1,

    /// <summary>
    /// Numeric characters
    /// </summary>
    Numeric = 2,

    /// <summary>
    /// Numbers with period, basically allows for decimal point
    /// </summary>
    FloatNumeric = 4,

    /// <summary>
    /// Multiple spaces
    /// </summary>
    ExtraSpaces = 8
}