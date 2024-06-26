﻿namespace EasySoft.Core.EasyCaching.ConfigCollection;

public class RedisConfig : IConfig
{
    public static readonly RedisConfig Instance = new();

    public string KeyPrefix { get; set; }

    public string Connections { get; set; }

    public string Sentinels { get; set; }

    public RedisConfig()
    {
        Connections = "";
        Sentinels = "";
        KeyPrefix = "";
    }
}