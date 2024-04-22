namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

/// <summary>
/// 一般日志
/// </summary>
[AdvanceTableInformation("访问模块")]
[AdvanceTableMapper("general_log")]
public class GeneralLog : AbstractFunctionEntity<GeneralLog>, IGeneralLogStore
{
    #region Properties

    [AdvanceColumnInformation("消息描述")]
    [AdvanceColumnMapper("message")]
    [AdvanceColumnNational]
    public string Message { get; set; } = "";

    [AdvanceColumnInformation("消息描述")]
    [AdvanceColumnMapper("description")]
    [AdvanceColumnNational]
    public string Description { get; set; } = "";

    [AdvanceColumnInformation("附属信息")]
    [AdvanceColumnMapper("ancillary_information")]
    [AdvanceColumnNational]
    public string AncillaryInformation { get; set; } = "";

    [AdvanceColumnInformation("消息描述数据类型")]
    [AdvanceColumnMapper("message_type")]
    public int MessageType { get; set; } = (int)CustomValueType.PlainValue;

    [AdvanceColumnInformation("消息内容")]
    [AdvanceColumnMapper("content")]
    [AdvanceColumnNational]
    public string Content { get; set; } = "";

    [AdvanceColumnInformation("消息内容数据类型")]
    [AdvanceColumnMapper("content_type")]
    public int ContentType { get; set; } = (int)CustomValueType.PlainValue;

    [AdvanceColumnInformation("消息类型")]
    [AdvanceColumnMapper("type")]
    public int Type { get; set; } = (int)GeneralLogType.Unknown;

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