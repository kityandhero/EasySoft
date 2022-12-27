namespace EasySoft.UtilityTools.Standard.Entities.Interfaces;

/// <summary>
/// IRolePersistence
/// </summary>
public interface IRole : IWhetherSuper
{
    /// <summary>
    /// 权限集合
    /// </summary>
    [Description("权限集合")]
    string Competence { get; set; }
}