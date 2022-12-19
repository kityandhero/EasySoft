namespace EasySoft.Core.LogServer.Core.DataTransferObjects;

/// <summary>
/// SqlExecutionRecordDto
/// </summary>
public class SqlExecutionRecordDto
{
    /// <summary>
    /// SqlExecutionRecordId
    /// </summary>
    public long SqlExecutionRecordId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Title { get; set; } = "";
}