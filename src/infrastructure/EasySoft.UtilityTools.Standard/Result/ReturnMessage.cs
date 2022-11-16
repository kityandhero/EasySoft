using System.Collections.Generic;
using System.ComponentModel;
using EasySoft.UtilityTools.Standard.Attributes;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.UtilityTools.Standard.Result;

public class ReturnMessage
{
    public bool Success { get; set; }

    public int Code { get; set; }

    public string Message { get; set; }

    public object Extra { get; set; }

    public ReturnMessage(int code, string message, bool success)
    {
        Code = code;
        Message = message;
        Success = success;
        Extra = new { };
    }

    public ReturnMessage(int code, string message, bool success, object extra)
    {
        Code = code;
        Message = message;
        Success = success;
        Extra = extra;
    }

    public ReturnMessage(ReturnCode code)
    {
        Code = (int)code;
        Message = code.GetCustomAttribute<DescriptionAttribute>()?.Description ?? "";
        Success = code.GetCustomAttribute<ReturnCodeSuccessAttribute>()?.Success ?? false;
        Extra = new { };
    }

    public ReturnMessage(ReturnCode code, object extra)
    {
        Code = (int)code;
        Message = code.GetCustomAttribute<DescriptionAttribute>()?.Description ?? "";
        Success = code.GetCustomAttribute<ReturnCodeSuccessAttribute>()?.Success ?? false;
        Extra = extra;
    }

    public ReturnMessage AppendMessage(params string[] messages)
    {
        var list = new List<string>
        {
            Message
        };

        list.AddRange(messages);

        return ToMessage(list.Join(","));
    }

    public ReturnMessage ToMessage()
    {
        return new ReturnMessage(Code, Message, Success);
    }

    public ReturnMessage ToMessage(bool success)
    {
        return new ReturnMessage(Code, Message, success);
    }

    public ReturnMessage ToMessage(string message)
    {
        return new ReturnMessage(Code, message, Success);
    }

    public ReturnMessage ToMessage(int code)
    {
        return new ReturnMessage(code, Message, Success);
    }

    /// <summary>
    /// 空结果
    /// </summary>
    public static ReturnMessage Empty => new(ReturnCode.Empty);

    /// <summary>
    /// 未知
    /// </summary>
    public static ReturnMessage Unknown => new(ReturnCode.Unknown);

    /// <summary>
    /// Ok
    /// </summary>
    public static ReturnMessage Ok => new(ReturnCode.Ok);

    /// <summary>
    /// 签名错误
    /// </summary>
    public static ReturnMessage SignError => new(ReturnCode.SignError);

    /// <summary>
    /// 访问超时
    /// </summary>
    public static ReturnMessage TimeOut => new(ReturnCode.TimeOut);

    /// <summary>
    /// 参数错误
    /// </summary>
    public static ReturnMessage ParamError => new(ReturnCode.ParamError);

    /// <summary>
    /// 无数据
    /// </summary>
    public static ReturnMessage NoData => new(ReturnCode.NoData);

    /// <summary>
    /// 无操作反馈
    /// </summary>
    public static ReturnMessage NoChange => new(ReturnCode.NoChange);

    /// <summary>
    /// 数据错误
    /// </summary>
    public static ReturnMessage DataError => new(ReturnCode.DataError);

    /// <summary>
    /// Token无效
    /// </summary>
    public static ReturnMessage AuthenticationFail => new(ReturnCode.AuthenticationFail);

    /// <summary>
    /// 忽略处理
    /// </summary>
    public static ReturnMessage IgnoreHandle => new(ReturnCode.IgnoreHandle);

    /// <summary>
    /// 密码不匹配
    /// </summary>
    public static ReturnMessage PasswordNotMatch => new(ReturnCode.PasswordNotMatch);

    /// <summary>
    /// 程序异常
    /// </summary>
    public static ReturnMessage Exception => new(ReturnCode.Exception);

    /// <summary>
    /// 方法需要重载实现
    /// </summary>
    public static ReturnMessage NeedOverride => new(ReturnCode.NeedOverride);
}