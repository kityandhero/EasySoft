using EasySoft.Core.AppSecurityVerification.Detectors;
using EasySoft.Core.AppSecurityVerification.Detectors.Interfaces;

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
                await appSecurityDetector.ChannelCheck();

                await appSecurityDetector.CredentialVerify();
            }
            catch (Exception e)
            {
                application.Logger.LogAdvanceException(e);
            }
        };

        return application;
    }
}