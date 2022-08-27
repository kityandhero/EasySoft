using EasySoft.Configuration.ConfigurationSources;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Configuration.ExtensionMethods;

public static class JsonConfigurationExtensions
{
    /// <summary>
    /// Adds a JSON content configuration source to <paramref name="builder"/>.
    /// </summary>
    public static IConfigurationBuilder AddJsonContent(
        this IConfigurationBuilder builder,
        string jsonContent
    )
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        if (string.IsNullOrEmpty(jsonContent))
        {
            throw new Exception("json content disallow empty");
        }

        return builder.AddJsonContent(s =>
        {
            s.JsonContent = jsonContent;
            s.ReloadOnChange = true;
            s.ResolveContentProvider();
        });
    }

    /// <summary>
    /// Adds a JSON configuration source to <paramref name="builder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
    /// <param name="configureSource">Configures the source.</param>
    /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
    public static IConfigurationBuilder AddJsonContent(
        this IConfigurationBuilder builder,
        Action<JsonContentConfigurationSource> configureSource
    ) => builder.Add(configureSource);
}