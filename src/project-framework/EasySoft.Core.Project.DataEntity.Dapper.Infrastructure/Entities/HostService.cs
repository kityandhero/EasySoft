using EasySoft.UtilityTools.Core.Assists;

namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("host_service")]
public class HostService : AbstractFunctionEntity<HostService>, IHostServiceLog
{
    #region Properties

    [AdvanceColumnInformation("名称")]
    [AdvanceColumnMapper("name")]
    [AdvanceColumnLength(100)]
    [AdvanceColumnNational]
    public string Name { get; set; } = "";

    [AdvanceColumnInformation("是否使用服务维持")]
    [AdvanceColumnMapper("daemon")]
    public int Daemon { get; set; } = Whether.No.ToInt();

    [AdvanceColumnInformation("服务识别码")]
    [AdvanceColumnMapper("service_channel")]
    public string ServiceChannel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

    [AdvanceColumnInformation("是否已经变更")]
    [AdvanceColumnMapper("changed")]
    public int Changed { get; set; } = (int)HostServiceChanged.Other;

    [AdvanceColumnInformation("变更类型")]
    [AdvanceColumnMapper("change_type")]
    public int ChangeType { get; set; } = (int)HostServiceChangeType.Other;

    [AdvanceColumnInformation("操作Ip")]
    [AdvanceColumnMapper("ip")]
    [AdvanceColumnLength(50)]
    public string Ip { get; set; } = IPAssist.GetLocalIP();

    [AdvanceColumnInformation("触发渠道码")]
    [AdvanceColumnMapper("trigger_channel")]
    [AdvanceColumnLength(200)]
    [AdvanceColumnNational]
    public string TriggerChannel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

    #endregion
}