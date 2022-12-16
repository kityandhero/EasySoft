namespace EasySoft.UtilityTools.Standard.Params;

/// <summary>
/// PageSearchParams
/// </summary>
public class PageSearchParams : IPageSearchParams
{
    /// <inheritdoc />
    public int PageNo { get; set; }

    /// <inheritdoc />
    public int PageSize { get; set; }
}