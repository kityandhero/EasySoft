using EasySoft.UtilityTools.Core.ExtensionMethods;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;

namespace EasySoft.Core.NLog.ExtensionMethods;

public static class ConfigureExtensions
{
    public static NLogProviderOptions Configure(
        this NLogProviderOptions options,
        string jsonString,
        string key
    )
    {
        if (string.IsNullOrWhiteSpace(jsonString))
        {
            throw new Exception("jsonConfig disallow empty");
        }

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new Exception("key disallow empty");
        }

        var configurationRoot = new ConfigurationBuilder().AddJsonContent(jsonString).Build();

        return options.Configure(configurationRoot.GetSection(key));
    }
}