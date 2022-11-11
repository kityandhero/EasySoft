using System;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using Microsoft.AspNetCore.Hosting;

namespace EasySoft.UtilityTools.Core.Assists;

public static class EnvironmentAssist
{
    private static IWebHostEnvironment? _environment;

    public static void SetEnvironment(IWebHostEnvironment? environment)
    {
        if (_environment != null) throw new Exception("environment has been set, it disallow set more than once.");

        _environment = environment;
    }

    public static IWebHostEnvironment GetEnvironment()
    {
        if (_environment == null) throw new Exception("environment has not set yet");

        return _environment;
    }

    public static string GetEnvironmentAliasName()
    {
        var environment = GetEnvironment();

        return environment.GetAliasName();
    }
}