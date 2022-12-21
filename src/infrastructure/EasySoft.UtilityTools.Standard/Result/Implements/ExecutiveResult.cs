using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result.Interfaces;

namespace EasySoft.UtilityTools.Standard.Result.Implements;

/// <inheritdoc />
public class ExecutiveResult : ExecutiveResult<object>
{
    /// <inheritdoc />
    public ExecutiveResult(ReturnMessage returnMessage) : base(returnMessage)
    {
    }

    /// <inheritdoc />
    public ExecutiveResult(ReturnCode returnCode) : this(new ReturnMessage(returnCode))
    {
    }
}

/// <summary>
/// 执行结果，用于返回方法等的执行结果判断
/// </summary>
public class ExecutiveResult<T> : BaseExecutiveResult, IExecutiveResult<T>
{
    /// <inheritdoc />
    public T? Data { get; set; }

    /// <summary>
    /// ExecutiveResult
    /// </summary>
    /// <param name="returnMessage"></param>
    public ExecutiveResult(ReturnMessage returnMessage) : base(returnMessage)
    {
        Data = default;
    }

    /// <summary>
    /// ExecutiveResult
    /// </summary>
    /// <param name="returnCode"></param>
    public ExecutiveResult(ReturnCode returnCode) : this(new ReturnMessage(returnCode))
    {
    }

    /// <summary>
    /// SetData
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public ExecutiveResult<T> SetData(T data)
    {
        Data = data;

        return this;
    }

    /// <summary>
    /// ToExecutiveResult
    /// </summary>
    /// <returns></returns>
    public ExecutiveResult ToExecutiveResult()
    {
        return new ExecutiveResult(Code);
    }
}

/// <summary>
/// ExistResult
/// </summary>
/// <typeparam name="T"></typeparam>
public class ExistResult<T> : ExecutiveResult<T>
{
    /// <summary>
    /// Exist
    /// </summary>
    public bool Exist { get; set; }

    /// <summary>
    /// ExistResult
    /// </summary>
    /// <param name="returnMessage"></param>
    public ExistResult(ReturnMessage returnMessage) : base(returnMessage)
    {
        Data = default;
        Exist = false;
    }

    /// <summary>
    /// ExistResult
    /// </summary>
    /// <param name="returnCode"></param>
    public ExistResult(ReturnCode returnCode) : this(new ReturnMessage(returnCode))
    {
    }

    /// <summary>
    /// SetExist
    /// </summary>
    /// <param name="exist"></param>
    /// <returns></returns>
    public ExecutiveResult<T> SetExist(bool exist)
    {
        Exist = exist;

        return this;
    }
}