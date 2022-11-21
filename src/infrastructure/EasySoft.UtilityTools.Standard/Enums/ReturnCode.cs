using EasySoft.UtilityTools.Standard.Attributes;

namespace EasySoft.UtilityTools.Standard.Enums;

/// <summary>
/// 返回代码
/// </summary>
public enum ReturnCode
{
    /// <summary>
    /// 空结果
    /// </summary>
    [Description("空结果")]
    [ReturnCodeSuccess(false)]
    Empty = 0,

    /// <summary>
    /// 未知
    /// </summary>
    [Description("未知")]
    [ReturnCodeSuccess(false)]
    Unknown = -1,

    /// <summary>
    /// 成功
    /// </summary>
    [Description("成功")]
    [ReturnCodeSuccess(true)]
    Ok = 200,

    /// <summary>
    /// 签名错误
    /// </summary>
    [Description("签名错误")]
    [ReturnCodeSuccess(false)]
    SignError = 120,

    /// <summary>
    /// 访问超时
    /// </summary>
    [Description("访问超时")]
    [ReturnCodeSuccess(false)]
    TimeOut = 130,

    /// <summary>
    /// 访问超时
    /// </summary>
    [Description("服务器发生未处理的异常")]
    [ReturnCodeSuccess(false)]
    Error = 500,

    /// <summary>
    /// 参数错误
    /// </summary>
    [Description("参数错误")]
    [ReturnCodeSuccess(false)]
    ParamError = 1001,

    /// <summary>
    /// 无数据
    /// </summary>
    [Description("无数据")]
    [ReturnCodeSuccess(false)]
    NoData = 1002,

    /// <summary>
    /// 数据错误
    /// </summary>
    [Description("数据错误")]
    [ReturnCodeSuccess(false)]
    DataError = 1004,

    /// <summary>
    /// 密码不匹配
    /// </summary>
    [Description("密码不匹配")]
    [ReturnCodeSuccess(false)]
    PasswordNotMatch = 1005,

    /// <summary>
    /// 无效访问
    /// </summary>
    [Description("无效访问")]
    [ReturnCodeSuccess(false)]
    InvalidAccess = 1006,

    /// <summary>
    /// Token无效
    /// </summary>
    [Description("Token无效")]
    [ReturnCodeSuccess(false)]
    AuthenticationFail = 2001,

    /// <summary>
    /// 标识过期
    /// </summary>
    [Description("标识过期")]
    [ReturnCodeSuccess(false)]
    TokenExpired = 2001,

    /// <summary>
    /// 忽略处理
    /// </summary>
    [Description("忽略处理")]
    [ReturnCodeSuccess(false)]
    IgnoreHandle = 2002,

    /// <summary>
    /// 无访问权限
    /// </summary>
    [Description("无访问权限")]
    [ReturnCodeSuccess(false)]
    NoPermission = 3001,

    /// <summary>
    /// 程序异常
    /// </summary>
    [Description("程序异常")]
    [ReturnCodeSuccess(false)]
    Exception = 5001,

    /// <summary>
    /// 方法需要重载实现
    /// </summary>
    [Description("无操作方法需要重载实现反馈")]
    [ReturnCodeSuccess(false)]
    NeedOverride = 5002,

    /// <summary>
    /// 微信OpenId无效
    /// </summary>
    [Description("微信OpenId无效")]
    [ReturnCodeSuccess(false)]
    WeChatOpenIdInvalid = 6000,

    /// <summary>
    /// 微信UnionId无效
    /// </summary>
    [Description("微信UnionId无效")]
    [ReturnCodeSuccess(false)]
    WeChatUnionIdInvalid = 6001,

    /// <summary>
    /// 无操作反馈
    /// </summary>
    [Description("无操作反馈")]
    [ReturnCodeSuccess(false)]
    NoChange = 10001
}