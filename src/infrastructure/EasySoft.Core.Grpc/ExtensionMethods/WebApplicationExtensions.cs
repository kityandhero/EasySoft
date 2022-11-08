using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Grpc.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication MapAdvanceGrpcService<TService>(
        this WebApplication application
    ) where TService : class
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapAdvanceGrpcService)}<{typeof(TService).Name}>()."
        );

        application.MapGrpcService<TService>();

        return application;
    }
}