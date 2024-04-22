namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableInformation("内测用户")]
[AdvanceTableMapper("internal_tester")]
public class InternalTester : AbstractFunctionEntity<InternalTester>
{
    #region Properties

    [AdvanceColumnInformation("用户标识")]
    [AdvanceColumnMapper("user_id")]
    public long UserId { get; set; } = 0L;

    [AdvanceColumnInformation("备注")]
    [AdvanceColumnMapper("note")]
    [AdvanceColumnLength(400)]
    [AdvanceColumnNational]
    public string Note { get; set; } = "";

    #endregion Properties
}