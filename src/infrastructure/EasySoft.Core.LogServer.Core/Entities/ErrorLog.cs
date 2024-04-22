using EasySoft.Core.LogServer.Core.Entities.Bases;

namespace EasySoft.Core.LogServer.Core.Entities;

/// <summary>
/// 错误日志
/// </summary>
public class ErrorLog : BaseEntity, IErrorLogStore
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
    public int Type { get; set; } = ErrorLogType.DataError.ToInt();

    /// <inheritdoc />
    public string TypeNote { get; set; } = "";

    /// <inheritdoc />
    public int Degree { get; set; } = ErrorLogDegree.Info.ToInt();

    /// <inheritdoc />
    public string DegreeNote { get; set; } = "";

    /// <inheritdoc />
    public int SendNotification { get; set; }

    /// <inheritdoc />
    public int SendResult { get; set; }

    /// <inheritdoc />
    public DateTime SendTime { get; set; }

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
    public int Resolve { get; set; }

    /// <inheritdoc />
    public string ExceptionTypeName { get; set; } = "";

    /// <inheritdoc />
    public string ExceptionTypeFullName { get; set; } = "";

    /// <inheritdoc />
    public string ResolveNote { get; set; } = "";

    /// <inheritdoc />
    public int Status { get; set; }

    /// <inheritdoc />
    public long CreateBy { get; set; }

    /// <inheritdoc />
    public DateTime CreateTime { get; set; } = DateTimeOffset.Now.DateTime;

    /// <inheritdoc />
    public long ModifyBy { get; set; }

    /// <inheritdoc />
    public DateTime ModifyTime { get; set; } = DateTimeOffset.Now.DateTime;

    /// <inheritdoc />
    public string Channel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

    /// <inheritdoc />
    public string TriggerChannel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

    /// <inheritdoc />
    public string Ip { get; set; } = "";

    #endregion

    #region Methods

    /// <inheritdoc />
    public string GetId()
    {
        return Id.ToString();
    }

    /// <inheritdoc />
    public string GetIdentificationName()
    {
        return ReflectionAssist.GetPropertyName(() => Id);
    }

    #endregion
}