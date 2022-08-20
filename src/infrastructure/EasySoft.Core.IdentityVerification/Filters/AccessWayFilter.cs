using System.ComponentModel;
using Autofac;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.IdentityVerification.Attributes;
using EasySoft.Core.Infrastructure.Channels;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.UtilityTools.Competence;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasySoft.Core.IdentityVerification.Filters;

public abstract class AccessWayFilter : IAdvanceAuthorizationFilter
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
    private static string GetDescription(AuthorizationFilterContext filterContext)
    {
        //检测描述属性
        var actionDescription = filterContext.ActionDescriptor.GetAttribute<DescriptionAttribute>();

        return actionDescription.Description;
    }

    [Description("获取自定义扩展权限")]
    private static string GetCompetence(AuthorizationFilterContext filterContext)
    {
        var competenceConfig = filterContext.ActionDescriptor.GetAttribute<CompetenceConfigAttribute>();

        return competenceConfig.ToString();
    }

    [Description("GuidTag(ActionResult的GuidTag特性)")]
    private static string GetGuidTag(AuthorizationFilterContext filterContext)
    {
        //检测GuidTag属性
        var actionDescription = filterContext.ActionDescriptor.GetAttribute<GuidTagAttribute>();

        return actionDescription.GuidTag;
    }

    protected void CheckAccessWay(AuthorizationFilterContext filterContext)
    {
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

        var accessWay = AccessWayAssist.GetByGuidTag(guidTag);

        if (accessWay == null)
        {
            var accessWayMessage = new AccessWayMessage
            {
                GuidTag = guidTag,
                Name = name,
                RelativePath = path,
                Expand = competence,
                Channel = channel,
            };

            Monitor.GetInstance().Add(accessWayMessage);
        }
        else
        {
            if (accessWay.Name == name && accessWay.RelativePath == path && accessWay.Expand == competence &&
                accessWay.Channel == channel)
            {
                return;
            }

            var accessWayMessage = new AccessWayMessage
            {
                GuidTag = guidTag,
                Name = name,
                RelativePath = path,
                Expand = competence,
                Channel = channel,
            };

            Monitor.GetInstance().Add(accessWayMessage);
        }
    }

    public abstract void OnAuthorization(AuthorizationFilterContext context);
}