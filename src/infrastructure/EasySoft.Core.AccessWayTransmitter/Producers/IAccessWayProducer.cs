using EasySoft.Core.AccessWayTransmitter.Interfaces;

namespace EasySoft.Core.AccessWayTransmitter.Producers;

public interface IAccessWayProducer
{
    public IAccessWayExchange Send(
        string guidTag,
        string name,
        string path,
        string competence
    );
}