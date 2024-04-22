namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

/// <summary>
/// 驻留服务日志
/// </summary>
[AdvanceTableInformation("访问模块")]
[AdvanceTableMapper("host_service_log")]
public class HostServiceLog : AbstractFunctionEntity<HostServiceLog>, IHostServiceLogStore
{
    #region Properties

    [AdvanceColumnInformation("访问名称")]
    [AdvanceColumnMapper("name")]
    [AdvanceColumnLength(2000)]
    [AdvanceColumnNational]
    public string Name { get; set; } = "";

    [AdvanceColumnInformation("服务识别码")]
    [AdvanceColumnMapper("service_channel")]
    public string ServiceChannel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

    [AdvanceColumnInformation("变动类型")]
    [AdvanceColumnMapper("change_type")]
    public int ChangeType { get; set; } = (int)HostServiceChangeType.Other;

    [AdvanceColumnInformation("触发渠道码")]
    [AdvanceColumnMapper("trigger_channel")]
    [AdvanceColumnLength(200)]
    [AdvanceColumnNational]
    public string TriggerChannel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

    [AdvanceColumnInformation("Ip")]
    [AdvanceColumnMapper("ip")]
    [AdvanceColumnLength(50)]
    public string Ip { get; set; } = "";

    #endregion Properties
}