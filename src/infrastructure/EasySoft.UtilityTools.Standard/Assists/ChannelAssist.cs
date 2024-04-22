using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// 通道辅助
/// </summary>
public static class ChannelAssist
{
    private static IChannel _channel = Channel.Unknown;

    private static ICollection<IChannel> _channelCollection = new List<IChannel>();

    private static ICollection<IChannel> _channelWithRoleCollection = new List<IChannel>();

    /// <summary>
    /// 设置当前通道
    /// </summary>
    /// <param name="channel">通道</param>
    /// <param name="successCallback"></param>
    public static void SetCurrentChannel(IChannel channel, Action<IChannel>? successCallback = null)
    {
        _channel = channel;

        successCallback?.Invoke(_channel);
    }

    /// <summary>
    /// 获取当前通道（需要先行使用 SetCurrentChannel 设置，否则将发生异常）
    /// </summary>
    /// <returns></returns>
    public static IChannel GetCurrentChannel()
    {
        if (_channel.Value == Channel.Unknown.Value)
        {
            throw new UnhandledException("channel need set");
        }

        return _channel;
    }

    /// <summary>
    /// 设置所有通道集合
    /// </summary>
    /// <param name="channels"></param>
    /// <exception cref="UnhandledException"></exception>
    public static void SetChannelCollection(ICollection<IChannel> channels)
    {
        var values = channels.Select(o => o.Value).ToList();

        if (values.Count() != values.Distinct().Count())
        {
            throw new UnhandledException(
                "channel values exist Duplicate value, please make sure the channel value is unique"
            );
        }

        _channelCollection = channels;
    }

    /// <summary>
    /// 获取所有通道集合
    /// </summary>
    /// <returns></returns>
    public static ICollection<IChannel> GetChannelCollection()
    {
        return _channelCollection;
    }

    /// <summary>
    /// 设置所有具有角色的通道集合
    /// </summary>
    /// <param name="channels">通告集合</param>
    public static void SetChannelWithRoleCollection(ICollection<IChannel> channels)
    {
        var values = channels.Select(o => o.Value).ToList();

        if (values.Count() != values.Distinct().Count())
        {
            throw new UnhandledException(
                "channel values exist Duplicate value, please make sure the channel value is unique"
            );
        }

        _channelWithRoleCollection = channels;
    }

    /// <summary>
    /// 获取所有具有角色的通道集合
    /// </summary>
    /// <returns></returns>
    public static ICollection<IChannel> GetChannelWithRoleCollection()
    {
        return _channelWithRoleCollection;
    }
}