using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.UtilityTools.Standard.Messages;

/// <summary>
/// ExecuteResultUpdateLogTransitiveMessage
/// </summary>
public class ExecuteResultUpdateLogTransitiveMessage : IExecuteResultUpdateLogTransitiveMessage
{
    /// <inheritdoc />
    public string Tag { get; set; } = "";

    /// <inheritdoc />
    public string ResultType { get; set; } = "";

    /// <inheritdoc />
    public string Result { get; set; } = "";

    /// <inheritdoc />
    public DateTime ResultTime { get; set; } = DateTime.Now;

    /// <inheritdoc />  
    public string TriggerChannel { get; set; } = Channel.Unknown.ToValue();

    /// <inheritdoc />
    public int Ignore { get; set; } = Whether.No.ToInt();
}