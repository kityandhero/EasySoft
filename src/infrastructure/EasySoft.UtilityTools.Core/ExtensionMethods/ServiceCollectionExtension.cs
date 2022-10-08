using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.UtilityTools.Core.ExtensionMethods;

public static class ServiceCollectionExtension
{
    private static readonly ConcurrentDictionary<string, char> RegisteredModels = new();

    public static bool HasRegistered(this IServiceCollection _, string modelName)
    {
        return !RegisteredModels.TryAdd(modelName.ToLower(), '1');
    }
}