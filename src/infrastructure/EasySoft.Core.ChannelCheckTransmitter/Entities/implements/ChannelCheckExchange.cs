using EasySoft.Core.ChannelCheckTransmitter.Entities.Interfaces;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.ChannelCheckTransmitter.Entities.implements;

/// <inheritdoc />
public class ChannelCheckExchange : IChannelCheckExchange
{
    /// <inheritdoc />
    public string Channel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();
}