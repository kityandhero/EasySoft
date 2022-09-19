using EasySoft.Core.Config.ConfigAssist;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ExtensionMethods;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddMultiJsonFile(
        this IConfigurationBuilder builder,
        string filePath
    )
    {
        builder.AddJsonFile(
            filePath,
            true,
            true
        );

        if (!string.IsNullOrWhiteSpace(EnvironmentConfigAssist.GetCustomEnv()))
        {
            var otherFilePath = filePath.Replace(".json", $".{EnvironmentConfigAssist.GetCustomEnv()}.json");

            builder.AddJsonFile(
                otherFilePath,
                true,
                true
            );
        }

        return builder;
    }
}