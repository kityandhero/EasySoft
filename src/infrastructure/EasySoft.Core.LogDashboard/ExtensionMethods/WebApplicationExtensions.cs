namespace EasySoft.Core.LogDashboard.ExtensionMethods;

public static class WebApplicationExtensions
{
    internal static WebApplication UseAdvanceLogDashboard(this WebApplication application)
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseAdvanceLogDashboard)}."
        );

        if (EnvironmentAssist.GetEnvironment().IsDevelopment()) application.UseDeveloperExceptionPage();

        application.UseLogDashboard();

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {(!FlagAssist.StartupUrls.Any() ? "https://[host]:[port]/LogDashboard" : FlagAssist.StartupUrls.Select(o => $"{o}/LogDashboard").Join(" "))} to visit LogDashboard."
        );

        return application;
    }
}