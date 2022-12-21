using EasySoft.Core.ErrorLogTransmitter.Extensions;
using EasySoft.Core.ErrorLogTransmitter.Interfaces;
using EasySoft.UtilityTools.Standard.Entities.Implements;

namespace EasySoft.Core.ErrorLogTransmitter.Entities;

/// <summary>
/// 错误日志传输信息
/// </summary>
public class ErrorLogExchange : BaseExchange, IErrorLogExchange, IIgnore
{
    /// <inheritdoc />
    public long UserId { get; set; }

    /// <inheritdoc />
    public string Url { get; set; } = "";

    /// <inheritdoc />
    public string Message { get; set; } = "";

    /// <inheritdoc />
    public string StackTrace { get; set; } = "";

    /// <inheritdoc />
    public string Source { get; set; } = "";

    /// <inheritdoc />
    public string Scene { get; set; } = "";

    /// <inheritdoc />
    public int Type { get; set; } = ErrorLogType.DataError.ToInt();

    /// <inheritdoc />
    public int Degree { get; set; } = ErrorLogDegree.Info.ToInt();

    /// <inheritdoc />
    public string Header { get; set; } = "";

    /// <inheritdoc />
    public string UrlParams { get; set; } = "";

    /// <inheritdoc />
    public string PayloadParams { get; set; } = "";

    /// <inheritdoc />
    public string FormParams { get; set; } = "";

    /// <inheritdoc />
    public string Host { get; set; } = "";

    /// <inheritdoc />
    public int Port { get; set; }

    /// <inheritdoc />
    public string CustomLog { get; set; } = "";

    /// <inheritdoc />
    public string CustomData { get; set; } = "";

    /// <inheritdoc />
    public int CustomDataType { get; set; } = CustomValueType.PlainValue.ToInt();

    /// <inheritdoc />
    public string ExceptionTypeName { get; set; } = "";

    /// <inheritdoc />
    public string ExceptionTypeFullName { get; set; } = "";

    /// <inheritdoc />
    public int Ignore { get; set; }
}