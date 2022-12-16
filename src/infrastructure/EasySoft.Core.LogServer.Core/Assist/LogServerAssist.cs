using EasySoft.Core.Cap.ExtensionMethods;
using EasySoft.Core.LogServer.Core.Entities;
using EasySoft.Core.LogServer.Core.Services.Implementations;
using EasySoft.Core.LogServer.Core.Services.Interfaces;
using EasySoft.Core.LogServer.Core.Subscribers;

namespace EasySoft.Core.LogServer.Core.Assist;

public static class LogServerAssist
{
    public static void Init()
    {
        ContextConfigure.AddEntityConfigureAssembly(typeof(ErrorLog).Assembly);

        BusinessServiceConfigure.AddBusinessServiceInterfaceAssembly(typeof(IErrorLogService).Assembly);
        BusinessServiceConfigure.AddBusinessServiceImplementationAssembly(typeof(ErrorLogService).Assembly);

        // 配置额外的构建项目
        ApplicationConfigurator.AddWebApplicationBuilderExtraActions(
            new ExtraAction<WebApplicationBuilder>()
                .SetName("")
                .SetAction(applicationBuilder => { applicationBuilder.AddCapSubscriber<ErrorLogExchangeSubscriber>(); })
        );
    }
}