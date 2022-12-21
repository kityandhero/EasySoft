using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result.Interfaces;

namespace EasySoft.UtilityTools.Standard.Result.Implements;

/// <summary>
/// ListResult
/// </summary>
/// <typeparam name="T"></typeparam>
public class ListResult<T> : BaseExecutiveResult

{
    /// <summary>
    /// 页数据
    /// </summary>
    public List<T> List { get; set; }

    /// <summary>
    /// Count
    /// </summary>
    public int Count => List.Count;

    /// <summary>
    /// ListResult
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="list"></param>
    public ListResult(ReturnCode returnCode, List<T> list) : this(new ReturnMessage(returnCode), list)
    {
    }

    /// <summary>
    /// ListResult
    /// </summary>
    /// <param name="returnMessage"></param>
    /// <param name="list"></param>
    public ListResult(IReturnMessage returnMessage, List<T> list) : base(returnMessage)
    {
        List = list;
    }

    /// <summary>
    /// ListResult
    /// </summary>
    /// <param name="returnMessage"></param>
    public ListResult(ReturnMessage returnMessage) : base(returnMessage)
    {
        List = new List<T>();
    }

    /// <summary>
    /// ListResult
    /// </summary>
    /// <param name="returnCode"></param>
    public ListResult(ReturnCode returnCode) : this(new ReturnMessage(returnCode))
    {
    }
}