using EasySoft.Core.ChannelCheckTransceiver.Entities.Interfaces;

namespace EasySoft.Core.ChannelCheckTransceiver.Producers;

/// <summary>
/// ChannelCheck 发布器
/// </summary>
public interface IChannelCheckProducer
{
    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>发送结果</returns>
    public Task SendAsync(
        CancellationToken cancellationToken = default
    );
}