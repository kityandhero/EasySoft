namespace EasySoft.Core.PermissionServer.Core.DataTransferObjects;

/// <summary>
/// CustomRoleDto
/// </summary>
public class CustomRoleDto
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