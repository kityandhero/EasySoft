using EasySoft.Core.Permission.Server.Detectors;
using EasySoft.Core.Permission.Server.Observers;
using EasySoft.Core.Permission.Server.Services.Implementations;

namespace EasySoft.Core.Permission.Server.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddPermissionServer = "0cf17112-dc65-42ce-a5c0-7e679f1868a4";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddPermissionServer(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddPermissionServer))
            return builder;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddPermissionServer)}."
        );

        builder.AddAssemblyBusinessServices(
            typeof(ISecurityService).Assembly,
            typeof(SecurityService).Assembly
        );

        builder.AddPermissionVerification<ApplicationPermissionObserver>();

        builder.Services.AddTransient<IAccessWayDetector, AccessWayDetector>();

        return builder;
    }
}