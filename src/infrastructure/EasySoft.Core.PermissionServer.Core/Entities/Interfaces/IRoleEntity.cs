namespace EasySoft.Core.PermissionServer.Core.Entities.Interfaces;

/// <summary>
/// IRoleEntity
/// </summary>
public interface IRoleEntity : IRolePersistence
{
    /// <summary>
    /// 名称  
    /// </summary>
    [Description("名称")]
    string Name { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    [Description("简介描述")]
    string Description { get; set; }

    /// <summary>
    /// 内容详情
    /// </summary>
    [Description("内容详情")]
    string Content { get; set; }

    /// <summary>
    /// 模块数量
    /// </summary>
    [Description("模块数量")]
    int ModuleCount { get; set; }
}