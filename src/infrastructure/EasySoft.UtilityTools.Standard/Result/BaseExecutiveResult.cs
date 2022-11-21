using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.UtilityTools.Standard.Result;

/// <summary>
/// BaseExecutiveResult
/// </summary>
public abstract class BaseExecutiveResult
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 结果代码
    /// </summary>
    public ReturnMessage Code { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// BaseExecutiveResult
    /// </summary>
    /// <param name="returnMessage"></param>
    protected BaseExecutiveResult(ReturnMessage returnMessage)
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