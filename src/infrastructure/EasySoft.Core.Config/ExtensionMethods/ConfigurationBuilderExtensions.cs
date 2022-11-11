using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Core.Assists;
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

        if (!string.IsNullOrWhiteSpace(EnvironmentAssist.GetEnvironmentAliasName()))
        {
            var otherFilePath = filePath.Replace(".json", $".{EnvironmentAssist.GetEnvironmentAliasName()}.json");

            builder.AddJsonFile(
                otherFilePath,
                true,
                true
            );
        }

        return builder;
    }
}