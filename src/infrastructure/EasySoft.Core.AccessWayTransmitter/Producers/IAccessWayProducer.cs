using EasySoft.Core.AccessWayTransmitter.Interfaces;

namespace EasySoft.Core.AccessWayTransmitter.Producers;

public interface IAccessWayProducer
{
    public Task<IAccessWayExchange> SendAsync(
        IAccessWayPersistence accessWayPersistence
    );
}