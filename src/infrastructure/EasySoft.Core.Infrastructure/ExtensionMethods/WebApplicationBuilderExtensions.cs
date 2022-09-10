using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Core.Channels;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Infrastructure.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 标记当前应用通道值, 用于远程日志等数据中, 便于数据辨认, 不使用此方法标记, 框架将采用内置值 0 代替
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="channel"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static WebApplicationBuilder AddAdvanceApplicationChannel(
        this WebApplicationBuilder builder,
        int channel,
        string name
    )
    {
        if (FlagAssist.ApplicationChannelInjectionComplete)
        {
            throw new Exception("UseAdvanceApplicationChannel disallow inject more than once");
        }

        builder.Host.AddAdvanceApplicationChannel(channel, name);

        FlagAssist.ApplicationChannelInjectionComplete = true;
        FlagAssist.ApplicationChannelIsDefault = false;

        return builder;
    }

    public static WebApplicationBuilder AddAdvanceApplicationChannel<T>(
        this WebApplicationBuilder builder,
        T applicationChannel
    ) where T : IApplicationChannel
    {
        if (FlagAssist.ApplicationChannelInjectionComplete)
        {
            throw new Exception("UseAdvanceApplicationChannel disallow inject more than once");
        }

        builder.Host.AddAdvanceApplicationChannel(applicationChannel.GetChannel(), applicationChannel.GetName());

        FlagAssist.ApplicationChannelInjectionComplete = true;
        FlagAssist.ApplicationChannelIsDefault = false;

        return builder;
    }

    public static WebApplicationBuilder AddAdvanceDefaultApplicationChannel(
        this WebApplicationBuilder builder
    )
    {
        if (FlagAssist.ApplicationChannelInjectionComplete)
        {
            throw new Exception("UseAdvanceApplicationChannel disallow inject more than once");
        }

        builder.Host.AddAdvanceDefaultApplicationChannel();

        FlagAssist.ApplicationChannelInjectionComplete = true;
        FlagAssist.ApplicationChannelIsDefault = true;

        return builder;
    }
}