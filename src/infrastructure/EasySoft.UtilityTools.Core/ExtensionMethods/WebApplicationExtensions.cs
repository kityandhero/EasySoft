using System.Collections.Concurrent;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.UtilityTools.Core.ExtensionMethods;

public static class WebApplicationExtensions
{
    private static readonly ConcurrentDictionary<string, char> UseModels = new();

    public static bool HasUsed(this WebApplication _, string modelName)
    {
        return !UseModels.TryAdd(modelName.ToLower(), '1');
    }
}