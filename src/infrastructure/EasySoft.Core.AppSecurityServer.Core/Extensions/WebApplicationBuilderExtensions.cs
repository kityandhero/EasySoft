using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
internal static class WebApplicationBuilderExtensions
{
    private const int DetectionInterval = 60000;

    /// <summary>
    /// add permission server core logic
    /// </summary>
    /// <param name="builder"></param>    
    /// <returns></returns>
    internal static WebApplicationBuilder AddAppSecurityServerCore(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAppSecurityServerCore)}."
        );

        builder.AddDetectionAppPublicKey();

        return builder;
    }

    /// <summary>
    /// add permission server core logic
    /// </summary>
    /// <param name="builder"></param>    
    /// <returns></returns>
    private static WebApplicationBuilder AddDetectionAppPublicKey(
        this WebApplicationBuilder builder
    )
    {
        ApplicationConfigure.OnApplicationStart += async (serviceProvider) =>
        {
            await MaintainMasterControl(serviceProvider);
            await DetectionAppPublicKey(serviceProvider, DateTime.Now);
        };

        ApplicationConfigure.AddTimer(DetectionInterval, DetectionAppPublicKey);

        return builder;
    }

    private static async void DetectionAppPublicKey(IServiceProvider serviceProvider, ElapsedEventArgs e)
    {
        await DetectionAppPublicKey(serviceProvider, e.SignalTime);
    }

    private static async Task DetectionAppPublicKey(IServiceProvider serviceProvider, DateTime time)
    {
        var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

        if (environment.IsDevelopment())
        {
            var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<object>();

            logger.LogAdvancePrompt(
                $"Detection app public key, interval {DetectionInterval} , time {time.ToYearMonthDayHourMinuteSecond()}.");
        }

        var appPublicKeyService = serviceProvider.GetRequiredService<IAppPublicKeyService>();

        await appPublicKeyService.DetectionAsync();
    }

    private static async Task MaintainMasterControl(IServiceProvider serviceProvider)
    {
        var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
        var appSecurityService = serviceProvider.GetRequiredService<IAppSecurityService>();

        if (environment.IsDevelopment())
        {
            var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<object>();

            logger.LogAdvancePrompt(
                "Maintain Master Control."
            );
        }

        await appSecurityService.MaintainMasterControlAsync();
    }
}