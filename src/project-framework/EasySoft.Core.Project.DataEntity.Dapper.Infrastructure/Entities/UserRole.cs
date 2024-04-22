namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("user_role")]
public class UserRole : AbstractFunctionEntity<UserRole>
{
    #region Properties

    [AdvanceColumnInformation("用户标识")]
    [AdvanceColumnMapper("user_id")]
    public long UserId { get; set; } = 0;

    [AdvanceColumnInformation("模板角色集合")]
    [AdvanceColumnMapper("preset_role_item_collection")]
    [AdvanceColumnNational]
    public string PresetRoleCollection { get; set; } = "";

    #endregion
}