using EasySoft.Core.ChannelCheckTransceiver.Entities.Interfaces;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.ChannelCheckTransceiver.Entities.implements;

/// <inheritdoc />
public class ChannelCheckMessage : IChannelCheckMessage
{
    /// <inheritdoc />
    public string Channel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();
}