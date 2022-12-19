using EasySoft.Core.LogServer.Core.Entities.Bases;

namespace EasySoft.Core.LogServer.Core.Entities;

/// <summary>
/// 一般日志持久化
/// </summary>
public class GeneralLog : BaseEntity, IGeneralLog, IChannel, IIp, IStatus, IOperate
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
    public int Channel { get; set; }

    /// <inheritdoc />
    public string Ip { get; set; } = "";

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
}