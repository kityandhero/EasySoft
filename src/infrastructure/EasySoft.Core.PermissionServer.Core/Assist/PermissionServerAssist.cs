using EasySoft.Core.Data.Configures;
using EasySoft.Core.PermissionServer.Core.Entities;
using EasySoft.Core.PermissionServer.Core.Services.Implementations;
using EasySoft.Core.PermissionServer.Core.Services.Interfaces;
using EasySoft.Core.PermissionServer.Core.Subscribers;

namespace EasySoft.Core.PermissionServer.Core.Assist;

public static class PermissionServerAssist
{
    public static void Init()
    {
        ContextConfigure.AddEntityConfigureAssembly(typeof(RoleGroup).Assembly);

        BusinessServiceConfigure.AddBusinessServiceInterfaceAssembly(typeof(ISecurityService).Assembly);
        BusinessServiceConfigure.AddBusinessServiceImplementationAssembly(typeof(SecurityService).Assembly);

        // 配置额外的构建项目
        ApplicationConfigurator.AddWebApplicationBuilderExtraActions(
            new ExtraAction<WebApplicationBuilder>()
                .SetName("")
                .SetAction(applicationBuilder =>
                {
                    applicationBuilder.AddCapSubscriber<AccessWayExchangeSubscriber>();
                })
        );
    }
}