using EasySoft.Core.AppSecurityVerification.Extensions;
using EasySoft.Core.PermissionVerification.Extensions;
using EasySoft.UtilityTools.AspNetCore.Configures;
using EasySoft.UtilityTools.Core.Startup;

namespace EasySoft.Simple.Tradition.Management.Core.Assists;

public static class ApplicationAssist
{
    public static void InitManagement()
    {
        ApplicationConfig.ApplicationAssist.Init();

        // 配置额外的构建项目
        WebApplicationConfigure.AddWebApplicationBuilderExtraActions(
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddPermissionServer")
                .SetAction(applicationBuilder => { applicationBuilder.AddAppSecurityVerification(); }),
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddPermissionServer")
                .SetAction(applicationBuilder =>
                    {
                        applicationBuilder.AddPermissionVerification<ApplicationPermissionObserver>();
                    }
                )
        );
    }
}