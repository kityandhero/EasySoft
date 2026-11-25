using EasySoft.UtilityTools.AspNetCore.Configures;
using EasySoft.UtilityTools.AspNetCore.Extensions;
using EasySoft.UtilityTools.Core.Startup;

namespace EasySoft.Core.Config.ExtensionMethods;

/// <summary>
/// WebApplicationExtensions
/// </summary>
public static class WebApplicationExtensions
{
    private const string IdentifierUseDevelopAuxiliary = "e84ad243-6571-4e74-aa77-02a3a8bb4ef1";

    /// <summary>
    /// UseConfigureTemplate
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    public static WebApplication UseConfigureTemplate(
        this WebApplication application
    )
    {
        if (application.HasUsed(IdentifierUseDevelopAuxiliary))
        {
            return application;
        }

        if (!HostEnvironmentAssist.IsDevelopment())
        {
            return application;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseConfigureTemplate)}."
        );

        WebApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapDatabaseConfigFile(); })
        );

        WebApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapDevelopConfigFile(); })
        );

        WebApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapElasticSearchConfigFile(); })
        );

        WebApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapGeneralConfigFile(); })
        );

        WebApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapMongoConfigFile(); })
        );

        WebApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapRabbitMQConfigFile(); })
        );

        return application;
    }
}