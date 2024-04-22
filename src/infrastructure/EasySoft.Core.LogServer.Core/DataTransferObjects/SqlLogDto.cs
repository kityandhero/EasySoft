namespace EasySoft.Core.LogServer.Core.DataTransferObjects;

/// <summary>
/// SqlLogDto
/// </summary>
public class SqlLogDto
{
    /// <summary>
    /// SqlLogId
    /// </summary>
    public long SqlLogId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Title { get; set; } = "";
}