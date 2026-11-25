using EasySoft.UtilityTools.Core.Extensions;

namespace EasySoft.UtilityTools.Core.Assists;

/// <summary>
/// HostEnvironmentAssist
/// </summary>
public static class HostEnvironmentAssist
{
    private static IHostEnvironment _hostEnvironment;

    /// <summary>   
    /// SetEnvironment
    /// </summary>
    /// <param name="environment"></param>
    /// <exception cref="Exception"></exception>
    public static void SetEnvironment(IHostEnvironment environment)
    {
        if (_hostEnvironment != null)
        {
            throw new Exception("environment has been set, it disallow set more than once.");
        }

        _hostEnvironment = environment;
    }

    /// <summary>
    /// GetEnvironment
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IHostEnvironment GetEnvironment()
    {
        if (_hostEnvironment == null)
        {
            throw new Exception("environment has not set yet");
        }

        return _hostEnvironment;
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
        catch (Exception)
        {
            return "";
        }
    }
}