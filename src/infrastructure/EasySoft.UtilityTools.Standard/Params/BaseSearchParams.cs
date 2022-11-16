using EasySoft.UtilityTools.Standard.Attributes;

namespace EasySoft.UtilityTools.Standard.Params;

public abstract class BaseSearchParams : ISearchParams
{
    [MinValue(1)]
    [DefaultValue(1)]
    [Description("页码")]
    public int PageNo { get; set; }

    [MaxValue(500)]
    [DefaultValue(10)]
    [Description("页条目数")]
    public int PageSize { get; set; }
}