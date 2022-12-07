using EasySoft.Core.PermissionVerification.Detectors;
using EasySoft.Core.PermissionVerification.Entities;

namespace EasySoft.Core.PermissionVerification.Officers;

/// <summary>
/// AccessWayOfficer
/// </summary>
public abstract class AccessWayOfficer : OfficerCore
{
    private IApplicationChannel Channel { get; }

    /// <summary>
    /// AccessPermission
    /// </summary>
    protected readonly AccessPermission AccessPermission;

    /// <summary>
    /// AccessWayOfficer
    /// </summary>
    protected AccessWayOfficer()
    {
        AccessPermission = new AccessPermission();
        Channel = AutofacAssist.Instance.Resolve<IApplicationChannel>();
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

        var channel = GetChannel();

        CompetenceCollection.GetInstance().SetCompetenceSets(
            CompetenceCollection.BuildCompetenceKey(AccessPermission.Url),
            AccessPermission.Competence
        );

        var accessWayDetector = AutofacAssist.Instance.Resolve<IAccessWayDetector>();

        var accessWayModels = await accessWayDetector.Find(AccessPermission.GuidTag);

        if (accessWayModels.Count <= 0)
        {
            AutofacAssist.Instance.Resolve<IAccessWayProducer>().Send(
                AccessPermission.GuidTag,
                AccessPermission.Name,
                AccessPermission.Path,
                AccessPermission.Competence
            );
        }
        else
        {
            var accessWayModel = accessWayModels[0];

            if (accessWayModel.Name == AccessPermission.Name &&
                accessWayModel.RelativePath == AccessPermission.Path &&
                accessWayModel.Expand == AccessPermission.Competence &&
                accessWayModel.Channel == channel)
                return;

            AutofacAssist.Instance.Resolve<IAccessWayProducer>().Send(
                AccessPermission.GuidTag,
                AccessPermission.Name,
                AccessPermission.Path,
                AccessPermission.Competence
            );
        }
    }
}