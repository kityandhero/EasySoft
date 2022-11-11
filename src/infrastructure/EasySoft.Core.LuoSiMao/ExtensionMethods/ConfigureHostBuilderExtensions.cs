using EasySoft.Core.LuoSiMao.LuoSiMao;

namespace EasySoft.Core.LuoSiMao.ExtensionMethods;

public static class ConfigureHostBuilderExtensions
{
    internal static ConfigureHostBuilder AddLuoSiMao(
        this ConfigureHostBuilder builder,
        SmsConfig smsConfig
    )
    {
        builder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.Register(c =>
            {
                var httpClientFactory = c.Resolve<IHttpClientFactory>();

                return new SmsService(smsConfig, httpClientFactory);
            }).AsSelf().As<ISmsService>().InstancePerDependency();
        });

        return builder;
    }
}