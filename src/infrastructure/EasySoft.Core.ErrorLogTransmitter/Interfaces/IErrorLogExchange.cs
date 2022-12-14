namespace EasySoft.Core.ErrorLogTransmitter.Interfaces;

/// <summary>
/// 错误日志传输信息
/// </summary>
public interface IErrorLogExchange : IExchangeEntity, IErrorLog, IIp, ICreate
{
}