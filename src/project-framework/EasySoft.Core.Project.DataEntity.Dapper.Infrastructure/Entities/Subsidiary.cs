namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

/// <summary>
/// 子公司
/// </summary>
[AdvanceTableInformation("子公司")]
[AdvanceTableMapper("subsidiary")]
public class Subsidiary : AbstractFunctionEntity<Subsidiary>, ISubsidiaryPure
{
    #region Properties

    [AdvanceColumnInformation("简称")]
    [AdvanceColumnMapper("short_name")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string ShortName { get; set; } = "";

    [AdvanceColumnInformation("全称")]
    [AdvanceColumnMapper("full_name")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string FullName { get; set; } = "";

    [AdvanceColumnInformation("上级标识")]
    [AdvanceColumnMapper("parent_id")]
    public long ParentId { get; set; } = 0;

    [AdvanceColumnInformation("内部编码")]
    [AdvanceColumnMapper("code")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string Code { get; set; } = "";

    [AdvanceColumnInformation("排序值")]
    [AdvanceColumnMapper("sort")]
    public int Sort { get; set; } = 0;

    [AdvanceColumnInformation("图片")]
    [AdvanceColumnMapper("logo")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Logo { get; set; } = "";

    [AdvanceColumnInformation("简介描述")]
    [AdvanceColumnMapper("description")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Description { get; set; } = "";

    #endregion Properties
}