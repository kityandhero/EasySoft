using System;
using Microsoft.AspNetCore.Hosting;

namespace EasySoft.UtilityTools.Core.ExtensionMethods;

public static class WebHostEnvironmentExtensions
{
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