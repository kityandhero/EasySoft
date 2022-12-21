using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result.Interfaces;

namespace EasySoft.UtilityTools.Standard.Result.Implements;

/// <summary>
/// BaseExecutiveResult
/// </summary>
public abstract class BaseExecutiveResult : IExecutiveResult
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

    /// <summary>
    /// BaseExecutiveResult
    /// </summary>
    /// <param name="returnMessage"></param>
    protected BaseExecutiveResult(IReturnMessage returnMessage)
    {
        Success = returnMessage.Success;
        Code = returnMessage;
        Message = returnMessage.Message;
    }

    /// <summary>
    /// BaseExecutiveResult
    /// </summary>
    /// <param name="returnCode"></param>
    protected BaseExecutiveResult(ReturnCode returnCode) : this(new ReturnMessage(returnCode))
    {
    }

    /// <summary>
    /// SetMessage
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public BaseExecutiveResult SetMessage(string message)
    {
        Message = message;

        return this;
    }
}