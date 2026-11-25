using EasySoft.UtilityTools.Core.Assists;

namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// ConfigurationBuilderExtensions
/// </summary>
public static class ConfigurationBuilderExtensions
{
    /// <summary>
    /// AddMultiJsonFile
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="filePath"></param>
    /// <returns></returns>
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

        if (!string.IsNullOrWhiteSpace(HostEnvironmentAssist.TryGetEnvironmentAliasName()))
        {
            var otherFilePath = filePath.Replace(".json", $".{HostEnvironmentAssist.GetEnvironmentAliasName()}.json");

            builder.AddJsonFile(
                otherFilePath,
                true,
                true
            );
        }

        return builder;
    }
}