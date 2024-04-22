namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableInformation("栏目表")]
[AdvanceTableMapper("section")]
public class Section : AbstractFunctionEntity<Section>
{
    #region Properties

    [AdvanceColumnInformation("名称")]
    [AdvanceColumnMapper("name")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string Name { get; set; } = "";

    [AdvanceColumnInformation("正方形图片")]
    [AdvanceColumnMapper("image")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Image { get; set; } = "";

    [AdvanceColumnInformation("长方形图片")]
    [AdvanceColumnMapper("rectangle_image")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string RectangleImage { get; set; } = "";

    [AdvanceColumnInformation("上级标识")]
    [AdvanceColumnMapper("parent_id")]
    public long ParentId { get; set; } = 0;

    [AdvanceColumnInformation("关键词")]
    [AdvanceColumnMapper("keyword")]
    public string Keyword { get; set; } = "";

    [AdvanceColumnInformation("简介描述")]
    [AdvanceColumnMapper("description")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Description { get; set; } = "";

    [AdvanceColumnInformation("视频")]
    [AdvanceColumnMapper("video")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Video { get; set; } = "";

    [AdvanceColumnInformation("音频")]
    [AdvanceColumnMapper("audio")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Audio { get; set; } = "";

    [AdvanceColumnInformation("附件")]
    [AdvanceColumnMapper("attachment")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Attachment { get; set; } = "";

    [AdvanceColumnInformation("内容数据")]
    [AdvanceColumnMapper("content_data")]
    [AdvanceColumnNational]
    public string ContentData { get; set; } = "";

    [AdvanceColumnInformation("媒体数据")]
    [AdvanceColumnMapper("media_data")]
    [AdvanceColumnNational]
    public string MediaData { get; set; } = "";

    [AdvanceColumnInformation("渲染模式")]
    [AdvanceColumnMapper("render_type")]
    public int RenderType { get; set; } = 0;

    [AdvanceColumnInformation("排序值")]
    [AdvanceColumnMapper("sort")]
    public int Sort { get; set; } = 0;

    [AdvanceColumnInformation("适用业务")]
    [AdvanceColumnMapper("business_mode")]
    public int BusinessMode { get; set; } = 0;

    [AdvanceColumnInformation("推荐状态")]
    [AdvanceColumnMapper("whether_recommend")]
    public int WhetherRecommend { get; set; } = 0;

    [AdvanceColumnInformation("置顶状态")]
    [AdvanceColumnMapper("whether_top")]
    public int WhetherTop { get; set; } = 0;

    [AdvanceColumnInformation("可见状态")]
    [AdvanceColumnMapper("whether_visible")]
    public int WhetherVisible { get; set; } = 0;

    #endregion Properties
}