namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableInformation("用户分公司关系")]
[AdvanceTableMapper("user_subsidiary_info")]
public class UserSubsidiaryInfo : AbstractFunctionEntity<UserSubsidiaryInfo>
{
    #region Properties

    [AdvanceColumnInformation("用户标识")]
    [AdvanceColumnMapper("user_id")]
    public long UserId { get; set; } = 0;

    [AdvanceColumnInformation("分公司标识")]
    [AdvanceColumnMapper("subsidiary_id")]
    public long SubsidiaryId { get; set; } = 0;

    [AdvanceColumnInformation("主要企业")]
    [AdvanceColumnMapper("whether_primary")]
    public int WhetherPrimary { get; set; } = 0;

    #endregion Properties
}