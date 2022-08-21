using System.ComponentModel;
using Autofac;
using EasySoft.Core.AccessWayTransmitter.Producers;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.IdentityVerification.Attributes;
using EasySoft.Core.IdentityVerification.Detectors;
using EasySoft.Core.Infrastructure.Channels;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.UtilityTools.Competence;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasySoft.Core.IdentityVerification.Filters;

public abstract class AccessWayFilter : BaseAuthorizationFilters
{
    private IApplicationChannel Channel { get; }

    protected AccessWayFilter()
    {
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

    [Description("获取Description(ActionResult的Description特性)")]
    protected string GetDescription(AuthorizationFilterContext filterContext)
    {
        //检测描述属性
        var actionDescription = filterContext.ActionDescriptor.GetAttribute<DescriptionAttribute>();

        return actionDescription.Description;
    }

    [Description("获取自定义扩展权限")]
    protected string GetCompetence(AuthorizationFilterContext filterContext)
    {
        var competenceConfig = filterContext.ActionDescriptor.GetAttribute<CompetenceConfigAttribute>();

        return competenceConfig.ToString();
    }

    [Description("GuidTag(ActionResult的GuidTag特性)")]
    protected GuidTagAttribute? GetGuidTagAttribute(AuthorizationFilterContext filterContext)
    {
        if (!filterContext.ActionDescriptor.ContainAttribute<GuidTagAttribute>())
        {
            return null;
        }

        //检测GuidTag属性
        var guidTagAttribute = filterContext.ActionDescriptor.GetAttribute<GuidTagAttribute>();

        return guidTagAttribute;
    }

    protected string GetGuidTag(AuthorizationFilterContext filterContext)
    {
        //检测GuidTag属性
        var guidTagAttribute = GetGuidTagAttribute(filterContext);

        return guidTagAttribute?.GuidTag ?? "";
    }

    protected void CheckAccessWay(AuthorizationFilterContext filterContext)
    {
        if (!GeneralConfigAssist.GetAccessWayDetectSwitch())
        {
            return;
        }

        var name = GetDescription(filterContext);
        var competence = GetCompetence(filterContext);
        var guidTag = GetGuidTag(filterContext);

        if (string.IsNullOrWhiteSpace(guidTag))
        {
            return;
        }

        var url = filterContext.HttpContext.Request.GetUrl();
        var path = filterContext.HttpContext.Request.GetAbsolutePath();
        var channel = GetChannel();

        CompetenceCollection.GetInstance().SetCompetenceSets(
            CompetenceCollection.BuildCompetenceKey(url),
            competence
        );

        var accessWayDetector = AutofacAssist.Instance.Container.Resolve<IAccessWayDetector>();

        var accessWay = accessWayDetector.Find(guidTag);

        if (accessWay == null)
        {
            AutofacAssist.Instance.Container.Resolve<IAccessWayProducer>().Send(
                guidTag,
                name,
                path,
                competence
            );
        }
        else
        {
            if (accessWay.Name == name && accessWay.RelativePath == path && accessWay.Expand == competence &&
                accessWay.Channel == channel)
            {
                return;
            }

            AutofacAssist.Instance.Container.Resolve<IAccessWayProducer>().Send(
                guidTag,
                name,
                path,
                competence
            );
        }
    }
}