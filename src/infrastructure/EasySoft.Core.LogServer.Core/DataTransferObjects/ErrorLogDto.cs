namespace EasySoft.Core.LogServer.Core.DataTransferObjects;

/// <summary>
/// ErrorLogDto
/// </summary>
public class ErrorLogDto
{
    /// <summary>
    /// ErrorLogId
    /// </summary>
    public long ErrorLogId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Title { get; set; } = "";
}