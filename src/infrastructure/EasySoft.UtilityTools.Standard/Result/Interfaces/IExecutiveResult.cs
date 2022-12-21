using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.UtilityTools.Standard.Result.Interfaces;

/// <summary>
/// executive result
/// </summary>
public interface IExecutiveResult<T> : IExecutiveResult
{
    /// <summary>
    /// 数据  
    /// </summary>
    public T? Data { get; set; }
}

/// <summary>
/// executive result
/// </summary>
public interface IExecutiveResult
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 结果代码
    /// </summary>
    public IReturnMessage Code { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; }
}