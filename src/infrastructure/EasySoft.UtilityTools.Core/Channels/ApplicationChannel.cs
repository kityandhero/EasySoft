using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.UtilityTools.Core.Channels;

/// <summary>
/// ApplicationChannel
/// </summary>
public class ApplicationChannel : IApplicationChannel
{
    /// <summary>
    /// DefaultChannel
    /// </summary>
    public static readonly IChannel DefaultChannel = Channel.Unknown;

    private IChannel _channel = Channel.Unknown;

    /// <summary>
    /// ApplicationChannel
    /// </summary>
    public ApplicationChannel()
    {
    }

    /// <summary>
    /// ApplicationChannel
    /// </summary>
    public ApplicationChannel(IChannel channel)
    {
        _channel = channel;

        ChannelAssist.SetCurrentChannel(channel);
    }

    /// <summary>
    /// GetChannel
    /// </summary>
    /// <returns></returns>
    public IChannel GetChannel()
    {
        return _channel;
    }

    /// <summary>
    /// GetName
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
        return _channel.Name;
    }

    /// <summary>
    /// SetChannel
    /// </summary>
    /// <param name="channel"></param>
    /// <returns></returns>
    public IApplicationChannel SetChannel(IChannel channel)
    {
        _channel = channel;

        return this;
    }
}