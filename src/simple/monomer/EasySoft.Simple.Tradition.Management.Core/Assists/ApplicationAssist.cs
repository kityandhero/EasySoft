﻿namespace EasySoft.Simple.Tradition.Management.Core.Assists;

public static class ApplicationAssist
{
    public static void InitManagement()
    {
        EasySoft.Simple.Tradition.ApplicationConfig.ApplicationAssist.Init();

        // 配置额外的构建项目
        ApplicationConfigurator.AddWebApplicationBuilderExtraActions(
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddPermissionServer")
                .SetAction(applicationBuilder =>
                {
                    applicationBuilder.AddPermissionVerification<ApplicationPermissionObserver>();
                })
        );
    }
}