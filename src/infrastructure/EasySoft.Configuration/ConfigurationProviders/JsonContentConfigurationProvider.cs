using EasySoft.Configuration.ConfigurationSources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace EasySoft.Configuration.ConfigurationProviders;

public class JsonContentConfigurationProvider : ConfigurationProvider
{
    public JsonContentConfigurationSource Source { get; }

    public JsonContentConfigurationProvider(JsonContentConfigurationSource source)
    {
        Source = source ?? throw new ArgumentNullException(nameof(source));

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
}