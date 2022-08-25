using System;
using System.IO;
using System.Threading;
using EasySoft.UtilityTools.Core.ConfigurationFileParsers;
using EasySoft.UtilityTools.Core.ConfigurationSources;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace EasySoft.UtilityTools.Core.ConfigurationProviders;

public class JsonContentConfigurationProvider : JsonConfigurationProvider
{
    private readonly string _content;
    
    /// <summary>
    /// Initializes a new instance with the specified source.
    /// </summary>
    /// <param name="source">The source settings.</param>
    public JsonContentConfigurationProvider(JsonContentConfigurationSource source) : base(source)
    {
        _content = source.Content;
        
        
        if (Source.ReloadOnChange && Source.FileProvider != null)
        {
            ChangeToken.OnChange(
                () => Source.FileProvider.Watch(Source.Path),
                () => {
                    Thread.Sleep(Source.ReloadDelay);
                    Load(reload: true);
                });
        }
    }

    /// <summary>
    /// Loads the JSON data from a stream.
    /// </summary>
    /// <param name="stream">The stream to read.</param>
    public override void Load(Stream stream)
    {
        try
        {
            Data = JsonContentConfigurationFileParser.Parse(_content);
        }
        catch (JsonReaderException)
        {
            throw new FormatException("Load error");
        }
    }
}