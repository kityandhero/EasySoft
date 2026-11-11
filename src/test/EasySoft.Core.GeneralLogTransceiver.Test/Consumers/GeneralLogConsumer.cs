using EasySoft.Core.GeneralLogTransceiver.Consumers;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.Core.GeneralLogTransceiver.Test.Consumers;

/// <summary>
/// GeneralLogConsumer 消费器示例
/// </summary>
public class GeneralLogConsumer : BaseGeneralLogConsumer
{
    /// <inheritdoc />
    public override Task OnHandle(IGeneralLogTransitiveMessage transitiveMessage, CancellationToken cancellationToken)
    {
        Console.WriteLine($"transitiveMessage received: {transitiveMessage.Message}");

        return Task.CompletedTask;
    }
}