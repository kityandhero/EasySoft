using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result.Interfaces;

namespace EasySoft.UtilityTools.Standard.Result.Implements;

/// <summary>
/// PageListResult
/// </summary>
/// <typeparam name="T"></typeparam>
public class PageListResult<T> : ListResult<T>, IPageListResult<T>
{
    /// <inheritdoc />
    public int PageIndex { get; set; }

    /// <inheritdoc />
    public int PageSize { get; set; }

    /// <inheritdoc />
    public long TotalSize { get; set; }

    /// <inheritdoc />
    public bool HasData { get; }

    /// <summary>
    /// PageListResult
    /// </summary>
    /// <param name="returnCode"></param>
    public PageListResult(ReturnCode returnCode) : this(new ReturnMessage(returnCode))
    {
    }

    /// <summary>
    /// PageListResult
    /// </summary>
    /// <param name="returnMessage"></param>
    /// <param name="list"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalSize"></param>
    public PageListResult(
        IReturnMessage returnMessage,
        List<T> list,
        int pageIndex,
        int pageSize,
        long totalSize
    ) : base(returnMessage, list)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalSize = totalSize;
        HasData = List.Count > 0;
    }

    /// <summary>
    /// PageListResult
    /// </summary>
    /// <param name="returnMessage"></param>
    /// <param name="list"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalSize"></param>
    public PageListResult(
        IReturnMessage returnMessage,
        List<T> list,
        int pageIndex,
        int pageSize,
        int totalSize
    ) : base(returnMessage, list)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalSize = totalSize;
        HasData = List.Count > 0;
    }

    /// <summary>
    /// PageListResult
    /// </summary>
    /// <param name="returnMessage"></param>
    public PageListResult(IReturnMessage returnMessage) : this(
        returnMessage,
        new List<T>(),
        1,
        10,
        0
    )
    {
    }

    /// <summary>
    /// PageListResult
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="list"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalSize"></param>
    public PageListResult(
        ReturnCode returnCode,
        List<T> list,
        int pageIndex,
        int pageSize,
        long totalSize
    ) : this(new ReturnMessage(returnCode), list, pageIndex, pageSize, totalSize)
    {
    }

    /// <summary>
    /// PageListResult
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="list"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalSize"></param>
    public PageListResult(
        ReturnCode returnCode,
        List<T> list,
        int pageIndex,
        int pageSize,
        int totalSize
    ) : this(new ReturnMessage(returnCode), list, pageIndex, pageSize, totalSize)
    {
    }
}