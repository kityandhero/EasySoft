namespace EasySoft.UtilityTools.Standard.Competence;

/// <summary>
/// 扩展权限
/// </summary>
public class PermissionExtra
{
    /// <summary>
    /// 扩展权限名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 是否拥有权限
    /// </summary>
    public bool Authority { get; set; }

    /// <summary>
    /// PermissionExtra
    /// </summary>
    public PermissionExtra()
    {
        Name = "";
    }
}