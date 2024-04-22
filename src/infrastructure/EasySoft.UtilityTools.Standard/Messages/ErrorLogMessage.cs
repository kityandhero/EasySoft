using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Standard.Messages;

/// <summary>
/// 异常日志传输消息
/// </summary>
public class ErrorLogMessage : IErrorLogMessage
{
    #region Properties

    /// <inheritdoc />
    public long OperatorId { get; set; }

    /// <inheritdoc />
    public string Url { get; set; } = "";

    /// <inheritdoc />
    public string Message { get; set; } = "";

    /// <inheritdoc />
    public string Description { get; set; } = "";

    /// <inheritdoc />
    public string AncillaryInformation { get; set; } = "";

    /// <inheritdoc />
    public string StackTrace { get; set; } = "";

    /// <inheritdoc />
    public string Source { get; set; } = "";

    /// <inheritdoc />
    public string Scene { get; set; } = "";

    /// <inheritdoc />
    public int Type { get; set; } = (int)ErrorLogType.DataError;

    /// <inheritdoc />
    public string TypeNote { get; set; } = ErrorLogType.DataError
        .GetCustomAttribute<DescriptionAttribute>()
        ?.Description ?? "";

    /// <inheritdoc />
    public int Degree { get; set; }

    /// <inheritdoc />
    public string DegreeNote { get; set; } = ErrorLogDegree.Info
        .GetCustomAttribute<DescriptionAttribute>()
        ?.Description ?? "";

    /// <inheritdoc />
    public int SendNotification { get; set; }

    /// <inheritdoc />
    public int SendResult { get; set; }

    /// <inheritdoc />
    public DateTime SendTime { get; set; } = ConstCollection.DbDefaultDateTime;

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
    public int CustomDataType { get; set; } = (int)CustomValueType.PlainValue;

    /// <inheritdoc />
    public int Resolve { get; set; } = (int)ErrorLogResolve.Unresolved;

    /// <inheritdoc />
    public string ExceptionTypeName { get; set; } = "";

    /// <inheritdoc />
    public string ExceptionTypeFullName { get; set; } = "";

    /// <inheritdoc />
    public string ResolveNote { get; set; } = ErrorLogResolve.Unresolved
        .GetCustomAttribute<DescriptionAttribute>()
        ?.Description ?? "";

    /// <inheritdoc />
    public string TriggerChannel { get; set; } = Models.Channel.Unknown.ToValue();

    /// <inheritdoc />
    public string Ip { get; set; } = "";

    /// <inheritdoc />
    public int Ignore { get; set; } = Whether.No.ToInt();

    #endregion
}