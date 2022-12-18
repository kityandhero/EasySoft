﻿namespace EasySoft.UtilityTools.Standard.Entity.Interfaces;

/// <summary>
/// 应用验证公钥
/// </summary>
public interface IAppPublicKey
{
    /// <summary>
    /// 验证公钥
    /// </summary>
    [Description("Key")]
    public string Key { get; set; }
}