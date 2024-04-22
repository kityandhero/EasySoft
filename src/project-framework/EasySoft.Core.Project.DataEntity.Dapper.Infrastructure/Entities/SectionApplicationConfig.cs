namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableInformation("栏目应用配置")]
[AdvanceTableMapper("section_application_config")]
public class SectionApplicationConfig : AbstractFunctionEntity<SectionApplicationConfig>
{
    #region Properties

    [AdvanceColumnInformation("名称")]
    [AdvanceColumnMapper("name")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string Name { get; set; } = "";

    [AdvanceColumnInformation("适用路径")]
    [AdvanceColumnMapper("target_path")]
    [AdvanceColumnLength(400)]
    public string TargetPath { get; set; } = "";

    [AdvanceColumnInformation("栏目标识")]
    [AdvanceColumnMapper("section_id")]
    public long SectionId { get; set; } = 0;

    [AdvanceColumnInformation("应用标识")]
    [AdvanceColumnMapper("application_id")]
    public long ApplicationId { get; set; } = 0;

    [AdvanceColumnInformation("自定义配置集合")]
    [AdvanceColumnMapper("custom_configuration")]
    [AdvanceColumnNational]
    public string CustomConfiguration { get; set; } = "";

    #endregion Properties
}