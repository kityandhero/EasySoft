namespace EasySoft.UtilityTools.Standard.Result.Interfaces;

/// <summary>
/// list result
/// </summary>
public interface IListResult<T>
{
    /// <summary>
    /// 页数据
    /// </summary>
    public List<T> List { get; set; }

    /// <summary>
    /// Count
    /// </summary>
    public int Count();
}