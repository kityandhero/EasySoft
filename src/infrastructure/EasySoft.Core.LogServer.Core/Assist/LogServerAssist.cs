﻿using EasySoft.Core.LogServer.Core.Entities;
using EasySoft.Core.LogServer.Core.Extensions;
using EasySoft.Core.LogServer.Core.Services.Implements;
using EasySoft.Core.LogServer.Core.Services.Interfaces;

namespace EasySoft.Core.LogServer.Core.Assist;

/// <summary>
/// LogServerAssist
/// </summary>
public static class LogServerAssist
{
    /// <summary>
    /// Init
    /// </summary>
    public static void Init(bool embedMode)
    {
        PermissionConfigure.AddRangeScanPermissionAssemblies(new List<Assembly>
        {
            typeof(ErrorLog).Assembly
        });

        LogServerConfigure.EmbedMode = embedMode;
        ContextConfigure.AddEntityConfigureAssembly(typeof(ErrorLog).Assembly);

        BusinessServiceConfigure.AddBusinessServiceInterfaceAssembly(typeof(IErrorLogService).Assembly);
        BusinessServiceConfigure.AddBusinessServiceImplementationAssembly(typeof(ErrorLogService).Assembly);

        // 配置额外的构建项目
        ApplicationConfigure.AddWebApplicationBuilderExtraActions(
            new ExtraAction<WebApplicationBuilder>()
                .SetName("")
                .SetAction(applicationBuilder => { applicationBuilder.AddLogServerCore(); })
        );

        // 配置额外的应用项目
        ApplicationConfigure.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(applicationBuilder => { applicationBuilder.UseLogSendExperiment(); })
        );
    }
}