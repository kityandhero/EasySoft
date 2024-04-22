namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("sms_category_statistic")]
public class SmsCategoryStatistic : AbstractFunctionEntity<SmsCategoryStatistic>
{
    #region Properties

    [AdvanceColumnInformation("发送数量")]
    [AdvanceColumnMapper("total_count")]
    public int TotalCount { get; set; } = 0;

    [AdvanceColumnInformation("类别码")]
    [AdvanceColumnMapper("sms_category_id")]
    public long SmsCategoryId { get; set; } = 0;

    #endregion Properties
}