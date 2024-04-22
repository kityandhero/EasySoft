namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

public class WeChatMessage : AbstractFunctionEntity<WeChatMessage>, IWeChatMessageStore
{
    #region Properties

    [AdvanceColumnInformation("应用标识")]
    [AdvanceColumnMapper("application_id")]
    public long ApplicationId { get; set; } = 0;

    [AdvanceColumnInformation("用户标识")]
    [AdvanceColumnMapper("user_id")]
    public long UserId { get; set; } = 0;

    [AdvanceColumnInformation("客户端类型")]
    [AdvanceColumnMapper("client_type")]
    public int ClientType { get; set; } = 0;

    [AdvanceColumnInformation("OpenId")]
    [AdvanceColumnMapper("open_id")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string OpenId { get; set; } = "";

    [AdvanceColumnInformation("消息模板 templateId")]
    [AdvanceColumnMapper("template_id")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string TemplateId { get; set; } = "";

    [AdvanceColumnInformation("templateKey")]
    [AdvanceColumnMapper("template_key")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string TemplateKey { get; set; } = "";

    [AdvanceColumnInformation("Url")]
    [AdvanceColumnMapper("url")]
    [AdvanceColumnLength(4000)]
    [AdvanceColumnNational]
    public string Url { get; set; } = "";

    [AdvanceColumnInformation("消息参数")]
    [AdvanceColumnMapper("json_data")]
    [AdvanceColumnNational]
    public string JsonData { get; set; } = "";

    [AdvanceColumnInformation("模板需要放大的关键词，不填则默认无放大（小程序）")]
    [AdvanceColumnMapper("emphasis_keyword")]
    [AdvanceColumnLength(2000)]
    [AdvanceColumnNational]
    public string EmphasisKeyword { get; set; } = "";

    [AdvanceColumnInformation("模板内容字体的颜色，不填默认黑色（非必填）（小程序）")]
    [AdvanceColumnMapper("color")]
    [AdvanceColumnLength(200)]
    [AdvanceColumnNational]
    public string Color { get; set; } = "";

    [AdvanceColumnInformation("表单提交场景下，为 submit 事件带上的 formId；支付场景下，为本次支付的 prepay_id（小程序）")]
    [AdvanceColumnMapper("form_id")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string FormId { get; set; } = "";

    [AdvanceColumnInformation("AppId")]
    [AdvanceColumnMapper("app_id")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string AppId { get; set; } = "";

    [AdvanceColumnInformation("点击模板查看详情跳转页面，不填则模板无跳转（非必填）（小程序）")]
    [AdvanceColumnMapper("page")]
    [AdvanceColumnLength(4000)]
    [AdvanceColumnNational]
    public string Page { get; set; } = "";

    [AdvanceColumnInformation("MiniProgramAppId")]
    [AdvanceColumnMapper("mini_program_app_id")]
    public string MiniProgramAppId { get; set; } = "";

    [AdvanceColumnInformation("MiniProgramPagePath")]
    [AdvanceColumnMapper("mini_program_page_path")]
    [AdvanceColumnLength(2000)]
    [AdvanceColumnNational]
    public string MiniProgramPagePath { get; set; } = "";

    [AdvanceColumnInformation("Mode")]
    [AdvanceColumnMapper("mode")]
    public int Mode { get; set; } = 0;

    [AdvanceColumnInformation("发送时间")]
    [AdvanceColumnMapper("send_time")]
    [AdvanceColumnDefaultValue(ConstCollection.DatabaseDefaultDateTime)]
    public DateTime SendTime { get; set; } = ConstCollection.DatabaseDefaultDateTime.ToDateTime();

    [AdvanceColumnInformation("发送UnixTime")]
    [AdvanceColumnMapper("send_unix_time")]
    public long SendUnixTime { get; set; } = 0;

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