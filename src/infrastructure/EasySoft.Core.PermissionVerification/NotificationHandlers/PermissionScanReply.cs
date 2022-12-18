using EasySoft.Core.PermissionVerification.Assists;
using EasySoft.Core.PermissionVerification.Detectors;
using EasySoft.UtilityTools.Core.Extensions;

namespace EasySoft.Core.PermissionVerification.NotificationHandlers;

/// <summary>
/// 权限扫描应答
/// </summary>
public class PermissionScanReply : INotificationHandler<AppSecurityFirstVerifyNotification>
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IWebHostEnvironment _environment;
    private readonly IAccessWayDetector _accessWayDetector;

    /// <summary>
    /// 权限扫描应答
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="environment"></param>
    /// <param name="accessWayDetector"></param>
    public PermissionScanReply(
        ILoggerFactory loggerFactory,
        IWebHostEnvironment environment,
        IAccessWayDetector accessWayDetector
    )
    {
        _loggerFactory = loggerFactory;
        _environment = environment;
        _accessWayDetector = accessWayDetector;
    }

    /// <inheritdoc />
    public async Task Handle(AppSecurityFirstVerifyNotification notification, CancellationToken cancellationToken)
    {
        if (_environment.IsDevelopment())
            _loggerFactory.CreateLogger<object>().LogAdvancePrompt(
                $"Receive {nameof(AppSecurityFirstVerifyNotification)} -> {notification.BuildInfo()}"
            );

        if (!notification.VerifyResult)
            return;

        await PermissionAssists.StartSaveAsync(_accessWayDetector);
    }
}