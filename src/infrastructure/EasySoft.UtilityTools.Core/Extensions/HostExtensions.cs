namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// HostExtensions
/// </summary>
public static class HostExtensions
{
    private static readonly ConcurrentDictionary<string, char> UseModels = new();

    /// <summary>
    /// HasUsed
    /// </summary>
    /// <param name="_"></param>
    /// <param name="modelName"></param>
    /// <returns></returns>
    public static bool HasUsed(this IHost _, string modelName)
    {
        return !UseModels.TryAdd(modelName.ToLower(), '1');
    }
}