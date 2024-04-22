namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableInformation("主控管理人员角色")]
[AdvanceTableMapper("master_manager_role")]
public class MasterManagerRole : AbstractFunctionEntity<MasterManagerRole>
{
    #region Properties

    [AdvanceColumnInformation("管理人数据标识")]
    [AdvanceColumnMapper("master_manager_id")]
    public long MasterManagerId { get; set; } = 0;

    [AdvanceColumnInformation("模板角色集合")]
    [AdvanceColumnMapper("preset_role_item_collection")]
    [AdvanceColumnNational]
    public string PresetRoleCollection { get; set; } = "";

    #endregion
}