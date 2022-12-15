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