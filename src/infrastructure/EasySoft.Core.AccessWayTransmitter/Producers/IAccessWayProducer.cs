using EasySoft.Core.AccessWayTransmitter.Interfaces;

namespace EasySoft.Core.AccessWayTransmitter.Producers;

/// <summary>
/// Access Way Producer
/// </summary>
public interface IAccessWayProducer
{
    /// <summary>
    /// send
    /// </summary>
    /// <param name="accessWay"></param>
    /// <returns></returns>
    public Task<IAccessWayExchange> SendAsync(
        IAccessWay accessWay
    );
}