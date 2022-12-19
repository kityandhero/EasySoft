using EasySoft.Core.LogServer.Core.Entities.Bases;

namespace EasySoft.Core.LogServer.Core.Entities;

/// <summary>
/// 错误日志
/// </summary>
public class ErrorLog : BaseEntity, IErrorLog, IChannel, IIp, IStatus, IOperate
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
    public int Type { get; set; } = ErrorLogExchangeType.DataError.ToInt();

    /// <inheritdoc />
    public int Degree { get; set; } = ErrorLogExchangeDegree.Info.ToInt();

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
    public int Channel { get; set; }

    /// <inheritdoc />
    public int Status { get; set; }

    /// <inheritdoc />
    public string Ip { get; set; } = "";

    /// <inheritdoc />
    public long CreateBy { get; set; }

    /// <inheritdoc />
    public DateTime CreateTime { get; set; } = DateTimeOffset.Now.DateTime;

    /// <inheritdoc />
    public long ModifyBy { get; set; }

    /// <inheritdoc />
    public DateTime ModifyTime { get; set; } = DateTimeOffset.Now.DateTime;
}