using EasySoft.UtilityTools.Core.Assists;

namespace EasySoft.Core.Hosting.Framework.Kernel.Extensions.HostBuilderExtensions;

public static class BuildExtensions
{
    public static IHost BuildWebApplication(
        this IHostBuilder builder
    )
    {
        var app = builder.Build();

        #region SetProvider

        StartupDescriptionMessageAssist.AddPrompt(
            $"SetProvider after build application."
        );

        ServiceProviderAssist.SetProvider(app.Services);

        #endregion

        FlagAssist.SetApplicationRunPerformed();

        // LoggerFactoryAssist.SetLoggerFactory(app.Services.GetRequiredService<ILoggerFactory>());

        // LoggerAssist.SetLogger(app.Logger);

        // StartupConfigMessageAssist.AddConfig(
        //     $"Environment is {EnvironmentAssist.GetEnvironment().EnvironmentName}."
        // );

        // StartupConfigMessageAssist.AddConfig(
        //     $"EnvironmentAlias is {EnvironmentAssist.GetEnvironmentAliasName()}."
        // );

        // StartupConfigMessageAssist.AddConfig(
        //     $"ContentRootPath is \"{EnvironmentAssist.GetEnvironment().ContentRootPath}\"."
        // );
        //
        // StartupDescriptionMessageAssist.AddPrompt(
        //     $"Application start completed{(!FlagAssist.StartupUrls.Any() ? "." : $" at {FlagAssist.StartupUrls.Join(" ")}.")}"
        // );

        #region Startup Info Print

        StartupBuilderExtraActionMessageAssist.Print();

        StartupApplicationExtraActionMessageAssist.Print();

        StartupMvcOptionExtraActionMessageAssist.Print();

        StartupEndPointExtraActionMessageAssist.Print();

        StartupConfigMessageAssist.Print();

        StartupDescriptionMessageAssist.Print();

        #endregion

        // ------------------------

        LoggerAssist.Hint(
            AuxiliaryConfigure.BuildHintMessage().ToArray()
        );

        #region Startup Warn Print

        StartupWarnMessageAssist.Print();

        #endregion

        // app.RegisterWorks();

        return app;
    }
}