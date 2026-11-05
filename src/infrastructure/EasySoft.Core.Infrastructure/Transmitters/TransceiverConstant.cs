using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.Infrastructure.Transmitters;

/// <summary>
/// TransceiverConstant
/// </summary>
public static class TransceiverConstant
{
    /// <summary>
    /// AccessWayMessage
    /// </summary>
    public const string TransceiverConfigureNode = "Transceiver";

    /// <summary>
    /// AccessWayMessage
    /// </summary>
    public const string AccessWayMessage = "AccessWayMessage";

    /// <summary>
    /// IErrorLogMessage
    /// </summary>
    public const string ErrorLogMessage = "ErrorLogMessage";

    #region GeneralLogMessage

    /// <summary>
    /// GeneralLogMessageConfigureNode
    /// </summary>
    public const string GeneralLogMessageConfigureNode = "GeneralLogMessage";

    /// <summary>
    /// GeneralLogMessage
    /// </summary>
    public const string GeneralLogMessage = "GeneralLogMessage";

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

    /// <summary>
    /// SqlLogExchange
    /// </summary>
    public const string SqlLogMessage = "SqlLogExchange";

    /// <summary>
    /// ChannelCheckMessage
    /// </summary>
    public const string ChannelCheckMessage = "ChannelCheckMessage";

    /// <summary>
    /// 获取 GeneralLogMessage RabbitMQ 连接字符串
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
}