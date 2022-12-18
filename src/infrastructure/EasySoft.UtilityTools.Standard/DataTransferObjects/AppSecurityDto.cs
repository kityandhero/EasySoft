using EasySoft.UtilityTools.Standard.Entities.Interfaces;

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

    /// <summary>
    /// Channel
    /// </summary>
    public int Channel { get; set; }

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