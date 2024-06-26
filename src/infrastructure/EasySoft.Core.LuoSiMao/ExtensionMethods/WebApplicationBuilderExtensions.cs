﻿using EasySoft.Core.LuoSiMao.LuoSiMao;

namespace EasySoft.Core.LuoSiMao.ExtensionMethods;

/// <summary>
/// 短信服务扩展
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 注册LuoSiMao短信服务
    /// </summary>
    /// <param name="builder">服务集合</param>
    /// <param name="key">密钥</param>
    public static WebApplicationBuilder AddLuoSiMao(this WebApplicationBuilder builder, string key)
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddLuoSiMao)}."
        );

        builder.Host.AddLuoSiMao(new SmsConfig(key));

        return builder;
    }

    /// <summary>
    /// 注册LuoSiMao短信服务
    /// </summary>
    /// <param name="builder">服务集合</param>
    /// <param name="smsConfig">密钥</param>
    public static WebApplicationBuilder AddLuoSiMao(this WebApplicationBuilder builder, SmsConfig smsConfig)
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddLuoSiMao)}."
        );

        builder.Host.AddLuoSiMao(smsConfig);

        return builder;
    }
}