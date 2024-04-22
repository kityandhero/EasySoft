using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Core.Channels;

/// <summary>
/// IApplicationChannel
/// </summary>
public interface IApplicationChannel
{
    /// <summary>
    /// GetChannel
    /// </summary>
    /// <returns></returns>
    public IChannel GetChannel();

    /// <summary>
    /// GetName
    /// </summary>
    /// <returns></returns>
    public string GetName();

    /// <summary>
    /// SetChannel
    /// </summary>
    /// <param name="channel"></param>
    /// <returns></returns>
    public IApplicationChannel SetChannel(IChannel channel);
}