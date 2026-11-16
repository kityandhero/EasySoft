using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Core.ErrorLogTransceiver.Assists;

/// <summary>
/// ErrorLogTransceiverConfigureAssist
/// </summary>
public class ErrorLogTransceiverConfigureAssist
{
    #region Fields

    /// <summary>
    /// ErrorLogTransceiverConfigureNode
    /// </summary>
    public const string ErrorLogTransceiverConfigureNode = "ErrorLogTransceiver";

    /// <summary>
    /// ErrorLogMessageServiceKey
    /// </summary>
    public const string ErrorLogMessageTransceiver = "ErrorLogMessageTransceiver";

    /// <summary>
    /// ErrorLogMessageServiceKey
    /// </summary>
    public const string ErrorLogMessageServiceKey = "ErrorLogMessageTransceiver";

    /// <summary>
    /// ErrorLogMessageExchange
    /// </summary>
    public const string ErrorLogMessageExchange = "ErrorLogMessageExchange";

    /// <summary>
    /// ErrorLogMessageRoutingKey
    /// </summary>
    public const string ErrorLogMessageRoutingKey = "ErrorLogMessageRoutingKey";

    /// <summary>
    /// ErrorLogMessageQueue
    /// </summary>
    public const string ErrorLogMessageQueue = "ErrorLogMessageQueue";

    /// <summary>
    /// ErrorLogPrefix
    /// </summary>
    public const string ErrorLogPrefix = "Prefix";

    #endregion

    #region Properties

    /// <summary>
    /// Configuration
    /// </summary>
    public IConfiguration Configuration { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// ErrorLogTransceiverConfigureAssist
    /// </summary>
    /// <param name="configuration"></param>
    public ErrorLogTransceiverConfigureAssist(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #endregion

    #region Methods

    #region GetRabbitMQConnectionString

    /// <summary>
    /// 获取 ErrorLogTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetRabbitMQConnectionString(IConfiguration configuration)
    {
        var transmissionErrorConfigValue = InfrastructureConfigureAssist.GetTransmissionError(
            configuration
        );

        var node = StringAssist.Join(
            [
                ErrorLogTransceiverConfigureNode,
                "RabbitMQ",
                "ConnectionString"
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        if (transmissionErrorConfigValue.Value && string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                $"ErrorLog 已经配置为开启，[{node}]配置无效，请设置"
            );
        }

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// 获取 ErrorLogTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetRabbitMQConnectionString()
    {
        return GetRabbitMQConnectionString(Configuration);
    }

    #endregion

    #region GetPrefix

    /// <summary>
    /// 获取 ErrorLogTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetPrefix(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                ErrorLogTransceiverConfigureNode,
                ErrorLogPrefix
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// 获取 ErrorLogTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetPrefix()
    {
        return GetPrefix(Configuration);
    }

    #endregion

    #endregion
}