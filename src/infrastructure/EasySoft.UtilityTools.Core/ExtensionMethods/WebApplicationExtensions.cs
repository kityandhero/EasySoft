namespace EasySoft.UtilityTools.Core.ExtensionMethods;

/// <summary>
/// WebApplicationExtensions
/// </summary>
public static class WebApplicationExtensions
{
    private static readonly ConcurrentDictionary<string, char> UseModels = new();

    /// <summary>
    /// HasUsed
    /// </summary>
    /// <param name="_"></param>
    /// <param name="modelName"></param>
    /// <returns></returns>
    public static bool HasUsed(this WebApplication _, string modelName)
    {
        return !UseModels.TryAdd(modelName.ToLower(), '1');
    }
}