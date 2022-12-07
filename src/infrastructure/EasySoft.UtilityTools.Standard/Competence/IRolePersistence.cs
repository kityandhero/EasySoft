namespace EasySoft.UtilityTools.Standard.Competence;

/// <summary>
/// IRolePersistence
/// </summary>
public interface IRolePersistence
{
    /// <summary>
    /// 权限集合
    /// </summary>
    string Competence { get; set; }

    /// <summary>
    /// 超级管理
    /// </summary>
    int WhetherSuper { get; set; }
}