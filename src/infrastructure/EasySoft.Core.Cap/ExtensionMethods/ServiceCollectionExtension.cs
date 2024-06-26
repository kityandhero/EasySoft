﻿namespace EasySoft.Core.Cap.ExtensionMethods;

/// <summary>
/// ServiceCollectionExtension
/// </summary>
internal static class ServiceCollectionExtension
{
    /// <summary>
    /// AddCapSubscriber
    /// </summary>
    /// <param name="services"></param>
    /// <typeparam name="TSubscriber"></typeparam>
    /// <returns></returns>
    internal static IServiceCollection AddCapSubscriber<TSubscriber>(
        this IServiceCollection services
    ) where TSubscriber : class, ICapSubscribe
    {
        services.AddScoped<TSubscriber>();

        return services;
    }
}