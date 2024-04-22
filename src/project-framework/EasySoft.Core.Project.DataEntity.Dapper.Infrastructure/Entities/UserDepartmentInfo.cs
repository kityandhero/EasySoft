namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableInformation("用户部门关系")]
[AdvanceTableMapper("user_department_info")]
public class UserDepartmentInfo : AbstractFunctionEntity<UserDepartmentInfo>
{
    #region Properties

    [AdvanceColumnInformation("用户标识")]
    [AdvanceColumnMapper("user_id")]
    public long UserId { get; set; } = 0;

    [AdvanceColumnInformation("部门标识")]
    [AdvanceColumnMapper("department_id")]
    public long DepartmentId { get; set; }

    [AdvanceColumnInformation("主要企业")]
    [AdvanceColumnMapper("whether_primary")]
    public int WhetherPrimary { get; set; }

    #endregion Properties
}