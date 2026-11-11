namespace EasySoft.Core.GeneralLogTransceiver.Consumers;

/// <summary>
/// 一般日志消费器
/// </summary>
public interface IGeneralLogConsumer : IConsumer<IGeneralLogTransitiveMessage>
{
}