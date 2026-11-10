using EasySoft.Core.ChannelCheckTransceiver.Entities.Interfaces;

namespace EasySoft.Core.ChannelCheckTransceiver.Consumers;

/// <inheritdoc />
public abstract class BaseChannelCheckConsumer : IChannelCheckConsumer
{
    /// <summary>
    /// 处理
    /// </summary>
    /// <param name="message">消息体</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public abstract Task OnHandle(IChannelCheckMessage message, CancellationToken cancellationToken);
}