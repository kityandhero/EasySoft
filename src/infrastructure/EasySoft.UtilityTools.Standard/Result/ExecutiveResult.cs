using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.UtilityTools.Standard.Result;

public class ExecutiveResult : ExecutiveResult<object>
{
    public ExecutiveResult(ReturnMessage returnMessage) : base(returnMessage)
    {
    }

    public ExecutiveResult(ReturnCode returnCode) : this(new ReturnMessage(returnCode))
    {
    }
}

/// <summary>
/// 执行结果，用于返回方法等的执行结果判断
/// </summary>
public class ExecutiveResult<T> : BaseExecutiveResult
{
    /// <summary>
    /// 数据  
    /// </summary>
    public T? Data { get; set; }

    public ExecutiveResult(ReturnMessage returnMessage) : base(returnMessage)
    {
        Data = default;
    }

    public ExecutiveResult(ReturnCode returnCode) : this(new ReturnMessage(returnCode))
    {
    }

    public ExecutiveResult<T> SetData(T data)
    {
        Data = data;

        return this;
    }

    public ExecutiveResult ToExecutiveResult()
    {
        return new ExecutiveResult(Code);
    }
}

public class ExistResult<T> : ExecutiveResult<T>
{
    public bool Exist { get; set; }

    public ExistResult(ReturnMessage returnMessage) : base(returnMessage)
    {
        Data = default;
        Exist = false;
    }

    public ExistResult(ReturnCode returnCode) : this(new ReturnMessage(returnCode))
    {
    }

    public ExecutiveResult<T> SetExist(bool exist)
    {
        Exist = exist;

        return this;
    }
}