namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("sms_config")]
public class SmsConfig : AbstractFunctionEntity<SmsConfig>
{
    #region Properties

    [AdvanceColumnInformation("可用数量")]
    [AdvanceColumnMapper("available_quantity")]
    public int AvailableQuantity { get; set; } = 0;

    #endregion Properties
}