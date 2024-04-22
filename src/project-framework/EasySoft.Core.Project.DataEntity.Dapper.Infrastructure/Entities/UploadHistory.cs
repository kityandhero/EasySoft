namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("upload_history")]
public class UploadHistory : AbstractFunctionEntity<UploadHistory>
{
    #region Properties

    [AdvanceColumnInformation("名称")]
    [AdvanceColumnMapper("name")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Name { get; set; } = "";

    [AdvanceColumnInformation("别名")]
    [AdvanceColumnMapper("alias")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Alias { get; set; } = "";

    [AdvanceColumnInformation("大小")]
    [AdvanceColumnMapper("size")]
    public decimal Size { get; set; } = 0;

    [AdvanceColumnInformation("后缀名")]
    [AdvanceColumnMapper("suffix")]
    [AdvanceColumnLength(300)]
    public string Suffix { get; set; } = "";

    [AdvanceColumnInformation("类型")]
    [AdvanceColumnMapper("file_type")]
    public int FileType { get; set; } = 0;

    [AdvanceColumnInformation("来源模式")]
    [AdvanceColumnMapper("source_type")]
    public int SourceType { get; set; } = 0;

    #endregion
}