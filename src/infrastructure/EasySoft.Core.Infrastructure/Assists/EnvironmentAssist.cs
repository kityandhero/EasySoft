﻿using Microsoft.AspNetCore.Hosting;

namespace EasySoft.Core.Infrastructure.Assists;

public static class EnvironmentAssist
{
    private static IWebHostEnvironment? _environment;

    public static void SetEnvironment(IWebHostEnvironment? environment)
    {
        _environment = environment;
    }

    public static IWebHostEnvironment? GetEnvironment()
    {
        if (_environment == null)
        {
            throw new Exception("environment has not set yet");
        }

        return _environment;
    }
}