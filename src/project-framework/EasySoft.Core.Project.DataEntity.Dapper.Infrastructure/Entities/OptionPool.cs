namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableInformation("选项池集合")]
[AdvanceTableMapper("option_pool")]
public class OptionPool : AbstractFunctionEntity<OptionPool>
{
    #region Properties

    [AdvanceColumnInformation("标题")]
    [AdvanceColumnMapper("title")]
    [AdvanceColumnLength(100)]
    [AdvanceColumnNational]
    public string Title { get; set; } = "";

    [AdvanceColumnInformation("简介描述")]
    [AdvanceColumnMapper("description")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Description { get; set; } = "";

    [AdvanceColumnInformation("排序值")]
    [AdvanceColumnMapper("sort")]
    public int Sort { get; set; } = 0;

    [AdvanceColumnInformation("类别码")]
    [AdvanceColumnMapper("category")]
    public int Category { get; set; } = 0;

    [AdvanceColumnInformation("业务模式")]
    [AdvanceColumnMapper("business_mode")]
    public int BusinessMode { get; set; } = 0;

    #endregion Properties
}