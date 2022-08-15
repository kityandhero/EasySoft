﻿using Framework.ConfigInterface;

namespace Framework.ConfigCollection;

public class ElasticSearchConfig : IConfig
{
    public static readonly ElasticSearchConfig Instance = new();

    /// <summary>
    /// ElasticSearch数据版本
    /// </summary>
    public string ElasticSearchDataVersion { get; set; }

    public ElasticSearchConfig()
    {
        ElasticSearchDataVersion = "0";
    }
}