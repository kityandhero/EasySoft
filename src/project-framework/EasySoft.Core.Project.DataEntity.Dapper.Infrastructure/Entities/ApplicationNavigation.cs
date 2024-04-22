namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

/// <summary>
/// 应用导航配置
/// </summary>
[AdvanceTableInformation("应用导航配置")]
[AdvanceTableMapper("application_navigation")]
public class ApplicationNavigation : AbstractFunctionEntity<ApplicationNavigation>
{
    #region Properties

    [AdvanceColumnInformation("应用标识")]
    [AdvanceColumnMapper("application_id")]
    public long ApplicationId { get; set; } = 0;

    [AdvanceColumnInformation("名称")]
    [AdvanceColumnMapper("name")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string Name { get; set; } = "";

    [AdvanceColumnInformation("唯一标记")]
    [AdvanceColumnMapper("unique_mark")]
    [AdvanceColumnLength(200)]
    [AdvanceColumnNational]
    public string UniqueMark { get; set; } = "";

    [AdvanceColumnInformation("适用路径")]
    [AdvanceColumnMapper("target_path")]
    [AdvanceColumnLength(200)]
    public string TargetPath { get; set; } = "";

    [AdvanceColumnInformation("导航集合")]
    [AdvanceColumnMapper("navigation_collection")]
    [AdvanceColumnNational]
    public string NavigationCollection { get; set; } = "";

    #endregion Properties
}