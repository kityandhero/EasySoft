namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("sms_category")]
public class SmsCategory : AbstractFunctionEntity<SmsCategory>
{
    #region Properties

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

    [AdvanceColumnInformation("特征值，对应于系统预设")]
    [AdvanceColumnMapper("flag")]
    public int Flag { get; set; } = 0;

    [AdvanceColumnInformation("短信模板")]
    [AdvanceColumnMapper("template")]
    [AdvanceColumnLength(1000)]
    [AdvanceColumnNational]
    public string Template { get; set; } = "";

    #endregion Properties
}