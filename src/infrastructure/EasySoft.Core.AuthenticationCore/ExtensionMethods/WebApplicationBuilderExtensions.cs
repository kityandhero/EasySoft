﻿using EasySoft.Core.AuthenticationCore.Operators;

namespace EasySoft.Core.AuthenticationCore.ExtensionMethods;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 注入操作员
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddOperator<T>(
        this WebApplicationBuilder builder
    ) where T : ActualOperator
    {
        builder.Host.AddOperator<T>();

        return builder;
    }
}