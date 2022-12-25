namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// PageListResultExtensions
/// </summary>
public static class PageListResultExtensions
{
    /// <summary>
    /// ToPageListResult
    /// </summary>
    /// <param name="pageListResult"></param>
    /// <param name="transfer"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static PageListResult<ExpandoObject> ToPageListResult<TSource>(
        this PageListResult<TSource> pageListResult,
        Func<TSource, ExpandoObject> transfer
    )
    {
        var result = new PageListResult<ExpandoObject>(pageListResult.Code)
        {
            List = pageListResult.List.Select(transfer).ToList(),
            PageIndex = pageListResult.PageIndex,
            PageSize = pageListResult.PageSize,
            TotalSize = pageListResult.TotalSize
        };

        return result;
    }

    /// <summary>
    /// ToPageListResult
    /// </summary>
    /// <param name="pageListResult"></param>
    /// <param name="transfer"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    /// <returns></returns>
    public static PageListResult<TTarget> ToPageListResult<TSource, TTarget>(
        this PageListResult<TSource> pageListResult,
        Func<TSource, TTarget> transfer
    )
    {
        var result = new PageListResult<TTarget>(pageListResult.Code)
        {
            List = pageListResult.List.Select(transfer).ToList(),
            PageIndex = pageListResult.PageIndex,
            PageSize = pageListResult.PageSize,
            TotalSize = pageListResult.TotalSize
        };

        return result;
    }
}