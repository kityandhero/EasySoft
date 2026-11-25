namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// HostEnvironmentExtensions
/// </summary>
public static class HostEnvironmentExtensions
{
    /// <summary>
    /// GetAliasName
    /// </summary>
    /// <param name="environment"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string GetAliasName(this IHostEnvironment environment)
    {
        try
        {
            return environment.EnvironmentName.ToLower() switch
            {
                "development" => "dev",
                "test" => "test",
                "staging" => "stag",
                "production" => "prod",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        catch (Exception)
        {
            return "";
        }
    }
}