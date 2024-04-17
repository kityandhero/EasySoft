using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// StringAssist
/// </summary>
public static class StringAssist
{
    /// <summary>
    /// 集合中全部为空字符串
    /// </summary>
    /// <param name="list"></param>  
    /// <returns></returns>
    public static bool IsAllNullOrWhiteSpace(params string?[] list)
    {
        return !list.Any() || list.All(string.IsNullOrWhiteSpace);
    }

    /// <summary>
    /// 集合中存在任意空字符串
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsAnyNullOrWhiteSpace(params string?[] list)
    {
        return list.Any() && list.Any(string.IsNullOrWhiteSpace);
    }

    #region Merge

    /// <summary>
    /// 合并多文本为指定符号间隔组合的文本
    /// </summary>
    /// <param name="list">合并对象</param>
    /// <param name="symbol">间隔符号</param>
    /// <param name="surroundSymbolWithSpace">间隔符号前后追加空格</param>
    /// <returns></returns>
    public static string MergeWithSymbol(
        IEnumerable<string> list,
        string symbol,
        bool surroundSymbolWithSpace = true
    )
    {
        var listFilter = list.ToListFilterNullOrWhiteSpace();

        return listFilter.Join(surroundSymbolWithSpace ? $" {symbol} " : symbol);
    }

    /// <summary>
    /// 合并多文本为指定符号间隔组合的文本
    /// </summary>
    /// <param name="list">合并对象</param>
    /// <param name="emptyReplaceAction">替换空白字符串的逻辑</param>
    /// <param name="symbol">间隔符号</param>
    /// <param name="surroundSymbolWithSpace">间隔符号前后追加空格</param>
    /// <returns></returns>
    public static string MergeWithSymbol(
        IEnumerable<string> list,
        Func<string> emptyReplaceAction,
        string symbol,
        bool surroundSymbolWithSpace = true
    )
    {
        var listFilter = list
            .Select(o => string.IsNullOrWhiteSpace(o) ? emptyReplaceAction() : o)
            .ToListFilterNullOrWhiteSpace();

        return listFilter.Join(surroundSymbolWithSpace ? $" {symbol} " : symbol);
    }

    /// <summary>
    /// 合并多文本为箭头组合的文本
    /// </summary>
    /// <param name="list">合并对象</param>  
    /// <returns></returns>
    public static string MergeWithArrow(params string[] list)
    {
        var listFilter = list.ToListFilterNullOrWhiteSpace();

        return MergeWithSymbol(listFilter.ToList(), "->");
    }

    /// <summary>
    /// 合并多文本为箭头组合的文本
    /// </summary>
    /// <param name="emptyReplaceAction">空文本替换方式</param>
    /// <param name="list">合并对象</param>
    /// <returns></returns>
    public static string MergeWithArrow(Func<string> emptyReplaceAction, params string[] list)
    {
        var listFilter = list
            .Select(o => string.IsNullOrWhiteSpace(o) ? emptyReplaceAction() : o)
            .ToListFilterNullOrWhiteSpace();

        return MergeWithSymbol(listFilter.ToList(), "->");
    }

    /// <summary>
    /// MergeWithArrowEmptyReplace
    /// </summary>
    /// <returns></returns>
    public static string MergeWithArrowEmptyReplace()
    {
        return "\"\"";
    }

    #endregion
}