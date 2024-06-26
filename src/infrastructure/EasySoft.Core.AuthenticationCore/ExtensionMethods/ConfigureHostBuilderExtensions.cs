﻿using EasySoft.Core.AuthenticationCore.Operators;

namespace EasySoft.Core.AuthenticationCore.ExtensionMethods;

/// <summary>
/// ConfigureHostBuilderExtensions
/// </summary>
public static class ConfigureHostBuilderExtensions
{
    /// <summary>
    /// 注入操作员
    /// </summary>
    /// <returns></returns>
    public static ConfigureHostBuilder AddOperator<T>(
        this ConfigureHostBuilder configureHostBuilder
    ) where T : ActualOperator
    {
        configureHostBuilder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            // https://docs.autofac.org/en/latest/faq/per-request-scope.html
            containerBuilder.RegisterType<T>().As<IActualOperator>().InstancePerLifetimeScope();
        });

        return configureHostBuilder;
    }
}