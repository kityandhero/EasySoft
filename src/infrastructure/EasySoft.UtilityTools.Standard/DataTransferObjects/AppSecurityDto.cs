using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Standard.DataTransferObjects;

/// <summary>
/// AppSecurityDto
/// </summary>
public class AppSecurityDto : IAppSecurity
{
    /// <summary>
    /// ErrorLogId
    /// </summary>
    public long AppSecurityId { get; set; }

    /// <inheritdoc />
    public string AppId { get; set; } = "";

    /// <inheritdoc />
    public string AppSecret { get; set; } = "";

    /// <inheritdoc />
    public int MasterControl { get; set; }

    /// <summary>
    /// Channel
    /// </summary>
    public string Channel { get; set; } = Models.Channel.Unknown.ToValue();

    /// <summary>
    /// UnixTime
    /// </summary>
    public long UnixTime { get; set; } = 0;

    /// <summary>
    /// Salt
    /// </summary>
    public string Salt { get; set; } = "";

    /// <summary>
    /// Sign
    /// </summary>
    public string Sign { get; set; } = "";

    /// <summary>
    /// PublicKey
    /// </summary>
    public string PublicKey { get; set; } = "";
}