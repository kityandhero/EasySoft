namespace EasySoft.UtilityTools.Standard.Entities.Interfaces;

/// <summary>
/// IRolePersistence
/// </summary>
public interface IRole
{
    /// <summary>
    /// 权限集合
    /// </summary>
    [Description("权限集合")]
    string Competence { get; set; }

    /// <summary>
    /// 超级管理
    /// </summary>
    [Description("超级管理")]
    int WhetherSuper { get; set; }
}