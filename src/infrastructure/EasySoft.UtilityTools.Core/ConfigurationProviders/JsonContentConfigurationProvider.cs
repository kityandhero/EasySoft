using System;
using System.Threading;
using EasySoft.UtilityTools.Core.ConfigurationFileParsers;
using EasySoft.UtilityTools.Core.ConfigurationSources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace EasySoft.UtilityTools.Core.ConfigurationProviders;

public class JsonContentConfigurationProvider : ConfigurationProvider
{
    private readonly string _content;

    public JsonContentConfigurationSource Source { get; }

    /// <summary>
    /// Initializes a new instance with the specified source.
    /// </summary>
    /// <param name="source">The source settings.</param>
    public JsonContentConfigurationProvider(JsonContentConfigurationSource source)
    {
        Source = source ?? throw new ArgumentNullException(nameof(source));

        _content = source.JsonContent;

        if (Source.ReloadOnChange)
        {
            ChangeToken.OnChange(
                () => Source.ResolveFileProvider().Watch(Source.JsonContent),
                () =>
                {
                    Thread.Sleep(Source.ReloadDelay);

                    OnReload();
                });
        }
    }

    /// <summary>
    /// Loads the JSON data from a stream.
    /// </summary>
    /// <param name="content">The stream to read.</param>
    public void Load(string content)
    {
        try
        {
            Data = JsonContentConfigurationFileParser.Parse(content);
        }
        catch (JsonReaderException)
        {
            throw new FormatException("Load error");
        }
    }
}