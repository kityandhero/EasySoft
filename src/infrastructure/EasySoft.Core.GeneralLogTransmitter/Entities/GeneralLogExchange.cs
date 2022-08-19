using EasySoft.Core.ExchangeRegulation.Entities;
using EasySoft.Core.ExchangeRegulation.ExtensionMethods;
using EasySoft.Core.GeneralLogTransmitter.Enums;
using EasySoft.Core.GeneralLogTransmitter.ExtensionMethods;
using EasySoft.Core.GeneralLogTransmitter.Interfaces;
using ValueType = EasySoft.Core.ExchangeRegulation.Enums.ValueType;

namespace EasySoft.Core.GeneralLogTransmitter.Entities;

public class GeneralLogExchange : BaseExchange, IGeneralLogExchange
{
    /// <summary>
    /// 消息描述
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 消息描述数据类型  
    /// </summary>
    public int MessageType { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 消息内容数据类型
    /// </summary>
    public int ContentType { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    public int Type { get; set; }

    public GeneralLogExchange()
    {
        Message = "";
        MessageType = ValueType.PlainValue.ToInt();
        Content = "";
        ContentType = ValueType.PlainValue.ToInt();
        Type = GeneralLogExchangeType.Unknown.ToInt();
    }
}