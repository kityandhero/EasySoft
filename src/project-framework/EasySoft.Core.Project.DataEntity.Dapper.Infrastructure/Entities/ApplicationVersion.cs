namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("application_version")]
public class ApplicationVersion : AbstractFunctionEntity<ApplicationVersion>
{
    #region Properties

    [AdvanceColumnInformation("应用标识")]
    [AdvanceColumnMapper("application_id")]
    public long ApplicationId { get; set; } = 0;

    [AdvanceColumnInformation("标题")]
    [AdvanceColumnMapper("title")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string Title { get; set; } = "";

    [AdvanceColumnInformation("最小版本")]
    [AdvanceColumnMapper("min_version")]
    [AdvanceColumnLength(200)]
    public string MinVersion { get; set; } = "";

    [AdvanceColumnInformation("Loog")]
    [AdvanceColumnMapper("url")]
    [AdvanceColumnLength(2000)]
    public string Url { get; set; } = "";

    [AdvanceColumnInformation("最大版本")]
    [AdvanceColumnMapper("max_version")]
    [AdvanceColumnLength(200)]
    public string MaxVersion { get; set; } = "";

    [AdvanceColumnInformation("简介描述")]
    [AdvanceColumnMapper("description")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Description { get; set; } = "";

    [AdvanceColumnInformation("内部版本号")]
    [AdvanceColumnMapper("internal_version")]
    public int InternalVersion { get; set; } = 0;

    [AdvanceColumnInformation("设备类型")]
    [AdvanceColumnMapper("device_type")]
    public int DeviceType { get; set; } = 0;

    #endregion Properties
}