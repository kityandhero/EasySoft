using EasySoft.UtilityTools.Core.ConfigurationFileParsers;
using EasySoft.UtilityTools.Core.ConfigurationSources;

namespace EasySoft.UtilityTools.Core.ConfigurationProviders;

/// <summary>
/// JsonContentConfigurationProvider
/// </summary>
public class JsonContentConfigurationProvider : ConfigurationProvider, IDisposable
{
    private readonly IDisposable? _changeTokenRegistration;

    /// <summary>
    /// Source
    /// </summary>
    public JsonContentConfigurationSource Source { get; }

    /// <summary>
    /// JsonContentConfigurationProvider
    /// </summary>
    /// <param name="source"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public JsonContentConfigurationProvider(JsonContentConfigurationSource source)
    {
        Source = source ?? throw new ArgumentNullException(nameof(source));

        Source.OnJsonContentChanged += TriggerWhenJsonContentChanged;

        if (Source.ReloadOnChange)
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

    private void TriggerWhenJsonContentChanged()
    {
        Source.PrepareRefresh();
    }

    /// <summary>
    /// Load
    /// </summary>
    /// <exception cref="FormatException"></exception>
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
        catch (Exception)
        {
            Console.WriteLine(Source.GetJsonContent());

            throw;
        }
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        _changeTokenRegistration?.Dispose();
    }
}