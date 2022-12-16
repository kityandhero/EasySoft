﻿namespace EasySoft.UtilityTools.Standard.Entity.Interfaces;

/// <summary>
/// 创建时间
/// </summary>
public interface ICreateTime
{
    /// <summary>
    /// 创建时间
    /// </summary>
    [Description("创建时间")]
    DateTime CreateTime { get; set; }
}