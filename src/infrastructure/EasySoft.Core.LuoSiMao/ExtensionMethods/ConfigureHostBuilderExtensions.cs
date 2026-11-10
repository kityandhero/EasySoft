using EasySoft.Core.LuoSiMao.LuoSiMao;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.LuoSiMao.ExtensionMethods;

public static class ConfigureHostBuilderExtensions
{
    internal static ConfigureHostBuilder AddLuoSiMao(
        this ConfigureHostBuilder builder,
        SmsConfig smsConfig
    )
    {
        builder.ConfigureServices(services =>
            {
                services.AddTransient(sp =>
                    ActivatorUtilities.CreateInstance<SmsService>(sp, smsConfig)
                );

                services.AddTransient<ISmsService>(sp =>
                    sp.GetRequiredService<SmsService>()
                );
            }
        );

        return builder;
    }
}