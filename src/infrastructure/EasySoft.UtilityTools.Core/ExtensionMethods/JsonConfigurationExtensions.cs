using EasySoft.UtilityTools.Core.ConfigurationSources;
using Microsoft.Extensions.Configuration;

namespace EasySoft.UtilityTools.Core.ExtensionMethods;

public static class JsonConfigurationExtensions
{
    public static IConfigurationBuilder AddJsonContent(
        this IConfigurationBuilder builder,
        string jsonContent,
        out JsonContentConfigurationSource jsonSource
    )
    {
        builder.AddJsonContent(s =>
        {
            s.ReloadOnChange = true;

            s.SetJsonContent(jsonContent);
        });

        var source = builder.Sources[0] as JsonContentConfigurationSource;

        jsonSource = source ?? throw new Exception("jsonSource is null");

        return builder;
    }

    /// <summary>
    /// 使用Json字符串作为配置源，该方法不会监听配置变化
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="jsonContent"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="Exception"></exception>
    public static IConfigurationBuilder AddJsonContent(
        this IConfigurationBuilder builder,
        string jsonContent
    )
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        if (string.IsNullOrEmpty(jsonContent)) throw new Exception("json content disallow empty");

        return builder.AddJsonContent(s =>
        {
            s.ReloadOnChange = true;

            s.SetJsonContent(jsonContent);
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
    )
    {
        if (builder.Sources.Count > 1) throw new Exception("AddJsonContent disallow more than one configure source");

        return builder.Add(configureSource);
    }
}