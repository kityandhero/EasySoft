using EasySoft.Core.PermissionServer.Core.Entities;
using EasySoft.Core.PermissionServer.Core.Extensions;
using EasySoft.Core.PermissionServer.Core.Services.Implements;
using EasySoft.Core.PermissionServer.Core.Services.Interfaces;
using EasySoft.Core.PermissionServer.Core.Subscribers;

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
        PermissionServerConfigure.EmbedMode = embedMode;

        ContextConfigure.AddEntityConfigureAssembly(typeof(RoleGroup).Assembly);

        BusinessServiceConfigure.AddBusinessServiceInterfaceAssembly(typeof(IPermissionService).Assembly);
        BusinessServiceConfigure.AddBusinessServiceImplementationAssembly(typeof(PermissionService).Assembly);

        // 配置额外的构建项目
        ApplicationConfigure.AddWebApplicationBuilderExtraActions(
            new ExtraAction<WebApplicationBuilder>()
                .SetName("")
                .SetAction(applicationBuilder => { applicationBuilder.AddPermissionServerCore(); })
        );
    }
}