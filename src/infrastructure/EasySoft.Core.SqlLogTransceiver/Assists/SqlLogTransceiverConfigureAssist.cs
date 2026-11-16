using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Core.SqlLogTransceiver.Assists;

/// <summary>
/// SqlLogTransceiverConfigureAssist
/// </summary>
public class SqlLogTransceiverConfigureAssist
{
    #region Fields

    /// <summary>
    /// SqlLogTransceiverConfigureNode
    /// </summary>
    public const string SqlLogTransceiverConfigureNode = "SqlLogTransceiver";

    /// <summary>
    /// SqlLogMessageServiceKey
    /// </summary>
    public const string SqlLogMessageTransceiver = "SqlLogMessageTransceiver";

    /// <summary>
    /// SqlLogMessageServiceKey
    /// </summary>
    public const string SqlLogMessageServiceKey = "SqlLogMessageTransceiver";

    /// <summary>
    /// SqlLogMessageExchange
    /// </summary>
    public const string SqlLogMessageExchange = "SqlLogMessageExchange";

    /// <summary>
    /// SqlLogMessageRoutingKey
    /// </summary>
    public const string SqlLogMessageRoutingKey = "SqlLogMessageRoutingKey";

    /// <summary>
    /// SqlLogMessageQueue
    /// </summary>
    public const string SqlLogMessageQueue = "SqlLogMessageQueue";

    /// <summary>
    /// SqlLogTransmission
    /// </summary>
    public const string SqlLogTransmission = "Transmission";

    /// <summary>
    /// SqlLogPrefix
    /// </summary>
    public const string SqlLogPrefix = "Prefix";

    #endregion

    #region Properties

    /// <summary>
    /// Configuration
    /// </summary>
    public IConfiguration Configuration { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// SqlLogTransceiverConfigureAssist
    /// </summary>
    /// <param name="configuration"></param>
    public SqlLogTransceiverConfigureAssist(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #endregion

    #region Methods

    #region GetTransmission

    /// <summary>
    /// 是否开启传输
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>|
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetTransmission(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                SqlLogTransceiverConfigureNode,
                SqlLogTransmission
            ],
            ":"
        );

        var v = configuration[node] ?? "false";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// 是否开启传输
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>|
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetTransmission()
    {
        return GetTransmission(Configuration);
    }

    #endregion

    #region GetRabbitMQConnectionString

    /// <summary>
    /// 获取 SqlLogTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetRabbitMQConnectionString(IConfiguration configuration)
    {
        var transmissionConfigValue = GetTransmission(configuration);

        var node = StringAssist.Join(
            [
                SqlLogTransceiverConfigureNode,
                "RabbitMQ",
                "ConnectionString"
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        if (transmissionConfigValue.Value && string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                $"SqlLog 已经配置为开启，[{node}]配置无效，请设置"
            );
        }

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// 获取 SqlLogTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetRabbitMQConnectionString()
    {
        return GetRabbitMQConnectionString(Configuration);
    }

    #endregion

    #region GetPrefix

    /// <summary>
    /// 获取 SqlLogTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetPrefix(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                SqlLogTransceiverConfigureNode,
                SqlLogPrefix
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
    /// 获取 SqlLogTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetPrefix()
    {
        return GetPrefix(Configuration);
    }

    #endregion

    #endregion
}