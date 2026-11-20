using EasySoft.Core.SqlLogTransceiver.Consumers;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.Core.GeneralLogTransceiver.Test;

public class CustomSqlLogConsumer : BaseSqlLogConsumer
{
    public override Task OnHandle(ISqlLogTransitiveMessage transitiveMessage, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}