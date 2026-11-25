using EasySoft.Core.PermissionVerification.Extensions;
using EasySoft.Simple.Tradition.Client.WebApi.EventSubscribers;
using EasySoft.UtilityTools.AspNetCore.Configures;
using EasySoft.UtilityTools.Core.Configures;
using EasySoft.UtilityTools.Core.Startup;

namespace EasySoft.Simple.Tradition.Client.WebApi;

public class StartUpConfigure : IStartUpConfigure
{
    public void Init()
    {
        ApplicationAssist.Init();

        // 配置额外的构建项目
        WebApplicationConfigure.AddWebApplicationBuilderExtraActions(
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddPermissionVerification")
                .SetAction(applicationBuilder =>
                    {
                        applicationBuilder.AddPermissionVerification<ApplicationPermissionObserver>();
                    }
                ),
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddCapEventSubscriber")
                .SetAction(applicationBuilder => { applicationBuilder.AddCapSubscriber<CapEventSubscriber>(); })
        );
    }
}