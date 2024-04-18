namespace EasySoft.Core.Config.ExtensionMethods;

/// <summary>
/// ConfigurationBuilderExtensions
/// </summary>
public static class ConfigurationBuilderExtensions
{
    /// <summary>
    /// AddMultiJsonFile
    /// </summary>
    /// <param name="builder"></param>
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

        if (!string.IsNullOrWhiteSpace(EnvironmentAssist.TryGetEnvironmentAliasName()))
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