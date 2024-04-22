namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("sms_log")]
public class SmsLog : AbstractFunctionEntity<SmsLog>
{
    #region Properties

    [AdvanceColumnInformation("类别")]
    [AdvanceColumnMapper("sms_category_id")]
    public long SmsCategoryId { get; set; } = 0;

    [AdvanceColumnInformation("手机号码")]
    [AdvanceColumnMapper("phone")]
    [AdvanceColumnLength(50)]
    public string Phone { get; set; } = "";

    [AdvanceColumnInformation("内容")]
    [AdvanceColumnMapper("content")]
    [AdvanceColumnNational]
    public string Content { get; set; } = "";

    [AdvanceColumnInformation("发送时间")]
    [AdvanceColumnMapper("send_time")]
    [AdvanceColumnDefaultValue(ConstCollection.DatabaseDefaultDateTime)]
    public DateTime SendTime { get; set; } = ConstCollection.DbDefaultDateTime;

    [AdvanceColumnInformation("应用标识")]
    [AdvanceColumnMapper("application_id")]
    public long ApplicationId { get; set; } = 0L;

    [AdvanceColumnInformation("汇总状态")]
    [AdvanceColumnMapper("aggregate")]
    public int Aggregate { get; set; } = 0;

    [AdvanceColumnInformation("错误信息")]
    [AdvanceColumnMapper("error_message")]
    [AdvanceColumnNational]
    public string ErrorMessage { get; set; } = "";

    [AdvanceColumnInformation("验证码")]
    [AdvanceColumnMapper("code")]
    [AdvanceColumnLength(50)]
    public string Code { get; set; } = "";

    #endregion Properties
}