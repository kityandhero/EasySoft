namespace EasySoft.Core.SqlLogTransceiver.Consumers;

/// <inheritdoc />
public abstract class BaseSqlLogConsumer : ISqlLogConsumer
{
    /// <summary>
    /// 处理
    /// </summary>   
    /// <param name="transitiveMessage>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public abstract Task OnHandle(ISqlLogTransitiveMessage transitiveMessage, CancellationToken cancellationToken);
}