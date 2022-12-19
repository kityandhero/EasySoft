using EasySoft.Core.PermissionVerification.Assists;
using EasySoft.Core.PermissionVerification.Entities;
using EasySoft.Core.PermissionVerification.Extensions;
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

    /// <summary>
    /// Mediator
    /// </summary>
    protected IMediator Mediator { get; }

    private IApplicationChannel Channel { get; }

    /// <summary>
    /// AccessPermission
    /// </summary>
    protected readonly AccessPermission AccessPermission;

    /// <summary>
    /// AccessWayOfficer
    /// </summary>
    protected AccessWayOfficer(
        ILoggerFactory loggerFactory,
        IWebHostEnvironment environment,
        IMediator mediator
    )
    {
        LoggerFactory = loggerFactory;
        Environment = environment;
        Mediator = mediator;

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
    private string GetChannelName()
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

        CompetenceCollection.GetInstance().SetCompetenceSets(
            CompetenceCollection.BuildCompetenceKey(AccessPermission.Url),
            AccessPermission.Competence
        );

        var accessWayModel = AccessPermission.ToAccessWayModel();

        await PermissionAssists.AddAccessWayModelScanHistory(Mediator, accessWayModel.GuidTag, accessWayModel);
    }
}