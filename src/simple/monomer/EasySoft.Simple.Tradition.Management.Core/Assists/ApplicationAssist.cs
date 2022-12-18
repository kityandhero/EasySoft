using EasySoft.Core.AppSecurityVerification.Extensions;
using EasySoft.Core.Infrastructure.Configures;
using EasySoft.Core.PermissionVerification.Extensions;

namespace EasySoft.Simple.Tradition.Management.Core.Assists;

public static class ApplicationAssist
{
    public static void InitManagement()
    {
        ApplicationConfig.ApplicationAssist.Init();

        // 配置额外的构建项目
        ApplicationConfigurator.AddWebApplicationBuilderExtraActions(
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddPermissionServer")
                .SetAction(applicationBuilder => { applicationBuilder.AddAppSecurityVerification(); }),
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddPermissionServer")
                .SetAction(applicationBuilder =>
                {
                    applicationBuilder.AddPermissionVerification<ApplicationPermissionObserver>();
                })
        );
    }
}