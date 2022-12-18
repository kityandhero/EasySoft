using EasySoft.Core.Infrastructure.Configures;

namespace EasySoft.Core.EnvironmentAuxiliary.ExtensionMethods;

public static class WebApplicationExtensions
{
    private const string UniqueIdentifierUseAdvanceEnvironmentAuxiliary = "f6bbe5f3-1393-4f6b-8afc-6bed7fa07eec";

    private const string UniqueIdentifierUseAdvanceViewConfig = "556f1ead-72fd-44ea-a6ec-e3e4f061c228";

    private const string UniqueIdentifierUseAdvanceAssemblyMap = "9ac4379a-6a40-42a0-ad14-2d2a45bf67b5";

    private const string UniqueIdentifierUseAdvanceActionMap = "c0632be6-5936-4d37-9c85-9282d1838b45";

    public static WebApplication UseAdvanceEnvironmentAuxiliary(
        this WebApplication application
    )
    {
        if (application.HasUsed(UniqueIdentifierUseAdvanceEnvironmentAuxiliary))
            return application;

        if (!EnvironmentAssist.IsDevelopment()) return application;

        application.UseAdvanceViewConfig();
        application.UseAdvanceAssemblyMap();
        application.UseAdvanceActionMap();

        return application;
    }

    public static WebApplication UseAdvanceViewConfig(
        this WebApplication application
    )
    {
        if (application.HasUsed(UniqueIdentifierUseAdvanceViewConfig))
            return application;

        if (!EnvironmentAssist.IsDevelopment()) return application;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseAdvanceViewConfig)}."
        );

        application.UseViewConfig(x => x.RenderPage());

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {FlagAssist.StartupDisplayUrls.Select(o => $"{o}/viewConfig ").Join(" ")} to get info such as appSetting, display only in view development mode."
        );

        return application;
    }

    public static WebApplication UseAdvanceAssemblyMap(
        this WebApplication application
    )
    {
        if (application.HasUsed(UniqueIdentifierUseAdvanceAssemblyMap))
            return application;

        if (!EnvironmentAssist.IsDevelopment()) return application;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseAdvanceAssemblyMap)}."
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapAssemblyMap(); })
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
}