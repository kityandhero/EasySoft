using System.Collections.Concurrent;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.UtilityTools.Core.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private static readonly ConcurrentDictionary<string, char> RegisteredModels = new();

    public static bool HasRegistered(this WebApplicationBuilder _, string modelName)
    {
        return !RegisteredModels.TryAdd(modelName.ToLower(), '1');
    }
}