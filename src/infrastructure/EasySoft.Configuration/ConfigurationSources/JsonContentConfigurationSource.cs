using EasySoft.Configuration.ConfigurationProviders;
using EasySoft.Configuration.Providers;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Configuration.ConfigurationSources;

public class JsonContentConfigurationSource : JsonContentConfigurationSourceCore
{
    public string JsonContent { get; set; }

    public JsonContentConfigurationSource()
    {
        JsonContent = "";
    }

    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new JsonContentConfigurationProvider(this);
    }

    public IContentProvider ResolveFileProvider()
    {
        ContentProvider ??= new ContentProvider("");

        return ContentProvider;
    }
}