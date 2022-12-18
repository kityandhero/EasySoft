using EasySoft.Core.Infrastructure.Configures;
using EasySoft.Core.PermissionVerification.Assists;
using EasySoft.Core.PermissionVerification.Configures;

namespace EasySoft.Core.PermissionVerification.Extensions;

/// <summary>
/// WebApplicationExtensions
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// UseScanPermission
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    internal static WebApplication UseScanPermission(
        this WebApplication application
    )
    {
        if (!GeneralConfigAssist.GetAccessWayDetectSwitch()) return application;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseScanPermission)}."
        );

        ApplicationConfigurator.OnApplicationStart += async serviceProvider =>
        {
            PermissionConfigure.ScanPermissionAssemblies.ForEach(PermissionAssists.ScanPermission);

            await PermissionAssists.StartSaveAsync();
        };

        return application;
    }
}