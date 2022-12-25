using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result.Interfaces;

namespace EasySoft.UtilityTools.Standard.Result.Factories;

/// <summary>
/// return message factory
/// </summary>
public static class ReturnMessageFactory
{
    /// <summary>
    /// 空结果
    /// </summary>
    public static IReturnMessage Empty => new ReturnMessage(ReturnCode.Empty);

    /// <summary>
    /// 未知
    /// </summary>
    public static IReturnMessage Unknown => new ReturnMessage(ReturnCode.Unknown);

    /// <summary>
    /// Ok
    /// </summary>
    public static IReturnMessage Ok => new ReturnMessage(ReturnCode.Ok);

    /// <summary>
    /// 签名错误
    /// </summary>
    public static IReturnMessage SignError => new ReturnMessage(ReturnCode.SignError);

    /// <summary>
    /// 访问超时
    /// </summary>
    public static IReturnMessage TimeOut => new ReturnMessage(ReturnCode.TimeOut);

    /// <summary>
    /// 参数错误
    /// </summary>
    public static IReturnMessage ParamError => new ReturnMessage(ReturnCode.ParamError);

    /// <summary>
    /// 无数据
    /// </summary>
    public static IReturnMessage NoData => new ReturnMessage(ReturnCode.NoData);

    /// <summary>
    /// 无操作反馈
    /// </summary>
    public static IReturnMessage NoChange => new ReturnMessage(ReturnCode.NoChange);

    /// <summary>
    /// 数据错误
    /// </summary>
    public static IReturnMessage DataError => new ReturnMessage(ReturnCode.DataError);

    /// <summary>
    /// Token无效
    /// </summary>
    public static IReturnMessage AuthenticationFail => new ReturnMessage(ReturnCode.AuthenticationFail);

    /// <summary>
    /// 忽略处理
    /// </summary>
    public static IReturnMessage IgnoreHandle => new ReturnMessage(ReturnCode.IgnoreHandle);

    /// <summary>
    /// 密码不匹配
    /// </summary>
    public static IReturnMessage PasswordNotMatch => new ReturnMessage(ReturnCode.PasswordNotMatch);

    /// <summary>
    /// 程序异常
    /// </summary>
    public static IReturnMessage Exception => new ReturnMessage(ReturnCode.Exception);

    /// <summary>
    /// 方法需要重载实现
    /// </summary>
    public static IReturnMessage NeedOverride => new ReturnMessage(ReturnCode.NeedOverride);
}