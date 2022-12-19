using EasySoft.Core.AppSecurityVerification.Detectors;

namespace EasySoft.Core.AppSecurityVerification.Extensions;

/// <summary>  
/// WebApplicationExtensions
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// UseScanPermission
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    internal static WebApplication UseAppSecurityFirstVerify(
        this WebApplication application
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseAppSecurityFirstVerify)}."
        );

        ApplicationConfigure.OnApplicationStart += async serviceProvider =>
        {
            var appSecurityDetector = serviceProvider.GetRequiredService<IAppSecurityDetector>();

            if (application.Environment.IsDevelopment())
                application.Logger.LogAdvancePrompt(
                    "Do first app security detector"
                );

            try
            {
                await appSecurityDetector.Verify();
            }
            catch (Exception e)
            {
                application.Logger.LogAdvanceException(e);
            }
        };

        return application;
    }
}