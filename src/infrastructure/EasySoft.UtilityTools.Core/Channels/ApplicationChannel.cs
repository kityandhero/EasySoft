namespace EasySoft.UtilityTools.Core.Channels;

/// <summary>
/// ApplicationChannel
/// </summary>
public class ApplicationChannel : IApplicationChannel
{
    /// <summary>
    /// DefaultChannel
    /// </summary>
    public const int DefaultChannel = 0;

    /// <summary>
    /// DefaultName
    /// </summary>
    public const string DefaultName = "默认应用";

    private int _channel;

    private string _name;

    /// <summary>
    /// ApplicationChannel
    /// </summary>
    public ApplicationChannel()
    {
        _name = "";
    }

    /// <summary>
    /// GetChannel
    /// </summary>
    /// <returns></returns>
    public int GetChannel()
    {
        return _channel;
    }

    /// <summary>
    /// GetName
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
        return _name;
    }

    /// <summary>
    /// SetChannel
    /// </summary>
    /// <param name="channel"></param>
    /// <returns></returns>
    public IApplicationChannel SetChannel(int channel)
    {
        _channel = channel;

        return this;
    }

    /// <summary>
    /// SetName
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IApplicationChannel SetName(string name)
    {
        _name = name;

        return this;
    }
}