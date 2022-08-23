using Autofac;
using EasySoft.Core.AccessWayTransmitter.Producers;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Channels;
using EasySoft.Core.PermissionVerification.Detectors;
using EasySoft.Core.PermissionVerification.Entities;
using EasySoft.UtilityTools.Standard.Competence;

namespace EasySoft.Core.PermissionVerification.Officers;

public abstract class AccessWayOfficer : OfficerCore
{
    private IApplicationChannel Channel { get; }

    protected readonly AccessPermission AccessPermission;

    protected AccessWayOfficer()
    {
        AccessPermission = new AccessPermission();
        Channel = AutofacAssist.Instance.Container.Resolve<IApplicationChannel>();
    }

    private int GetChannel()
    {
        return Channel.GetChannel();
    }

    protected string GetChannelName()
    {
        return Channel.GetName();
    }

    protected void CollectAccessWay()
    {
        if (!GeneralConfigAssist.GetAccessWayDetectSwitch())
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(AccessPermission.GuidTag))
        {
            return;
        }

        var channel = GetChannel();

        CompetenceCollection.GetInstance().SetCompetenceSets(
            CompetenceCollection.BuildCompetenceKey(AccessPermission.Url),
            AccessPermission.Competence
        );

        var accessWayDetector = AutofacAssist.Instance.Container.Resolve<IAccessWayDetector>();

        var accessWay = accessWayDetector.Find(AccessPermission.GuidTag);

        if (accessWay == null)
        {
            AutofacAssist.Instance.Container.Resolve<IAccessWayProducer>().Send(
                AccessPermission.GuidTag,
                AccessPermission.Name,
                AccessPermission.Path,
                AccessPermission.Competence
            );
        }
        else
        {
            if (accessWay.Name == AccessPermission.Name && accessWay.RelativePath == AccessPermission.Path &&
                accessWay.Expand == AccessPermission.Competence &&
                accessWay.Channel == channel)
            {
                return;
            }

            AutofacAssist.Instance.Container.Resolve<IAccessWayProducer>().Send(
                AccessPermission.GuidTag,
                AccessPermission.Name,
                AccessPermission.Path,
                AccessPermission.Competence
            );
        }
    }
}