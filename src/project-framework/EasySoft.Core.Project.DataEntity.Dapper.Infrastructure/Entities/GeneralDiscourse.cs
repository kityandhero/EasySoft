namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableInformation("常用语")]
[AdvanceTableMapper("general_discourse")]
public class GeneralDiscourse : AbstractFunctionEntity<GeneralDiscourse>
{
    #region Properties

    [AdvanceColumnInformation("内容")]
    [AdvanceColumnMapper("content")]
    [AdvanceColumnLength(1000)]
    [AdvanceColumnNational]
    public string Content { get; set; } = "";

    [AdvanceColumnInformation("排序值")]
    [AdvanceColumnMapper("sort")]
    public int Sort { get; set; }

    [AdvanceColumnInformation("类别")]
    [AdvanceColumnMapper("type")]
    public int Type { get; set; } = 0;

    #endregion Properties
}