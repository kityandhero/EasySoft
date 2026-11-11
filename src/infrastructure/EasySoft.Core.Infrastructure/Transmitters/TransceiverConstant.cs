using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.Infrastructure.Transmitters;

/// <summary>
/// TransceiverConstant
/// </summary>
public static class TransceiverConstant
{
    /// <summary>
    /// AccessWayTransitiveMessage
    /// </summary>
    public const string TransceiverConfigureNode = "Transceiver";

    #region AccessWayTransitiveMessage

    /// <summary>
    /// AccessWayMessageConfigureNode
    /// </summary>
    public const string AccessWayMessageConfigureNode = "AccessWayTransitiveMessage";

    /// <summary>
    /// AccessWayTransitiveMessage
    /// </summary>
    public const string AccessWayMessage = "AccessWayTransitiveMessage";

    /// <summary>
    /// AccessWayMessageServiceKey
    /// </summary>
    public const string AccessWayMessageTransmitter = "AccessWayMessageTransmitter";

    /// <summary>
    /// AccessWayMessageServiceKey
    /// </summary>
    public const string AccessWayMessageServiceKey = "AccessWayMessageTransmitter";

    /// <summary>
    /// AccessWayMessageExchange
    /// </summary>
    public const string AccessWayMessageExchange = "AccessWayMessageExchange";

    /// <summary>
    /// AccessWayMessageRoutingKey
    /// </summary>
    public const string AccessWayMessageRoutingKey = "AccessWayMessageRoutingKey";

    /// <summary>
    /// AccessWayMessageRoutingKey
    /// </summary>
    public const string AccessWayMessageQueue = "AccessWayMessageQueue";

    #endregion

    #region GeneralLogTransitiveMessage

    /// <summary>
    /// GeneralLogMessageConfigureNode
    /// </summary>
    public const string GeneralLogMessageConfigureNode = "GeneralLogTransitiveMessage";

    /// <summary>
    /// GeneralLogTransitiveMessage
    /// </summary>
    public const string GeneralLogMessage = "GeneralLogTransitiveMessage";

    /// <summary>
    /// GeneralLogMessageServiceKey
    /// </summary>
    public const string GeneralLogMessageTransmitter = "GeneralLogMessageTransmitter";

    /// <summary>
    /// GeneralLogMessageServiceKey
    /// </summary>
    public const string GeneralLogMessageServiceKey = "GeneralLogMessageTransmitter";

    /// <summary>
    /// GeneralLogMessageExchange
    /// </summary>
    public const string GeneralLogMessageExchange = "GeneralLogMessageExchange";

    /// <summary>
    /// GeneralLogMessageRoutingKey
    /// </summary>
    public const string GeneralLogMessageRoutingKey = "GeneralLogMessageRoutingKey";

    /// <summary>
    /// GeneralLogMessageRoutingKey
    /// </summary>
    public const string GeneralLogMessageQueue = "GeneralLogMessageQueue";

    #endregion

    #region ErrorLogTransitiveMessage

    /// <summary>
    /// ErrorLogMessageConfigureNode
    /// </summary>
    public const string ErrorLogMessageConfigureNode = "ErrorLogTransitiveMessage";

    /// <summary>
    /// ErrorLogTransitiveMessage
    /// </summary>
    public const string ErrorLogMessage = "ErrorLogTransitiveMessage";

    /// <summary>
    /// ErrorLogMessageServiceKey
    /// </summary>
    public const string ErrorLogMessageTransmitter = "ErrorLogMessageTransmitter";

    /// <summary>
    /// ErrorLogMessageServiceKey
    /// </summary>
    public const string ErrorLogMessageServiceKey = "ErrorLogMessageTransmitter";

    /// <summary>
    /// ErrorLogMessageExchange
    /// </summary>
    public const string ErrorLogMessageExchange = "ErrorLogMessageExchange";

    /// <summary>
    /// ErrorLogMessageRoutingKey
    /// </summary>
    public const string ErrorLogMessageRoutingKey = "ErrorLogMessageRoutingKey";

    /// <summary>
    /// ErrorLogMessageRoutingKey
    /// </summary>
    public const string ErrorLogMessageQueue = "ErrorLogMessageQueue";

    #endregion

    #region SqlLogTransitiveMessage

    /// <summary>
    /// SqlLogMessageConfigureNode
    /// </summary>
    public const string SqlLogMessageConfigureNode = "SqlLogTransitiveMessage";

    /// <summary>
    /// SqlLogTransitiveMessage
    /// </summary>
    public const string SqlLogMessage = "SqlLogTransitiveMessage";

    /// <summary>
    /// SqlLogMessageServiceKey
    /// </summary>
    public const string SqlLogMessageTransmitter = "SqlLogMessageTransmitter";

    /// <summary>
    /// SqlLogMessageServiceKey
    /// </summary>
    public const string SqlLogMessageServiceKey = "SqlLogMessageTransmitter";

    /// <summary>
    /// SqlLogMessageExchange
    /// </summary>
    public const string SqlLogMessageExchange = "SqlLogMessageExchange";

    /// <summary>
    /// SqlLogMessageRoutingKey
    /// </summary>
    public const string SqlLogMessageRoutingKey = "SqlLogMessageRoutingKey";

    /// <summary>
    /// SqlLogMessageRoutingKey
    /// </summary>
    public const string SqlLogMessageQueue = "SqlLogMessageQueue";

    #endregion

    #region ChannelCheckMessage

    /// <summary>
    /// ChannelCheckMessageConfigureNode
    /// </summary>
    public const string ChannelCheckMessageConfigureNode = "ChannelCheckMessage";

    /// <summary>
    /// ChannelCheckMessage
    /// </summary>
    public const string ChannelCheckMessage = "ChannelCheckMessage";

    /// <summary>
    /// ChannelCheckMessageServiceKey
    /// </summary>
    public const string ChannelCheckMessageTransmitter = "ChannelCheckMessageTransmitter";

    /// <summary>
    /// ChannelCheckMessageServiceKey
    /// </summary>
    public const string ChannelCheckMessageServiceKey = "ChannelCheckMessageTransmitter";

    /// <summary>
    /// ChannelCheckMessageExchange
    /// </summary>
    public const string ChannelCheckMessageExchange = "ChannelCheckMessageExchange";

    /// <summary>
    /// ChannelCheckMessageRoutingKey
    /// </summary>
    public const string ChannelCheckMessageRoutingKey = "ChannelCheckMessageRoutingKey";

    /// <summary>
    /// ChannelCheckMessageRoutingKey
    /// </summary>
    public const string ChannelCheckMessageQueue = "ChannelCheckMessageQueue";

    #endregion

    /// <summary>
    /// 获取 AccessWayTransitiveMessage RabbitMQ 连接字符串
    /// </summary>
    /// <returns></returns>
    public static string GetAccessWayMessageRabbitMQConnectionString()
    {
        return StringAssist.Join(
            [
                TransceiverConfigureNode,
                AccessWayMessageConfigureNode,
                "ConnectionString",
                "RabbitMQ"
            ],
            ":"
        );
    }

    /// <summary>
    /// 获取 GeneralLogTransitiveMessage RabbitMQ 连接字符串
    /// </summary>
    /// <returns></returns>
    public static string GetGeneralLogMessageRabbitMQConnectionString()
    {
        return StringAssist.Join(
            [
                TransceiverConfigureNode,
                GeneralLogMessageConfigureNode,
                "ConnectionString",
                "RabbitMQ"
            ],
            ":"
        );
    }

    /// <summary>
    /// 获取 ErrorLogTransitiveMessage RabbitMQ 连接字符串
    /// </summary>
    /// <returns></returns>
    public static string GetErrorLogMessageRabbitMQConnectionString()
    {
        return StringAssist.Join(
            [
                TransceiverConfigureNode,
                ErrorLogMessageConfigureNode,
                "ConnectionString",
                "RabbitMQ"
            ],
            ":"
        );
    }

    /// <summary>
    /// 获取 SqlLogTransitiveMessage RabbitMQ 连接字符串
    /// </summary>
    /// <returns></returns>
    public static string GetSqlLogMessageRabbitMQConnectionString()
    {
        return StringAssist.Join(
            [
                TransceiverConfigureNode,
                SqlLogMessageConfigureNode,
                "ConnectionString",
                "RabbitMQ"
            ],
            ":"
        );
    }

    /// <summary>
    /// 获取 ChannelCheckMessage RabbitMQ 连接字符串
    /// </summary>
    /// <returns></returns>
    public static string GetChannelCheckMessageRabbitMQConnectionString()
    {
        return StringAssist.Join(
            [
                TransceiverConfigureNode,
                ChannelCheckMessageConfigureNode,
                "ConnectionString",
                "RabbitMQ"
            ],
            ":"
        );
    }
}