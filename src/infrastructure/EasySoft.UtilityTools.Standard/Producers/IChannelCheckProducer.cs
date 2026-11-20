using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Standard.Producers;

/// <summary>
/// ChannelCheck 发布器
/// </summary>
public interface IChannelCheckProducer
{
    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="channelCheckTransitiveMessage">消息体</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>发送结果</returns>
    public Task SendAsync(
        IChannelCheckTransitiveMessage channelCheckTransitiveMessage,
        CancellationToken cancellationToken = default
    );
}