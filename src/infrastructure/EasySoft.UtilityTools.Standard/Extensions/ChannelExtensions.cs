using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// ChannelExtensions
/// </summary>
public static class ChannelExtensions
{
    /// <summary>
    /// ToInt
    /// </summary>
    /// <param name="channel"></param>
    /// <returns></returns>
    public static string ToValue(this IChannel channel)
    {
        return channel.Value;
    }

    /// <summary>
    /// ToFlagItem
    /// </summary>
    /// <param name="channel"></param>
    /// <returns></returns>
    public static IFlagItem ToFlagItem(this IChannel channel)
    {
        return new FlagItem
        {
            Key = channel.Name.ToLowerFirst(),
            Flag = channel.Value,
            Name = channel.Description,
            Availability = Whether.Yes.ToInt()
        };
    }

    /// <summary>
    /// ToFlagItemCollection
    /// </summary>
    /// <param name="channels"></param>
    /// <returns></returns>
    public static IEnumerable<IFlagItem> ToFlagItemCollection(this IEnumerable<IChannel> channels)
    {
        return channels.Select(
            o => new FlagItem
            {
                Key = o.Name.ToLowerFirst(),
                Flag = o.Value.ToString(),
                Name = o.Description,
                Availability = Whether.Yes.ToInt()
            }
        );
    }

    /// <summary>
    /// ToFlagItemObjectCollection
    /// </summary>
    /// <param name="channels"></param>
    /// <returns></returns>
    public static IEnumerable<object> ToFlagItemObjectCollection(this IEnumerable<IChannel> channels)
    {
        return channels.ToFlagItemCollection()
            .Select(
                o => new
                {
                    key = o.Name,
                    flag = o.Flag,
                    name = o.Name,
                    availability = o.Availability
                }
            );
    }
}