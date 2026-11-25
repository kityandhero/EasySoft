using EasySoft.Core.PermissionVerification.Assists;
using EasySoft.Core.PermissionVerification.Configures;
using EasySoft.UtilityTools.AspNetCore.Configures;

namespace EasySoft.Core.PermissionVerification.Extensions;

/// <summary>
/// HostExtensions
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
        var authorizationConfigureAssist = ServiceProviderAssist.Resolve<AuthorizationConfigureAssist>();

        if (!authorizationConfigureAssist.GetAccessWayDetectSwitch().Value)
        {
            return application;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseScanPermission)}."
        );

        WebApplicationConfigure.OnApplicationStart += _ =>
        {
            PermissionConfigure.ScanPermissionAssemblies.ForEach(PermissionAssists.ScanPermission);
        };

        return application;
    }
}