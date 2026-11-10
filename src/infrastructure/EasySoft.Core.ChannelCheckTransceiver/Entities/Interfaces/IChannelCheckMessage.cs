namespace EasySoft.Core.ChannelCheckTransceiver.Entities.Interfaces;

/// <summary>
/// channel check exchange
/// </summary>
public interface IChannelCheckMessage
{
    /// <summary>
    /// channel
    /// </summary>
    string Channel { get; set; }
}