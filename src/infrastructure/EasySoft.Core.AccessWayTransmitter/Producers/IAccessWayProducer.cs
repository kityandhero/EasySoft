using EasySoft.Core.AccessWayTransmitter.Interfaces;

namespace EasySoft.Core.AccessWayTransmitter.Producers;

public interface IAccessWayProducer
{
    public Task<IAccessWayExchange> SendAsync(
        string guidTag,
        string name,
        string path,
        string competence
    );
}