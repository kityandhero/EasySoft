namespace EasySoft.Core.ErrorLogTransceiver.Consumers;

/// <inheritdoc />
public abstract class BaseErrorLogConsumer : IErrorLogConsumer
{
    /// <summary>
    /// 处理
    /// </summary>
    /// <param name="transitiveMessage">消息体</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public abstract Task OnHandle(
        IErrorLogTransitiveMessage transitiveMessage,
        CancellationToken cancellationToken
    );
}