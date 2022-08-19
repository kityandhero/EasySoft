using EasySoft.Core.ErrorLogTransmitter.Enums;
using EasySoft.Core.ErrorLogTransmitter.ExtensionMethods;
using EasySoft.Core.ErrorLogTransmitter.Interfaces;
using EasySoft.Core.ExchangeRegulation.Entities;
using EasySoft.Core.ExchangeRegulation.Enums;
using EasySoft.Core.ExchangeRegulation.ExtensionMethods;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.Core.ErrorLogTransmitter.Entities;

public class ErrorLogExchange : BaseExchange, IErrorLogExchange
{
    /// <summary>
    /// 创建人
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Url入口
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 消息描述
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 堆栈跟踪
    /// </summary>
    public string StackTrace { get; set; }

    /// <summary>
    /// 源代码信息
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// 场景描述
    /// </summary>
    public string Scene { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 重要程度
    /// </summary>
    public int Degree { get; set; }

    /// <summary>
    /// Url头信息
    /// </summary>
    public string Header { get; set; }

    /// <summary>
    /// Url参数
    /// </summary>
    public string UrlParams { get; set; }

    /// <summary>
    /// Payload参数
    /// </summary>
    public string PayloadParams { get; set; }

    /// <summary>
    /// Url表单参数
    /// </summary>
    public string FormParams { get; set; }

    /// <summary>
    /// 域名
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// 端口
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// 自定义日志
    /// </summary>
    public string CustomLog { get; set; }

    /// <summary>
    /// 自定义数据
    /// </summary>
    public object CustomData { get; set; }

    /// <summary>
    /// 自定义数据类型
    /// </summary>
    public int CustomDataType { get; set; }

    /// <summary>
    /// 异常类型的名称
    /// </summary>
    public string ExceptionTypeName { get; set; }

    /// <summary>
    /// 异常类型的全名
    /// </summary>
    public string? ExceptionTypeFullName { get; set; }

    public ErrorLogExchange()
    {
        UserId = 0;
        Url = "";
        Message = "";
        StackTrace = "";
        Source = "";
        Scene = "";
        Type = ErrorLogExchangeType.DataError.ToInt();
        Degree = ErrorLogExchangeDegree.Info.ToInt();
        Header = "";
        FormParams = "";
        UrlParams = "";
        Host = "";
        Port = 0;
        PayloadParams = "";
        CustomLog = "";
        CustomData = "";
        CustomDataType = CustomValueType.PlainValue.ToInt();
        ExceptionTypeName = "";
        ExceptionTypeFullName = "";
    }
}