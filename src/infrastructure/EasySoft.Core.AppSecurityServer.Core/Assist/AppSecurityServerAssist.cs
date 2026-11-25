using EasySoft.Core.AppSecurityServer.Core.Entities;
using EasySoft.Core.AppSecurityServer.Core.Extensions;
using EasySoft.Core.AppSecurityServer.Core.Services.Implements;
using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;
using EasySoft.Core.PermissionVerification.Configures;
using EasySoft.UtilityTools.AspNetCore.Configures;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Startup;

namespace EasySoft.Core.AppSecurityServer.Core.Assist;

/// <summary>
/// PermissionServerAssist
/// </summary>
public static class AppSecurityServerAssist
{
    /// <summary>
    /// Init
    /// </summary>
    public static void Init(bool embedMode)
    {
        PermissionConfigure.AddRangeScanPermissionAssemblies(
            new List<Assembly>
            {
                typeof(AppSecurity).Assembly
            }
        );

        ApplicationSecurityServerConfigure.EmbedMode = embedMode;
        ContextConfigure.AddEntityConfigureAssembly(typeof(AppSecurity).Assembly);

        BusinessServiceConfigure.AddBusinessServiceInterfaceAssembly(typeof(IAppSecurityService).Assembly);
        BusinessServiceConfigure.AddBusinessServiceImplementationAssembly(typeof(AppSecurityService).Assembly);

        // 配置额外的构建项目
        WebApplicationConfigure.AddWebApplicationBuilderExtraActions(
            new ExtraAction<WebApplicationBuilder>()
                .SetName("")
                .SetAction(applicationBuilder => { applicationBuilder.AddAppSecurityServerCore(); })
        );
    }
}