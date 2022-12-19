using EasySoft.Core.GeneralLogTransmitter.Interfaces;

namespace EasySoft.Core.GeneralLogTransmitter.Producers;

/// <summary>
/// 一般日志发布者
/// </summary>
public interface IGeneralLogProducer
{
    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task<IGeneralLogExchange> SendAsync(string message);

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="generalLogExchange"></param>
    /// <returns></returns>
    public Task<IGeneralLogExchange> SendAsync(IGeneralLogExchange generalLogExchange);

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="message"></param>
    /// <param name="messageValueType"></param>
    /// <returns></returns>
    public Task<IGeneralLogExchange> SendAsync(object message, CustomValueType messageValueType);

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="message"></param>
    /// <param name="messageValueType"></param>
    /// <param name="content"></param>
    /// <param name="contentValueType"></param>
    /// <returns></returns>
    public Task<IGeneralLogExchange> SendAsync(
        object message,
        CustomValueType messageValueType,
        object content,
        CustomValueType contentValueType
    );
}