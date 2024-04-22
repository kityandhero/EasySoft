namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

/// <summary>
/// 应用
/// </summary>
[AdvanceTableInformation("应用")]
[AdvanceTableMapper("application")]
public class Application : AbstractFunctionEntity<Application>, IApplication
{
    #region Properties

    [AdvanceColumnInformation("应用源标识")]
    [AdvanceColumnMapper("application_source_id")]
    public long ApplicationSourceId { get; set; } = 0;

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

    [AdvanceColumnInformation("Logo")]
    [AdvanceColumnMapper("logo")]
    [AdvanceColumnLength(800)]
    [AdvanceColumnNational]
    public string Logo { get; set; } = "";

    [AdvanceColumnInformation("简介描述")]
    [AdvanceColumnMapper("description")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Description { get; set; } = "";

    [AdvanceColumnInformation("类型")]
    [AdvanceColumnMapper("type")]
    public int Type { get; set; } = 0;

    /// <summary>
    /// 第三方AppId
    /// </summary>
    [AdvanceColumnInformation("AppId")]
    [AdvanceColumnMapper("app_id")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string AppId { get; set; } = "";

    /// <summary>
    /// 第三方AppSecret
    /// </summary>
    [AdvanceColumnInformation("AppSecret")]
    [AdvanceColumnMapper("app_secret")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string AppSecret { get; set; } = "";

    /// <summary>
    /// 第三方支付账户标识
    /// </summary>
    [AdvanceColumnInformation("MerchantId")]
    [AdvanceColumnMapper("merchant_id")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string MerchantId { get; set; } = "";

    /// <summary>
    /// 第三方App Key
    /// </summary>
    [AdvanceColumnInformation("AppKey")]
    [AdvanceColumnMapper("app_key")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string AppKey { get; set; } = "";

    [AdvanceColumnInformation("SubAppId")]
    [AdvanceColumnMapper("sub_app_id")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string SubAppId { get; set; } = "";

    [AdvanceColumnInformation("SubAppSecret")]
    [AdvanceColumnMapper("sub_app_secret")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string SubAppSecret { get; set; } = "";

    [AdvanceColumnInformation("SubMerchantId")]
    [AdvanceColumnMapper("sub_merchant_id")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string SubMerchantId { get; set; } = "";

    [AdvanceColumnInformation("MerchantType")]
    [AdvanceColumnMapper("merchant_type")]
    public int MerchantType { get; set; } = 0;

    [AdvanceColumnInformation("第三方证书")]
    [AdvanceColumnMapper("certificate")]
    public string Certificate { get; set; } = "";

    [AdvanceColumnInformation("证书密码")]
    [AdvanceColumnMapper("certificate_password")]
    [AdvanceColumnLength(80)]
    [AdvanceColumnNational]
    public string CertificatePassword { get; set; } = "";

    [AdvanceColumnInformation("证书类型")]
    [AdvanceColumnMapper("certificate_type")]
    public int CertificateType { get; set; } = 0;

    [AdvanceColumnInformation("七牛域名")]
    [AdvanceColumnMapper("qn_host")]
    [AdvanceColumnLength(50)]
    public string QnHost { get; set; } = "";

    [AdvanceColumnInformation("AccessToken")]
    [AdvanceColumnMapper("access_token")]
    [AdvanceColumnLength(200)]
    public string AccessToken { get; set; } = "";

    [AdvanceColumnInformation("AccessToken有效时间（秒）")]
    [AdvanceColumnMapper("access_token_valid_second")]
    public int AccessTokenValidSecond { get; set; } = 0;

    [AdvanceColumnInformation("AccessToken过期时间")]
    [AdvanceColumnMapper("access_token_expire_time")]
    [AdvanceColumnDefaultValue(ConstCollection.DatabaseDefaultDateTime)]
    public DateTime AccessTokenExpireTime { get; set; } = DateTime.Now;

    [AdvanceColumnInformation("平台主消息通道")]
    [AdvanceColumnMapper("message_channel_application_id")]
    public long MessageChannelApplicationId { get; set; } = 0;

    [AdvanceColumnInformation("维护时间")]
    [AdvanceColumnMapper("maintain_time")]
    [AdvanceColumnDefaultValue(ConstCollection.DatabaseDefaultDateTime)]
    public DateTime MaintainTime { get; set; } = DateTime.Now;

    #endregion Properties
}