﻿using System.ComponentModel;

namespace EasySoft.Core.ExchangeRegulation.Enums;

public enum CustomValueType
{
    /// <summary>
    /// Unknown
    /// </summary>
    [Description("Unknown")]
    Unknown = 0,

    /// <summary>
    /// JsonObject
    /// </summary>
    [Description("JsonObject")]
    JsonObject = 100,

    /// <summary>
    /// JsonObjectList
    /// </summary>
    [Description("JsonObjectList")]
    JsonObjectList = 200,

    /// <summary>
    /// PlainValue
    /// </summary>
    [Description("PlainValue")]
    PlainValue = 300,
}