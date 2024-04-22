namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableInformation("用户登录名")]
[AdvanceTableMapper("user_login")]
public class UserLogin : AbstractFunctionEntity<UserLogin>
{
    #region Properties

    [AdvanceColumnInformation("用户标识")]
    [AdvanceColumnMapper("user_id")]
    public long UserId { get; set; } = 0;

    /// <summary>
    /// 登录名
    /// </summary>   
    [AdvanceColumnInformation("登录名")]
    [AdvanceColumnMapper("login_name")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string LoginName { get; set; } = "";

    /// <summary>
    /// 登录密码
    /// </summary>
    [AdvanceColumnInformation("登录密码")]
    [AdvanceColumnMapper("password")]
    [AdvanceColumnLength(200)]
    public string Password { get; set; } = "";

    #endregion Properties
}