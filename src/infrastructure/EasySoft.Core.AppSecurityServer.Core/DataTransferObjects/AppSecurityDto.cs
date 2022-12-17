namespace EasySoft.Core.AppSecurityServer.Core.DataTransferObjects;

/// <summary>
/// AppSecurityDto
/// </summary>
public class AppSecurityDto
{
    /// <summary>
    /// ErrorLogId
    /// </summary>
    public long AppSecurityId { get; set; }

    /// <summary>
    /// ChannelId
    /// </summary>
    public string AppId { get; set; } = "";

    /// <summary>
    /// AppSecret
    /// </summary>
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
}