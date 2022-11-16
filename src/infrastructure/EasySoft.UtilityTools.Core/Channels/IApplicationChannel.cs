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
    public int GetChannel();

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
    public IApplicationChannel SetChannel(int channel);

    /// <summary>
    /// SetName
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IApplicationChannel SetName(string name);
}