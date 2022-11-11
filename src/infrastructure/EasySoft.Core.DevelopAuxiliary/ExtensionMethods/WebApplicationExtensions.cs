using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.ExtensionMethods;

namespace EasySoft.Core.DevelopAuxiliary.ExtensionMethods;

public static class WebApplicationExtensions
{
    private const string UniqueIdentifierUseAdvanceViewConfig = "556f1ead-72fd-44ea-a6ec-e3e4f061c228";

    private const string UniqueIdentifierUseAdvanceActionMap = "c0632be6-5936-4d37-9c85-9282d1838b45";

    private const string UniqueIdentifierUseDevelopAuxiliary = "e84ad243-6571-4e74-aa77-02a3a8bb4ef1";

    public static WebApplication UseAdvanceViewConfig(
        this WebApplication application
    )
    {
        if (application.HasUsed(UniqueIdentifierUseAdvanceViewConfig))
            return application;

        if (!EnvironmentAssist.IsDevelopment()) return application;

        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseConfigureTemplate)}."
        );

        application.UseViewConfig(x => x.RenderPage());

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {FlagAssist.StartupUrls.Select(o => $"{o}/viewConfig ").Join(" ")} to get info such as appSetting, display only in view development mode."
        );

        return application;
    }

    public static WebApplication UseAdvanceActionMap(
        this WebApplication application
    )
    {
        if (application.HasUsed(UniqueIdentifierUseAdvanceActionMap))
            return application;

        if (!EnvironmentAssist.IsDevelopment()) return application;

        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseAdvanceActionMap)}."
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapActionMap(); })
        );

        return application;
    }

    public static WebApplication UseConfigureTemplate(
        this WebApplication application
    )
    {
        if (application.HasUsed(UniqueIdentifierUseDevelopAuxiliary))
            return application;

        if (!EnvironmentAssist.IsDevelopment()) return application;

        StartupConfigMessageAssist.AddConfig(
            $"UseConfigureTemplate: enabled."
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