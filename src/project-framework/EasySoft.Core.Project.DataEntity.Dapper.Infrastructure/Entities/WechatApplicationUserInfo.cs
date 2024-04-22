namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("wechat_application_user_info")]
public class WechatApplicationUserInfo : AbstractFunctionEntity<WechatApplicationUserInfo>
{
    #region Properties

    [AdvanceColumnMapper("user_id")]
    public long UserId { get; set; } = 0;

    [AdvanceColumnMapper("open_id")]
    [AdvanceColumnLength(50)]
    public string OpenId { get; set; } = "";

    [AdvanceColumnMapper("union_id")]
    [AdvanceColumnLength(50)]
    public string UnionId { get; set; } = "";

    [AdvanceColumnMapper("latitude_register")]
    public decimal LatitudeRegister { get; set; } = 0;

    [AdvanceColumnMapper("longitude_register")]
    public decimal LongitudeRegister { get; set; } = 0;

    [AdvanceColumnMapper("latitude_last_sign_in")]
    public decimal LatitudeLastSignIn { get; set; } = 0;

    [AdvanceColumnMapper("longitude_last_sign_in")]
    public decimal LongitudeLastSignIn { get; set; } = 0;

    [AdvanceColumnMapper("application_id")]
    public long ApplicationId { get; set; } = 0;

    #endregion Properties
}