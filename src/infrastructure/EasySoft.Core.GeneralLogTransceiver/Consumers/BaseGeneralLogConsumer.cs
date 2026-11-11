namespace EasySoft.Core.GeneralLogTransceiver.Consumers;

/// <inheritdoc />
public abstract class BaseGeneralLogConsumer : IGeneralLogConsumer
{
    /// <summary>
    /// 处理
    /// </summary>
    /// <param name="transitiveMessage>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public abstract Task OnHandle(IGeneralLogTransitiveMessage transitiveMessage, CancellationToken cancellationToken);
}