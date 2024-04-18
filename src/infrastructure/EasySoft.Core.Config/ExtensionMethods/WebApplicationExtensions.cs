using EasySoft.Core.Config.Options;
using EasySoft.Core.Infrastructure.Configures;
using EasySoft.Core.Infrastructure.Startup;

namespace EasySoft.Core.Config.ExtensionMethods;

/// <summary>
/// WebApplicationExtensions
/// </summary>
public static class WebApplicationExtensions
{
    private const string IdentifierUseDevelopAuxiliary = "e84ad243-6571-4e74-aa77-02a3a8bb4ef1";

    /// <summary>
    /// UseAdvanceStaticFiles
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    public static WebApplication UseAdvanceStaticFiles(
        this WebApplication application
    )
    {
        if (!application.UseHostFiltering()
                .ApplicationServices.GetAutofacRoot()
                .IsRegistered<AdvanceStaticFileOptions>())
        {
            application.UseStaticFiles();
        }
        else
        {
            var staticFileOptions = application.UseHostFiltering()
                .ApplicationServices.GetAutofacRoot()
                .Resolve<AdvanceStaticFileOptions>();

            application.UseStaticFiles(staticFileOptions);

            FlagAssist.SetAdvanceStaticFileOptionsSwitchOpen();
        }

        return application;
    }

    /// <summary>
    /// UseConfigureTemplate
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    public static WebApplication UseConfigureTemplate(
        this WebApplication application
    )
    {
        if (!AuxiliaryConfigure.PromptConfigFileInfo)
        {
            return application;
        }

        if (application.HasUsed(IdentifierUseDevelopAuxiliary))
        {
            return application;
        }

        if (!EnvironmentAssist.IsDevelopment())
        {
            return application;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseConfigureTemplate)}."
        );

        ApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapDatabaseConfigFile(); })
        );

        ApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapDevelopConfigFile(); })
        );

        ApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapElasticSearchConfigFile(); })
        );

        ApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapGeneralConfigFile(); })
        );

        ApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapMongoConfigFile(); })
        );

        ApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapRabbitMQConfigFile(); })
        );

        return application;
    }
}