using EasySoft.Core.Config.Options;
using EasySoft.Core.Infrastructure.Configures;
using EasySoft.Core.Infrastructure.Startup;

namespace EasySoft.Core.Config.ExtensionMethods;

public static class WebApplicationExtensions
{
    private const string UniqueIdentifierUseDevelopAuxiliary = "e84ad243-6571-4e74-aa77-02a3a8bb4ef1";

    public static WebApplication UseAdvanceStaticFiles(
        this WebApplication application
    )
    {
        if (!application.UseHostFiltering().ApplicationServices.GetAutofacRoot()
                .IsRegistered<AdvanceStaticFileOptions>())
        {
            application.UseStaticFiles();
        }
        else
        {
            var staticFileOptions = application.UseHostFiltering().ApplicationServices.GetAutofacRoot()
                .Resolve<AdvanceStaticFileOptions>();

            application.UseStaticFiles(staticFileOptions);

            FlagAssist.SetAdvanceStaticFileOptionsSwitchOpen();
        }

        return application;
    }

    public static WebApplication UseConfigureTemplate(
        this WebApplication application
    )
    {
        if (!AuxiliaryConfigure.PromptConfigFileInfo) return application;

        if (application.HasUsed(UniqueIdentifierUseDevelopAuxiliary))
            return application;

        if (!EnvironmentAssist.IsDevelopment()) return application;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseConfigureTemplate)}."
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapDatabaseConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapDevelopConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapElasticSearchConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapGeneralConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapMongoConfigFile(); })
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapRabbitMQConfigFile(); })
        );

        return application;
    }
}