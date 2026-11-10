using EasySoft.Core.ChannelCheckTransceiver.Entities.Interfaces;

namespace EasySoft.Core.ChannelCheckTransceiver.Consumers;

/// <summary>
/// 一般日志消费器
/// </summary>
public interface IChannelCheckConsumer : IConsumer<IChannelCheckMessage>
{
}