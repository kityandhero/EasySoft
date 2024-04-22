namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

/// <summary>
/// 部门
/// </summary>
[AdvanceTableInformation("部门")]
[AdvanceTableMapper("department")]
public class Department : AbstractFunctionEntity<Department>
{
    #region Properties

    [AdvanceColumnInformation("上级标识")]
    [AdvanceColumnMapper("parent_id")]
    public long ParentId { get; set; } = 0;

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

    [AdvanceColumnInformation("归属模式")]
    [AdvanceColumnMapper("ownership_mode")]
    public int OwnershipMode { get; set; } = 0;

    [AdvanceColumnInformation("所属公司标识")]
    [AdvanceColumnMapper("subsidiary_id")]
    public long SubsidiaryId { get; set; } = 0;

    [AdvanceColumnInformation("排序值")]
    [AdvanceColumnMapper("sort")]
    public int Sort { get; set; } = 0;

    #endregion Properties
}