using EasySoft.Core.AccessWayTransceiver.Consumers;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.Core.GeneralLogTransceiver.Test;

public class CustomAccessWayConsumer : BaseAccessWayConsumer
{
    public override Task OnHandle(IAccessWayTransitiveMessage transitiveMessage, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}