using EasySoft.Core.AccessWayTransmitter.Interfaces;
using EasySoft.UtilityTools.Standard.Entities.Interfaces;

namespace EasySoft.Core.AccessWayTransmitter.Producers;

public interface IAccessWayProducer
{
    public Task<IAccessWayExchange> SendAsync(
        IAccessWay accessWay
    );
}