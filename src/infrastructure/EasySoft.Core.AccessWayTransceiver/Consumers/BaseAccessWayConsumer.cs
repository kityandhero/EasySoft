using EasySoft.Core.AccessWayTransceiver.Consumers;

namespace EasySoft.Core.AccessWayTransceiver.Consumers;

/// <inheritdoc />
public abstract class BaseAccessWayConsumer : IAccessWayConsumer
{
    /// <summary>
    /// 处理
    /// </summary>
    /// <param name="TransitiveMessage>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public abstract Task OnHandle(
        IAccessWayTransitiveMessage transitiveMessage,
        CancellationToken cancellationToken
    );
}