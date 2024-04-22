using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Standard.Messages;

/// <summary>
/// 微信传输消息
/// </summary>
public class WeChatMessageTransfer : IWeChatMessageTransfer, IQueueMessage, IIgnore
{
    #region Properties

    /// <inheritdoc />
    public long ApplicationId { get; set; }

    /// <inheritdoc />
    public long UserId { get; set; }

    /// <inheritdoc />
    public int ClientType { get; set; }

    /// <inheritdoc />
    public string OpenId { get; set; } = "";

    /// <inheritdoc />
    public string TemplateId { get; set; } = "";

    /// <inheritdoc />
    public string TemplateKey { get; set; } = "";

    /// <inheritdoc />
    public string Url { get; set; } = "";

    /// <inheritdoc />
    public string JsonData { get; set; } = "";

    /// <inheritdoc />
    public string EmphasisKeyword { get; set; } = "";

    /// <inheritdoc />
    public string Color { get; set; } = "";

    /// <inheritdoc />
    public string FormId { get; set; } = "";

    /// <inheritdoc />
    public string AppId { get; set; } = "";

    /// <inheritdoc />
    public string Page { get; set; } = "";

    /// <inheritdoc />
    public string MiniProgramAppId { get; set; } = "";

    /// <inheritdoc />
    public string MiniProgramPagePath { get; set; } = "";

    /// <inheritdoc />
    public int Mode { get; set; }

    /// <inheritdoc />
    public DateTime SendTime { get; set; }

    /// <inheritdoc />
    public long SendUnixTime { get; set; }

    /// <inheritdoc />
    public string TriggerChannel { get; set; } = Models.Channel.Unknown.ToValue();

    /// <inheritdoc />
    public string Ip { get; set; } = "";

    /// <inheritdoc />
    public int Ignore { get; set; } = Whether.No.ToInt();

    #endregion
}