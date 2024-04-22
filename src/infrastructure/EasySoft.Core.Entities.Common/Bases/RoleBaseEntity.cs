namespace EasySoft.Core.Entities.Common.Bases;

public abstract class RoleBaseEntity<T> : AbstractFunctionEntity<T> where T : BaseEntity<T>
{
    [AdvanceColumnInformation("名称")]
    [AdvanceColumnMapper("name")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string Name { get; set; } = "";

    [AdvanceColumnInformation("简介描述")]
    [AdvanceColumnMapper("description")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Description { get; set; } = "";

    [AdvanceColumnInformation("内容详情")]
    [AdvanceColumnMapper("content")]
    [AdvanceColumnNational]
    public string Content { get; set; } = "";

    [AdvanceColumnInformation("模块数量")]
    [AdvanceColumnMapper("module_count")]
    public int ModuleCount { get; set; } = 0;

    [AdvanceColumnInformation("权限集合")]
    [AdvanceColumnMapper("competence")]
    public string Competence { get; set; } = "";

    [AdvanceColumnInformation("超级管理")]
    [AdvanceColumnMapper("is_super")]
    public int IsSuper { get; set; } = 0;

    [AdvanceColumnInformation("Ip")]
    [AdvanceColumnMapper("ip")]
    [AdvanceColumnLength(50)]
    public string Ip { get; set; } = "";
}