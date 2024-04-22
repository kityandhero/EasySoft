namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// IChannelStore
/// </summary>
public interface IChannel
{
    /// <summary>
    /// Value
    /// </summary>
    string Value { get; }

    /// <summary>
    /// Name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Description
    /// </summary>
    string Description { get; }
}