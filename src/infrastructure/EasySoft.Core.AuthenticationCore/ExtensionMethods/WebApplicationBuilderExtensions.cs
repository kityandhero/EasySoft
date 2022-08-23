using Autofac;
using EasySoft.Core.AuthenticationCore.Operators;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.AuthenticationCore.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 注入操作员
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder UseOperatorInjection<T>(
        this WebApplicationBuilder builder
    ) where T : ActualOperator
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            // https://docs.autofac.org/en/latest/faq/per-request-scope.html
            containerBuilder.RegisterType<T>().As<IActualOperator>().InstancePerLifetimeScope();
        });

        return builder;
    }
}