namespace EasySoft.Core.Grpc.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication MapAdvanceGrpcService<TService>(
        this WebApplication application
    ) where TService : class
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapAdvanceGrpcService)}<{typeof(TService).Name}>."
        );

        application.MapGrpcService<TService>();

        return application;
    }
}