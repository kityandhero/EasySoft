using EasySoft.UtilityTools.Standard.Attributes;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Result.Interfaces;

namespace EasySoft.UtilityTools.Standard.Result.Implements;

/// <summary>
/// ReturnMessage
/// </summary>
public class ReturnMessage : IReturnMessage
{
    /// <inheritdoc />
    public bool Success { get; set; }

    /// <inheritdoc />
    public int Code { get; set; }

    /// <inheritdoc />
    public string Message { get; set; }

    /// <inheritdoc />
    public object Extra { get; set; }

    /// <summary>
    /// ReturnMessage
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="success"></param>
    public ReturnMessage(int code, string message, bool success)
    {
        Code = code;
        Message = message;
        Success = success;
        Extra = new { };
    }

    /// <summary>
    /// ReturnMessage
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="success"></param>
    /// <param name="extra"></param>
    public ReturnMessage(int code, string message, bool success, object extra)
    {
        Code = code;
        Message = message;
        Success = success;
        Extra = extra;
    }

    /// <summary>
    /// ReturnMessage
    /// </summary>
    /// <param name="code"></param>
    public ReturnMessage(ReturnCode code)
    {
        Code = (int)code;
        Message = code.GetCustomAttribute<DescriptionAttribute>()?.Description ?? "";
        Success = code.GetCustomAttribute<ReturnCodeSuccessAttribute>()?.Success ?? false;
        Extra = new { };
    }

    /// <summary>
    /// ReturnMessage
    /// </summary>
    /// <param name="code"></param>
    /// <param name="extra"></param>
    public ReturnMessage(ReturnCode code, object extra)
    {
        Code = (int)code;
        Message = code.GetCustomAttribute<DescriptionAttribute>()?.Description ?? "";
        Success = code.GetCustomAttribute<ReturnCodeSuccessAttribute>()?.Success ?? false;
        Extra = extra;
    }

    /// <summary>
    /// AppendMessage
    /// </summary>
    /// <param name="messages"></param>
    /// <returns></returns>
    public IReturnMessage AppendMessage(params string[] messages)
    {
        var list = new List<string>
        {
            Message
        };

        list.AddRange(messages);

        return ToMessage(list.Join(","));
    }

    /// <inheritdoc />
    public IReturnMessage ToMessage()
    {
        return new ReturnMessage(Code, Message, Success);
    }

    /// <inheritdoc />
    public IReturnMessage ToMessage(bool success)
    {
        return new ReturnMessage(Code, Message, success);
    }

    /// <inheritdoc />
    public IReturnMessage ToMessage(string message)
    {
        return new ReturnMessage(Code, message, Success);
    }

    /// <inheritdoc />
    public IReturnMessage ToMessage(int code)
    {
        return new ReturnMessage(code, Message, Success);
    }
}