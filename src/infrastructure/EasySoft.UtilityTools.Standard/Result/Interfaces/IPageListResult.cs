namespace EasySoft.UtilityTools.Standard.Result.Interfaces;

/// <summary>
/// page list result
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPageListResult<T> : IListResult<T>
{
    /// <summary>
    /// 页码
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// 页条目数
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 总数据量
    /// </summary>
    public long TotalSize { get; set; }

    /// <summary>
    /// 是否有数据
    /// </summary>
    public bool HasData { get; }
}