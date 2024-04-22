namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("master_manager")]
public class MasterManager : AbstractFunctionEntity<MasterManager>
{
    #region Properties

    [AdvanceColumnMapper("login_name")]
    [AdvanceColumnLength(80)]
    [AdvanceColumnNational]
    public string LoginName { get; set; } = "";

    [AdvanceColumnMapper("password")]
    [AdvanceColumnLength(200)]
    [AdvanceColumnNational]
    public string Password { get; set; } = "";

    [AdvanceColumnMapper("name")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string Name { get; set; } = "";

    [AdvanceColumnInformation("手机号码")]
    [AdvanceColumnMapper("phone")]
    [AdvanceColumnLength(50)]
    public string Phone { get; set; } = "";

    [AdvanceColumnInformation("电子邮箱")]
    [AdvanceColumnMapper("email")]
    [AdvanceColumnLength(50)]
    public string Email { get; set; } = "";

    [AdvanceColumnInformation("头像")]
    [AdvanceColumnMapper("avatar")]
    [AdvanceColumnLength(400)]
    public string Avatar { get; set; } = "";

    #endregion Properties
}