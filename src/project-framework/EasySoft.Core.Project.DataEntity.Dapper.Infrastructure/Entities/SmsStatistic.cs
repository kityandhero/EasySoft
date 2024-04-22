namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("sms_statistic")]
public class SmsStatistic : AbstractFunctionEntity<SmsStatistic>
{
    #region Properties

    [AdvanceColumnInformation("发送数量")]
    [AdvanceColumnMapper("total_count")]
    public int TotalCount { get; set; } = 0;

    #endregion Properties
}