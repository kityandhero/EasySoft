namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// 微信消息体
/// </summary>
public interface IWeChatMessage
{
    /// <summary>
    /// 应用标识
    /// </summary>
    public long ApplicationId { get; set; }

    /// <summary>
    /// 用户标识
    /// </summary>   
    public long UserId { get; set; }

    /// <summary>
    /// 客户端类型
    /// </summary>
    public int ClientType { get; set; }

    /// <summary>
    /// OpenId
    /// </summary>
    public string OpenId { get; set; }

    /// <summary>
    /// 消息模板 templateId
    /// </summary>
    public string TemplateId { get; set; }

    /// <summary>
    /// templateKey
    /// </summary>
    public string TemplateKey { get; set; }

    /// <summary>
    /// Url
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 消息参数
    /// </summary>
    public string JsonData { get; set; }

    /// <summary>
    /// 模板需要放大的关键词，不填则默认无放大（小程序）
    /// </summary>
    public string EmphasisKeyword { get; set; }

    /// <summary>
    /// 模板内容字体的颜色，不填默认黑色（非必填）（小程序）
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    /// 表单提交场景下，为 submit 事件带上的 formId；支付场景下，为本次支付的 prepay_id（小程序）
    /// </summary>
    public string FormId { get; set; }

    /// <summary>
    /// 应用标识
    /// </summary>
    public string AppId { get; set; }

    /// <summary>
    /// 点击模板查看详情跳转页面，不填则模板无跳转（非必填）（小程序）
    /// </summary>
    public string Page { get; set; }

    /// <summary>
    /// 小程序标识
    /// </summary>
    public string MiniProgramAppId { get; set; }

    /// <summary>
    /// 小程序跳转路径
    /// </summary>
    public string MiniProgramPagePath { get; set; }

    /// <summary>
    /// 模式
    /// </summary>
    public int Mode { get; set; }

    /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime SendTime { get; set; }

    /// <summary>
    /// SendTime对应的UnixTime，用于排序较好
    /// </summary>
    public long SendUnixTime { get; set; }
}