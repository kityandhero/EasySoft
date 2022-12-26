using EasySoft.Core.ChannelCheckTransmitter.Entities.Interfaces;

namespace EasySoft.Core.ChannelCheckTransmitter.Entities.implements;

/// <inheritdoc />
public class ChannelCheckExchange : IChannelCheckExchange
{
    /// <inheritdoc />
    public int Channel { get; set; }
}