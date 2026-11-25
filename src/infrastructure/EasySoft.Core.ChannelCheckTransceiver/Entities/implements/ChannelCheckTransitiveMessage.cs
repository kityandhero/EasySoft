namespace EasySoft.Core.ChannelCheckTransceiver.Entities.implements;

/// <inheritdoc />
public class ChannelCheckTransitiveMessage : IChannelCheckTransitiveMessage
{
    /// <inheritdoc />
    public string Channel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();
}