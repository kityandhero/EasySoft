﻿namespace EasySoft.Core.Infrastructure.Assists;

public static class FlagAssist
{
    private static bool _applicationRunPerformed;
    private static bool _advanceStaticFileOptionsSwitch;

    public static string TokenMode { get; set; }

    public static bool EasyTokenMiddlewareModeSwitch { get; set; }

    public static bool EasyTokenSecretOptionInjectionComplete { get; set; }

    public static bool EasyTokenSecretInjectionComplete { get; set; }

    public static bool JsonWebTokenMiddlewareModeSwitch { get; set; }

    public static bool PermissionVerificationSwitch { get; set; }

    public static bool PermissionVerificationMiddlewareModeSwitch { get; set; }

    public static IEnumerable<string> StartupUrls { get; set; }

    static FlagAssist()
    {
        _applicationRunPerformed = false;
        _advanceStaticFileOptionsSwitch = false;

        TokenMode = "";
        EasyTokenMiddlewareModeSwitch = false;
        EasyTokenSecretOptionInjectionComplete = false;
        EasyTokenSecretInjectionComplete = false;
        JsonWebTokenMiddlewareModeSwitch = false;
        PermissionVerificationSwitch = false;
        PermissionVerificationMiddlewareModeSwitch = false;
        StartupUrls = new List<string>();
    }

    public static void SetApplicationRunPerformed()
    {
        _applicationRunPerformed = true;
    }

    /// <summary>
    /// 获取应用是否已经 running
    /// </summary>
    /// <returns></returns>
    public static bool GetApplicationRunWhetherPerformed()
    {
        return _applicationRunPerformed;
    }

    public static void SetAdvanceStaticFileOptionsSwitchOpen()
    {
        _advanceStaticFileOptionsSwitch = true;
    }

    /// <summary>
    /// 获取应用是否已经 running
    /// </summary>
    /// <returns></returns>
    public static bool GetAdvanceStaticFileOptionsSwitch()
    {
        return _advanceStaticFileOptionsSwitch;
    }
}