namespace EasySoft.Core.PermissionServer.Entities.Bases;

public abstract class BaseRoleEntity : BaseEntity, IRolePersistence
{
    /// <summary>
    /// 名称  
    /// </summary>
    [Description("名称")]
    public string Name { get; set; } = "";

    /// <summary>
    /// Description
    /// </summary>
    [Description("简介描述")]
    public string Description { get; set; } = "";

    /// <summary>
    /// 内容详情
    /// </summary>
    [Description("内容详情")]
    public string Content { get; set; } = "";

    /// <summary>
    /// 模块数量
    /// </summary>
    [Description("模块数量")]
    public int ModuleCount { get; set; } = 0;

    /// <summary>
    /// 权限集合
    /// </summary>
    [Description("权限集合")]
    public string Competence { get; set; } = "";

    /// <summary>
    /// 超级管理
    /// </summary>
    [Description("超级管理")]
    public int WhetherSuper { get; set; } = 0;

    /// <summary>
    /// 状态码
    /// </summary>
    [Description("状态码")]
    public int Status { get; set; } = 0;

    /// <summary>
    /// Ip
    /// </summary>
    [Description("Ip")]
    public string Ip { get; set; } = "";

    /// <summary>
    /// 创建人标识
    /// </summary>
    [Description("创建人标识")]
    public long CreateUserId { get; set; } = 0;

    /// <summary>
    /// 创建时间"
    /// </summary>
    [Description("创建时间")]
    public DateTime CreateTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 更新人标识
    /// </summary>
    [Description("更新人标识")]
    public long UpdateUserId { get; set; } = 0;

    /// <summary>
    /// 更新时间
    /// </summary>
    [Description("更新时间")]
    public DateTime UpdateTime { get; set; } = DateTime.Now;
}