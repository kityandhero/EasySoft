namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableInformation("数据标签")]
[AdvanceTableMapper("tag")]
public class Tag : AbstractFunctionEntity<Tag>
{
    #region Properties

    [AdvanceColumnInformation("名称")]
    [AdvanceColumnMapper("name")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string Name { get; set; } = "";

    [AdvanceColumnInformation("色值")]
    [AdvanceColumnMapper("color")]
    [AdvanceColumnLength(20)]
    public string Color { get; set; } = "";

    [AdvanceColumnInformation("显示名称")]
    [AdvanceColumnMapper("display_name")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string DisplayName { get; set; } = "";

    [AdvanceColumnInformation("排序值")]
    [AdvanceColumnMapper("sort")]
    public int Sort { get; set; } = 0;

    [AdvanceColumnInformation("图片")]
    [AdvanceColumnMapper("image")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Image { get; set; } = "";

    [AdvanceColumnInformation("简介描述")]
    [AdvanceColumnMapper("description")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Description { get; set; } = "";

    [AdvanceColumnInformation("显示区域")]
    [AdvanceColumnMapper("display_range")]
    public int DisplayRange { get; set; } = 0;

    [AdvanceColumnInformation("是否推荐")]
    [AdvanceColumnMapper("whether_recommend")]
    public int WhetherRecommend { get; set; } = 0;

    [AdvanceColumnInformation("类型")]
    [AdvanceColumnMapper("type")]
    public int Type { get; set; } = 0;

    [AdvanceColumnInformation("适用业务")]
    [AdvanceColumnMapper("business_mode")]
    public int BusinessMode { get; set; } = 0;

    [AdvanceColumnInformation("自定义备注")]
    [AdvanceColumnMapper("remark")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Remark { get; set; } = "";

    #endregion Properties
}