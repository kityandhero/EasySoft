namespace EasySoft.UtilityTools.Standard.Competence;

/// <summary>
/// IRolePersistence
/// </summary>
public interface IRolePersistence
{
    /// <summary>
    /// 标识  
    /// </summary>
    long Id { get; set; }

    /// <summary>
    /// 名称  
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    string Description { get; set; }

    /// <summary>
    /// 内容详情
    /// </summary>
    string Content { get; set; }

    /// <summary>
    /// 模块数量
    /// </summary>
    int ModuleCount { get; set; }

    /// <summary>
    /// 权限集合
    /// </summary>
    string Competence { get; set; }

    /// <summary>
    /// 超级管理
    /// </summary>
    int WhetherSuper { get; set; }

    /// <summary>
    /// 渠道码
    /// </summary>
    int Channel { get; set; }

    /// <summary>
    /// 状态码
    /// </summary>
    int Status { get; set; }

    /// <summary>
    /// Ip
    /// </summary>
    string Ip { get; set; }

    /// <summary>
    /// 创建人标识
    /// </summary>
    long CreateUserId { get; set; }

    /// <summary>
    /// 创建时间"
    /// </summary>
    DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新人标识  
    /// </summary>
    long UpdateUserId { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    DateTime UpdateTime { get; set; }
}