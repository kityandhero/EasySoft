﻿using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

/// <summary>
/// MaintainConfig
/// </summary>
public class MaintainConfig : IConfig
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static readonly MaintainConfig Instance = new();

    /// <summary>
    /// UrlPollingRequests
    /// </summary>
    public string UrlPollingRequests { get; set; } = "";
}