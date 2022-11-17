namespace EasySoft.Core.Infrastructure.Assists;

/// <summary>
/// FlagAssist
/// </summary>
public static class FlagAssist
{
    private static bool _applicationRunPerformed;
    private static bool _advanceStaticFileOptionsSwitch;

    /// <summary>
    /// TokenMode
    /// </summary>
    public static string TokenMode { get; set; }

    /// <summary>
    /// EasyTokenMiddlewareModeSwitch
    /// </summary>
    public static bool EasyTokenMiddlewareModeSwitch { get; set; }

    /// <summary>
    /// EasyTokenSecretOptionInjectionComplete
    /// </summary>
    public static bool EasyTokenSecretOptionInjectionComplete { get; set; }

    /// <summary>
    /// EasyTokenSecretInjectionComplete
    /// </summary>
    public static bool EasyTokenSecretInjectionComplete { get; set; }

    /// <summary>
    /// JsonWebTokenMiddlewareModeSwitch
    /// </summary>
    public static bool JsonWebTokenMiddlewareModeSwitch { get; set; }

    /// <summary>
    /// PermissionVerificationSwitch
    /// </summary>
    public static bool PermissionVerificationSwitch { get; set; }

    /// <summary>
    /// PermissionVerificationMiddlewareModeSwitch
    /// </summary>
    public static bool PermissionVerificationMiddlewareModeSwitch { get; set; }

    /// <summary>
    /// StartupUrls
    /// </summary>
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

    /// <summary>
    /// SetApplicationRunPerformed
    /// </summary>
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

    /// <summary>
    /// SetAdvanceStaticFileOptionsSwitchOpen
    /// </summary>
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