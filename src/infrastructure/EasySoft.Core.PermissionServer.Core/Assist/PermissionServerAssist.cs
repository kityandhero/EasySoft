using EasySoft.Core.PermissionServer.Core.Entities;
using EasySoft.Core.PermissionServer.Core.Extensions;
using EasySoft.Core.PermissionServer.Core.Services.Implements;
using EasySoft.Core.PermissionServer.Core.Services.Interfaces;
using EasySoft.Core.PermissionServer.Core.Subscribers;
using EasySoft.Core.PermissionVerification.Configures;

namespace EasySoft.Core.PermissionServer.Core.Assist;

/// <summary>
/// PermissionServerAssist
/// </summary>
public static class PermissionServerAssist
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="embedMode">是否使用嵌入模式，嵌入模式不会执行请求校验</param>
    public static void Init(bool embedMode)
    {
        PermissionConfigure.AddRangeScanPermissionAssemblies(new List<Assembly>
        {
            typeof(PresetRole).Assembly
        });

        PermissionServerConfigure.EmbedMode = embedMode;

        ContextConfigure.AddEntityConfigureAssembly(typeof(RoleGroup).Assembly);

        BusinessServiceConfigure.AddBusinessServiceInterfaceAssembly(typeof(IRpcService).Assembly);
        BusinessServiceConfigure.AddBusinessServiceImplementationAssembly(typeof(RpcService).Assembly);

        // 配置额外的构建项目
        ApplicationConfigure.AddWebApplicationBuilderExtraActions(
            new ExtraAction<WebApplicationBuilder>()
                .SetName("")
                .SetAction(applicationBuilder => { applicationBuilder.AddPermissionServerCore(); })
        );
    }
}