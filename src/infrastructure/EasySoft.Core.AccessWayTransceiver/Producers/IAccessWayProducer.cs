namespace EasySoft.Core.AccessWayTransceiver.Producers;

/// <summary>
/// AccessWay 发布器
/// </summary>
public interface IAccessWayProducer
{
    /// <summary>
    /// send
    /// </summary>
    /// <param name="accessWay">消息体</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>发送结果</returns>
    public Task SendAsync(
        IAccessWay accessWay,
        CancellationToken cancellationToken = default
    );
}