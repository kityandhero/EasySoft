using EasySoft.Core.ChannelCheckTransmitter.Entities.Interfaces;

namespace EasySoft.Core.ChannelCheckTransmitter.Producers;

/// <summary>
/// Access Way Producer
/// </summary>
public interface IChannelCheckProducer
{
    /// <summary>
    /// send
    /// </summary>
    /// <returns></returns>
    public Task<IChannelCheckExchange> SendAsync();
}