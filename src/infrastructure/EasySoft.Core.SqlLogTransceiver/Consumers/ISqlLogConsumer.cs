using SlimMessageBus;

namespace EasySoft.Core.SqlLogTransceiver.Consumers;

/// <summary>
/// SQL日志消费器
/// </summary>
public interface ISqlLogConsumer : IConsumer<ISqlLogMessage>
{
}