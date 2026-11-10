namespace EasySoft.Core.ErrorLogTransceiver.Consumers;

/// <summary>
/// 错误日志消费器
/// </summary>
public interface IErrorLogConsumer : IConsumer<IErrorLogMessage>
{
}