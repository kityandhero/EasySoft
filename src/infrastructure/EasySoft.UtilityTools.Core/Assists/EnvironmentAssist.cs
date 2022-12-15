using EasySoft.UtilityTools.Core.ExtensionMethods;

namespace EasySoft.UtilityTools.Core.Assists;

/// <summary>
/// EnvironmentAssist
/// </summary>
public static class EnvironmentAssist
{
    private static IWebHostEnvironment? _environment;

    /// <summary>
    /// SetEnvironment
    /// </summary>
    /// <param name="environment"></param>
    /// <exception cref="Exception"></exception>
    public static void SetEnvironment(IWebHostEnvironment? environment)
    {
        if (_environment != null) throw new Exception("environment has been set, it disallow set more than once.");

        _environment = environment;
    }

    /// <summary>
    /// GetEnvironment
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IWebHostEnvironment GetEnvironment()
    {
        if (_environment == null) throw new Exception("environment has not set yet");

        return _environment;
    }

    /// <summary>
    /// IsDevelopment
    /// </summary>
    /// <returns></returns>
    public static bool IsDevelopment()
    {
        var environment = GetEnvironment();

        return environment.IsDevelopment();
    }

    /// <summary>
    /// GetEnvironmentAliasName
    /// </summary>
    /// <returns></returns>
    public static string GetEnvironmentAliasName()
    {
        var environment = GetEnvironment();

        return environment.GetAliasName();
    }

    /// <summary>
    /// TryGetEnvironmentAliasName
    /// </summary>
    /// <returns></returns>
    public static string TryGetEnvironmentAliasName()
    {
        try
        {
            return GetEnvironmentAliasName();
        }
        catch (Exception e)
        {
            return "";
        }
    }
}