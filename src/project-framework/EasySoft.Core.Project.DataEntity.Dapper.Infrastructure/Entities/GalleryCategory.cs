namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("gallery_category")]
public class GalleryCategory : AbstractFunctionEntity<GalleryCategory>
{
    #region Properties

    [AdvanceColumnInformation("类别名称")]
    [AdvanceColumnMapper("name")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string Name { get; set; } = "";

    [AdvanceColumnInformation("排序值")]
    [AdvanceColumnMapper("sort")]
    public int Sort { get; set; } = 0;

    [AdvanceColumnInformation("备注")]
    [AdvanceColumnMapper("note")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Note { get; set; } = "";

    [AdvanceColumnInformation("是否开放")]
    [AdvanceColumnMapper("whether_open")]
    public int WhetherOpen { get; set; } = 0;

    #endregion Properties
}