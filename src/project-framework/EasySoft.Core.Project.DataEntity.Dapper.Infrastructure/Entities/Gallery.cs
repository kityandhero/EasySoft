namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("gallery")]
public class Gallery : AbstractFunctionEntity<Gallery>
{
    #region Properties

    [AdvanceColumnInformation("链接")]
    [AdvanceColumnMapper("url")]
    [AdvanceColumnLength(400)]
    public string Url { get; set; } = "";

    [AdvanceColumnInformation("标题")]
    [AdvanceColumnMapper("title")]
    [AdvanceColumnLength(100)]
    [AdvanceColumnNational]
    public string Title { get; set; } = "";

    [AdvanceColumnInformation("图片链接")]
    [AdvanceColumnMapper("image_url")]
    [AdvanceColumnLength(400)]
    public string ImageUrl { get; set; } = "";

    [AdvanceColumnInformation("类别标识")]
    [AdvanceColumnMapper("category_id")]
    public long CategoryId { get; set; } = 0;

    [AdvanceColumnInformation("排序值")]
    [AdvanceColumnMapper("sort")]
    public int Sort { get; set; } = 0;

    [AdvanceColumnInformation("类型")]
    [AdvanceColumnMapper("type")]
    public int Type { get; set; } = 0;

    [AdvanceColumnInformation("适用业务")]
    [AdvanceColumnMapper("business_mode")]
    public int BusinessMode { get; set; } = 0;

    #endregion Properties
}