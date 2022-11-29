using EasySoft.UtilityTools.Standard.Attributes;

namespace EasySoft.UtilityTools.Standard.Params;

/// <summary>
/// ISearchParams
/// </summary>
public interface IPageSearchParams : IApiParams
{
    /// <summary>
    /// 页码
    /// </summary>
    [MinValue(1)]
    [DefaultValue(1)]
    [Description("页码")]
    public int PageNo { get; set; }

    /// <summary>
    /// 页条目数
    /// </summary>
    [MaxValue(500)]
    [DefaultValue(10)]
    [Description("页条目数")]
    public int PageSize { get; set; }
}