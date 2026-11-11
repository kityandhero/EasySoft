using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Standard.Producers;

/// <summary>
/// AccessWay 发布器
/// </summary>
public interface IAccessWayProducer
{
    /// <summary>
    /// send
    /// </summary>
    /// <param name="accessWayTransitiveMessage">消息体</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>发送结果</returns>
    public Task SendAsync(
        IAccessWayTransitiveMessage accessWayTransitiveMessage,
        CancellationToken cancellationToken = default
    );
}