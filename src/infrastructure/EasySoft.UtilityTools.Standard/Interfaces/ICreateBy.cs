﻿namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// 创建人
/// </summary>
public interface ICreateBy
{
    /// <summary>
    /// 创建人
    /// </summary>
    [Description("创建人")]
    long CreateBy { get; set; }
}