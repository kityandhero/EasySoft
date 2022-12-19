namespace EasySoft.Core.LogServer.Core.DataTransferObjects;

/// <summary>
/// GeneralLogDto
/// </summary>
public class GeneralLogDto
{
    /// <summary>
    /// GeneralLogId
    /// </summary>
    public long GeneralLogId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Title { get; set; } = "";
}