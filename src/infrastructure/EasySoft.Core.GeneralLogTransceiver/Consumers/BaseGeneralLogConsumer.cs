namespace EasySoft.Core.GeneralLogTransceiver.Consumers;

/// <inheritdoc />
public abstract class BaseGeneralLogConsumer : IGeneralLogConsumer
{
    /// <summary>
    /// 处理
    /// </summary>
    /// <param name="message">消息体</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task OnHandle(IGeneralLogMessage message, CancellationToken cancellationToken);
}