namespace EasySoft.UtilityTools.Core.ExtensionMethods;

/// <summary>
/// WebHostEnvironmentExtensions
/// </summary>
public static class WebHostEnvironmentExtensions
{
    /// <summary>
    /// GetAliasName
    /// </summary>
    /// <param name="environment"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string GetAliasName(this IWebHostEnvironment environment)
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