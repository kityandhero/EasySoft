namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("application_source")]
public class ApplicationSource : AbstractFunctionEntity<ApplicationSource>
{
    #region Properties

    [AdvanceColumnInformation("名称")]
    [AdvanceColumnMapper("name")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string Name { get; set; } = "";

    [AdvanceColumnInformation("简称")]
    [AdvanceColumnMapper("short_name")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string ShortName { get; set; } = "";

    [AdvanceColumnInformation("Loog")]
    [AdvanceColumnMapper("logo")]
    [AdvanceColumnLength(800)]
    [AdvanceColumnNational]
    public string Logo { get; set; } = "";

    [AdvanceColumnInformation("简介描述")]
    [AdvanceColumnMapper("description")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Description { get; set; } = "";

    [AdvanceColumnInformation("类型")]
    [AdvanceColumnMapper("type")]
    public int Type { get; set; } = 0;

    [AdvanceColumnInformation("创建模式")]
    [AdvanceColumnMapper("create_mode")]
    public int CreateMode { get; set; } = 0;

    #endregion Properties
}