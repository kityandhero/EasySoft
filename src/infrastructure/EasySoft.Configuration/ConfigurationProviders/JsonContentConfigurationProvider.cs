using System.Text.Json;
using EasySoft.Configuration.ConfigurationFileParsers;
using EasySoft.Configuration.ConfigurationSources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace EasySoft.Configuration.ConfigurationProviders;

public class JsonContentConfigurationProvider : ConfigurationProvider, IDisposable
{
    private readonly IDisposable? _changeTokenRegistration;

    public JsonContentConfigurationSource Source { get; }

    public JsonContentConfigurationProvider(JsonContentConfigurationSource source)
    {
        Source = source ?? throw new ArgumentNullException(nameof(source));

        Source.OnJsonContentChanged += TriggerWhenJsonContentChanged;

        if (Source.ReloadOnChange)
        {
            _changeTokenRegistration = ChangeToken.OnChange(
                () =>
                {
                    var token = Source.Watch();

                    return token;
                },
                () =>
                {
                    Thread.Sleep(Source.ReloadDelay);

                    Console.WriteLine("configure changed" + new Random().Next());

                    OnReload();
                });
        }
    }

    private void TriggerWhenJsonContentChanged()
    {
        Source.PrepareRefresh();
    }

    public override void Load()
    {
        try
        {
            Data = JsonContentConfigurationFileParser.Parse(Source.GetJsonContent());
        }
        catch (JsonException e)
        {
            throw new FormatException($"JSONParseError: {e.Message}");
        }
    }

    public void Dispose()
    {
        _changeTokenRegistration?.Dispose();
    }
}