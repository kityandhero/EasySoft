using EasySoft.Core.PermissionVerification.Detectors;
using EasySoft.Core.PermissionVerification.Detectors.Interfaces;
using EasySoft.UtilityTools.Core.Extensions;

namespace EasySoft.Core.PermissionVerification.NotificationHandlers;

/// <summary>
/// PermissionAccessReply
/// </summary>
public class PermissionAccessReply : INotificationHandler<PermissionAccessNotification>
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
    public PermissionAccessReply(
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
    public async Task Handle(PermissionAccessNotification notification, CancellationToken cancellationToken)
    {
        if (_environment.IsDevelopment())
            _loggerFactory.CreateLogger<object>().LogAdvancePrompt(
                $"Receive {nameof(PermissionAccessNotification)} -> {notification.BuildInfo()}"
            );

        var accessWayPersistence = notification.AccessWay;

        var accessWayModels = await _accessWayDetector.Find(accessWayPersistence.GuidTag);

        if (accessWayModels.Count <= 0)
        {
            await AutofacAssist.Instance.Resolve<IAccessWayProducer>().SendAsync(accessWayPersistence);

            if (_environment.IsDevelopment())
            {
                var logger = _loggerFactory.CreateLogger<PermissionAccessReply>();

                logger.LogAdvancePrompt(
                    $"Permission -> {accessWayPersistence.BuildInfo()} has not existed, send to queue by producer."
                );
            }
        }
        else
        {
            var accessWayModel = accessWayModels[0];

            if (accessWayModel.Name.ToLower() == accessWayPersistence.Name.ToLower() &&
                accessWayModel.RelativePath.ToLower() == accessWayPersistence.RelativePath.ToLower() &&
                accessWayModel.Expand.ToLower() == accessWayPersistence.Expand.ToLower() &&
                accessWayModel.Channel == accessWayPersistence.Channel)
            {
                if (!_environment.IsDevelopment()) return;

                var logger = _loggerFactory.CreateLogger<PermissionAccessReply>();

                logger.LogAdvancePrompt(
                    $"Permission -> {accessWayPersistence.BuildInfo()} has existed and not changed, ignore send."
                );

                return;
            }

            await AutofacAssist.Instance.Resolve<IAccessWayProducer>().SendAsync(accessWayPersistence);

            if (_environment.IsDevelopment())
            {
                var logger = _loggerFactory.CreateLogger<PermissionAccessReply>();

                logger.LogAdvancePrompt(
                    $"Permission -> {accessWayPersistence.BuildInfo()} has existed and changed, send to queue by producer."
                );
            }
        }
    }
}