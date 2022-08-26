using System.IO;
using EasySoft.UtilityTools.Core.ConfigurationProviders;
using EasySoft.UtilityTools.Core.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace EasySoft.UtilityTools.Core.ConfigurationSources;

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