namespace EasySoft.Core.PermissionServer.Core.DataTransferObjects;

/// <summary>
/// AccessWaySearchDto
/// </summary>
public class AccessWaySearchDto : PageSearchParams
{
    /// <summary>
    /// AccessWayCId
    /// </summary>
    public long AccessWayId { get; set; }

    /// <summary>
    /// 模块组名
    /// </summary>
    [Description("模块组名")]
    public string Name { get; set; } = "";
}