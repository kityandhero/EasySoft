using EasySoft.Core.GeneralLogTransmitter.Interfaces;

namespace EasySoft.Core.GeneralLogTransmitter.Entities;

/// <summary>
/// 一般日志传输信息
/// </summary>
public class GeneralLogExchange : BaseExchange, IGeneralLogExchange
{
    /// <inheritdoc />
    public string Message { get; set; } = "";

    /// <inheritdoc />
    public int MessageType { get; set; } = CustomValueType.PlainValue.ToInt();

    /// <inheritdoc />
    public string Content { get; set; } = "";

    /// <inheritdoc />
    public int ContentType { get; set; } = CustomValueType.PlainValue.ToInt();

    /// <inheritdoc />
    public int Type { get; set; } = GeneralLogType.Common.ToInt();

    /// <inheritdoc />
    public int Ignore { get; set; }
}