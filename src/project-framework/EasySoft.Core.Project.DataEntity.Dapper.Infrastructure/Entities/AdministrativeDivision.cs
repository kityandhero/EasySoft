namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("administrative_division")]
public class AdministrativeDivision : AbstractFunctionEntity<AdministrativeDivision>
{
    #region Properties

    [AdvanceColumnInformation("地区代码")]
    [AdvanceColumnMapper("code")]
    public long Code { get; set; } = 0;

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

    [AdvanceColumnInformation("字母")]
    [AdvanceColumnMapper("letter")]
    [AdvanceColumnLength(100)]
    [AdvanceColumnNational]
    public string Letter { get; set; } = "";

    [AdvanceColumnInformation("行政中心")]
    [AdvanceColumnMapper("municipal")]
    public int Municipal { get; set; } = 0;

    [AdvanceColumnInformation("简介描述")]
    [AdvanceColumnMapper("description")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Description { get; set; } = "";

    [AdvanceColumnInformation("上级代码")]
    [AdvanceColumnMapper("parent_code")]
    public long ParentCode { get; set; } = 0;

    [AdvanceColumnInformation("地区等级")]
    [AdvanceColumnMapper("level")]

    public int Level { get; set; } = 0;

    [AdvanceColumnInformation("是否有下级")]
    [AdvanceColumnMapper("has_child")]

    public int HasChild { get; set; } = 0;

    [AdvanceColumnInformation("是否首都")]
    [AdvanceColumnMapper("whether_capital")]
    public int WhetherCapital { get; set; } = 0;

    [AdvanceColumnInformation("热门城市")]
    [AdvanceColumnMapper("hot")]
    public int Hot { get; set; } = 0;

    [AdvanceColumnMapper("regional_administrative_rand")]
    public int RegionalAdministrativeRand { get; set; } = 0;

    [AdvanceColumnInformation("内容")]
    [AdvanceColumnMapper("content")]
    [AdvanceColumnNational]
    public string Content { get; set; } = "";

    [AdvanceColumnInformation("贫困地区")]
    [AdvanceColumnMapper("poverty")]
    public int Poverty { get; set; } = 0;

    [AdvanceColumnInformation("首字母")]
    [AdvanceColumnMapper("initials_set")]
    [AdvanceColumnLength(10)]
    [AdvanceColumnNational]
    public string InitialsSet { get; set; } = "";

    [AdvanceColumnInformation("来源名称")]
    [AdvanceColumnMapper("source_name")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string SourceName { get; set; } = "";

    [AdvanceColumnInformation("经度")]
    [AdvanceColumnMapper("longitude")]
    [AdvanceColumnAccuracy(6)]
    public decimal Longitude { get; set; } = 0;

    [AdvanceColumnInformation("纬度")]
    [AdvanceColumnMapper("latitude")]
    [AdvanceColumnAccuracy(6)]
    public decimal Latitude { get; set; } = 0;

    [AdvanceColumnInformation("操作备注")]
    [AdvanceColumnMapper("admin_remark")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string AdminRemark { get; set; } = "";

    [AdvanceColumnInformation("IP地址")]
    [AdvanceColumnMapper("ip")]
    public string IP { get; set; } = "";

    [AdvanceColumnInformation("备注")]
    [AdvanceColumnMapper("remark")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Remark { get; set; } = "";

    #endregion Properties
}