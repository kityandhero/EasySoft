using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.UtilityTools.Standard.Messages;

/// <summary>
/// 执行日志传输消息
/// </summary>
public class ExecuteLogMessage : IExecuteLog, IQueueMessageTransfer
{
    #region Properties

    /// <inheritdoc />
    public string Tag { get; set; } = UniqueIdAssist.CreateUUID();

    /// <inheritdoc />
    public string DeclaringTypeNamespace { get; set; } = "";

    /// <inheritdoc />
    public string DeclaringTypeName { get; set; } = "";

    /// <inheritdoc />
    public string Name { get; set; } = "";

    /// <inheritdoc />
    public string Parameter { get; set; } = "";

    /// <inheritdoc />
    public string ResultType { get; set; } = "";

    /// <inheritdoc />
    public string Result { get; set; } = "";

    /// <inheritdoc />
    public DateTime ExecuteTime { get; set; } = DateTime.Now;

    /// <inheritdoc />
    public DateTime ResultTime { get; set; } = DateTime.Now;

    /// <inheritdoc />
    public string TriggerChannel { get; set; } = Channel.Unknown.ToValue();

    /// <inheritdoc />
    public string Ip { get; set; } = "";

    /// <inheritdoc />
    public int Ignore { get; set; } = Whether.No.ToInt();

    #endregion
}