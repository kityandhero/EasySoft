namespace EasySoft.Core.Infrastructure.Assists;

public static class FlagAssist
{
    private static bool _applicationRunPerformed;
    private static bool _advanceStaticFileOptionsSwitch;
    private static bool _logDashboardSwitch;
    private static bool _entityFrameworkSwitch;
    private static bool _healthChecksSwitch;
    private static bool _actionMapSwitch;

    public static bool CovertInjectionComplete { get; set; }

    public static string TokenMode { get; set; }

    public static bool EasyTokenMiddlewareModeSwitch { get; set; }

    public static bool EasyTokenSecretOptionInjectionComplete { get; set; }

    public static bool EasyTokenSecretInjectionComplete { get; set; }

    public static bool EasyTokenConfigComplete { get; set; }

    public static bool JsonWebTokenConfigComplete { get; set; }

    public static bool JsonWebTokenMiddlewareModeSwitch { get; set; }

    public static bool PermissionVerificationSwitch { get; set; }

    public static bool PermissionVerificationMiddlewareModeSwitch { get; set; }

    public static bool ApplicationChannelInjectionComplete { get; set; }

    public static bool ApplicationChannelIsDefault { get; set; }

    public static IEnumerable<string> StartupUrls { get; set; }

    static FlagAssist()
    {
        _applicationRunPerformed = false;
        _advanceStaticFileOptionsSwitch = false;
        _logDashboardSwitch = false;
        _entityFrameworkSwitch = false;
        _healthChecksSwitch = false;
        _actionMapSwitch = false;

        CovertInjectionComplete = false;
        TokenMode = "";
        EasyTokenMiddlewareModeSwitch = false;
        EasyTokenSecretOptionInjectionComplete = false;
        EasyTokenSecretInjectionComplete = false;
        EasyTokenConfigComplete = false;
        JsonWebTokenConfigComplete = false;
        JsonWebTokenMiddlewareModeSwitch = false;
        PermissionVerificationSwitch = false;
        PermissionVerificationMiddlewareModeSwitch = false;
        ApplicationChannelInjectionComplete = false;
        ApplicationChannelIsDefault = false;
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

    public static void SetLogDashboardSwitchOpen()
    {
        _logDashboardSwitch = true;
    }

    public static bool GetLogDashboardSwitch()
    {
        return _logDashboardSwitch;
    }

    public static void SetEntityFrameworkSwitchOpen()
    {
        _entityFrameworkSwitch = true;
    }

    public static bool GetEntityFrameworkSwitch()
    {
        return _entityFrameworkSwitch;
    }

    public static void SetHealthChecksSwitchOpen()
    {
        _healthChecksSwitch = true;
    }

    public static bool GetHealthChecksSwitch()
    {
        return _healthChecksSwitch;
    }

    public static void SetActionMapSwitchOpen()
    {
        _actionMapSwitch = true;
    }

    public static bool GetActionMapSwitch()
    {
        return _actionMapSwitch;
    }
}