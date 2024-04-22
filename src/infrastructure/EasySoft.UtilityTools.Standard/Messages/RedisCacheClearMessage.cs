using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Standard.Messages;

/// <summary>
/// 缓存清理传输消息
/// </summary>
public class RedisCacheClearMessage : IRedisCacheClear, IQueueMessageTransfer
{
    /// <inheritdoc />
    public string CacheKey { get; set; } = "";

    /// <inheritdoc />
    public bool MatchMode { get; set; } = false;

    /// <inheritdoc />
    public string TriggerChannel { get; set; } = Models.Channel.Unknown.ToValue();

    /// <inheritdoc />
    public int Ignore { get; set; }

    /// <inheritdoc />
    public string Ip { get; set; } = "";
}