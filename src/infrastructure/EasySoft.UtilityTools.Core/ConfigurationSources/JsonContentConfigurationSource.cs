using EasySoft.UtilityTools.Core.ConfigurationProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace EasySoft.UtilityTools.Core.ConfigurationSources;

public class JsonContentConfigurationSource : JsonConfigurationSource
{
    public string Content { get; set; }

    public JsonContentConfigurationSource()
    {
        Content = "";
    }

    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        EnsureDefaults(builder);

        return new JsonContentConfigurationProvider(this);
    }
}