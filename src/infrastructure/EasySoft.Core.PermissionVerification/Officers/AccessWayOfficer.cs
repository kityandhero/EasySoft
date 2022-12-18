using EasySoft.Core.PermissionVerification.Detectors;
using EasySoft.Core.PermissionVerification.Entities;
using EasySoft.UtilityTools.Core.Extensions;

namespace EasySoft.Core.PermissionVerification.Officers;

/// <summary>
/// AccessWayOfficer
/// </summary>
public abstract class AccessWayOfficer : OfficerCore
{
    /// <summary>
    /// LoggerFactory
    /// </summary>
    protected ILoggerFactory LoggerFactory { get; }

    /// <summary>
    /// Environment
    /// </summary>
    protected IWebHostEnvironment Environment { get; }

    private IApplicationChannel Channel { get; }

    /// <summary>
    /// AccessPermission
    /// </summary>
    protected readonly AccessPermission AccessPermission;

    /// <summary>
    /// AccessWayOfficer
    /// </summary>
    protected AccessWayOfficer(ILoggerFactory loggerFactory, IWebHostEnvironment environment)
    {
        LoggerFactory = loggerFactory;
        Environment = environment;

        Channel = AutofacAssist.Instance.Resolve<IApplicationChannel>();
        AccessPermission = new AccessPermission
        {
            Channel = GetChannel(),
            ChannelName = GetChannelName()
        };
    }

    private int GetChannel()
    {
        return Channel.GetChannel();
    }

    /// <summary>
    /// GetChannelName
    /// </summary>
    /// <returns></returns>
    protected string GetChannelName()
    {
        return Channel.GetName();
    }

    /// <summary>
    /// CollectAccessWay
    /// </summary>
    protected async Task CollectAccessWay()
    {
        if (!GeneralConfigAssist.GetAccessWayDetectSwitch()) return;

        if (string.IsNullOrWhiteSpace(AccessPermission.GuidTag)) return;

        if (Environment.IsDevelopment())
        {
            var logger = LoggerFactory.CreateLogger<AccessWayOfficer>();

            logger.LogAdvanceExecute(
                $"{nameof(AccessWayOfficer)}.{nameof(CollectAccessWay)}"
            );

            logger.LogAdvancePrompt(
                $"{nameof(AccessPermission)} is \"{AccessPermission.Name}, {AccessPermission.GuidTag}({AccessPermission.Path}), {AccessPermission.ChannelName}({AccessPermission.Channel})\"."
            );
        }

        var channel = GetChannel();

        CompetenceCollection.GetInstance().SetCompetenceSets(
            CompetenceCollection.BuildCompetenceKey(AccessPermission.Url),
            AccessPermission.Competence
        );

        var accessWayDetector = AutofacAssist.Instance.Resolve<IAccessWayDetector>();

        var accessWayModels = await accessWayDetector.Find(AccessPermission.GuidTag);

        if (accessWayModels.Count <= 0)
        {
            await AutofacAssist.Instance.Resolve<IAccessWayProducer>().SendAsync(
                AccessPermission.GuidTag,
                AccessPermission.Name,
                AccessPermission.Path,
                AccessPermission.Competence
            );

            if (Environment.IsDevelopment())
            {
                var logger = LoggerFactory.CreateLogger<AccessWayOfficer>();

                logger.LogAdvancePrompt(
                    $"{nameof(AccessPermission)} {nameof(AccessPermission.GuidTag)} \"{AccessPermission.GuidTag}\" has not existed, send to queue by producer."
                );
            }
        }
        else
        {
            var accessWayModel = accessWayModels[0];

            if (accessWayModel.Name.ToLower() == AccessPermission.Name.ToLower() &&
                accessWayModel.RelativePath.ToLower() == AccessPermission.Path.ToLower() &&
                accessWayModel.Expand.ToLower() == AccessPermission.Competence.ToLower() &&
                accessWayModel.Channel == channel)
            {
                if (!Environment.IsDevelopment()) return;

                var logger = LoggerFactory.CreateLogger<AccessWayOfficer>();

                logger.LogAdvancePrompt(
                    $"{nameof(AccessPermission)} {nameof(AccessPermission.GuidTag)} \"{AccessPermission.GuidTag}\" has existed and not changed, ignore send."
                );

                return;
            }

            await AutofacAssist.Instance.Resolve<IAccessWayProducer>().SendAsync(
                AccessPermission.GuidTag,
                AccessPermission.Name,
                AccessPermission.Path,
                AccessPermission.Competence
            );

            if (Environment.IsDevelopment())
            {
                var logger = LoggerFactory.CreateLogger<AccessWayOfficer>();

                logger.LogAdvancePrompt(
                    $"{nameof(AccessPermission)} {nameof(AccessPermission.GuidTag)} \"{AccessPermission.GuidTag}\" has existed and changed, send to queue by producer."
                );
            }
        }
    }
}