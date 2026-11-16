using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Core.GeneralLogTransceiver.Assists;

/// <summary>
/// GeneralLogTransceiverConfigureAssist
/// </summary>
public class GeneralLogTransceiverConfigureAssist
{
    #region Fields

    /// <summary>
    /// GeneralLogTransceiverConfigureNode
    /// </summary>
    public const string GeneralLogTransceiverConfigureNode = "GeneralLogTransceiver";

    /// <summary>
    /// GeneralLogMessageServiceKey
    /// </summary>
    public const string GeneralLogMessageTransceiver = "GeneralLogMessageTransceiver";

    /// <summary>
    /// GeneralLogMessageServiceKey
    /// </summary>
    public const string GeneralLogMessageServiceKey = "GeneralLogMessageTransceiver";

    /// <summary>
    /// GeneralLogMessageExchange
    /// </summary>
    public const string GeneralLogMessageExchange = "GeneralLogMessageExchange";

    /// <summary>
    /// GeneralLogMessageRoutingKey
    /// </summary>
    public const string GeneralLogMessageRoutingKey = "GeneralLogMessageRoutingKey";

    /// <summary>
    /// GeneralLogMessageQueue
    /// </summary>
    public const string GeneralLogMessageQueue = "GeneralLogMessageQueue";

    /// <summary>
    /// GeneralLogTransmission
    /// </summary>
    public const string GeneralLogTransmission = "Transmission";

    /// <summary>
    /// GeneralLogPrefix
    /// </summary>
    public const string GeneralLogPrefix = "Prefix";

    #endregion

    #region Properties

    /// <summary>
    /// Configuration
    /// </summary>
    public IConfiguration Configuration { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// GeneralLogTransceiverConfigureAssist
    /// </summary>
    /// <param name="configuration"></param>
    public GeneralLogTransceiverConfigureAssist(IConfiguration configuration)
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
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetTransmission(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                GeneralLogTransceiverConfigureNode,
                GeneralLogTransmission
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
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetTransmission()
    {
        return GetTransmission(Configuration);
    }

    #endregion

    #region GetRabbitMQConnectionString

    /// <summary>
    /// 获取 GeneralLogTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetRabbitMQConnectionString(IConfiguration configuration)
    {
        var transmissionConfigValue = GetTransmission(configuration);

        var node = StringAssist.Join(
            [
                GeneralLogTransceiverConfigureNode,
                "RabbitMQ",
                "ConnectionString"
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        if (transmissionConfigValue.Value && string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                $"GeneralLog 已经配置为开启，[{node}]配置无效，请设置"
            );
        }

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// 获取 GeneralLogTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetRabbitMQConnectionString()
    {
        return GetRabbitMQConnectionString(Configuration);
    }

    #endregion

    #region GetPrefix

    /// <summary>
    /// 获取 GeneralLogTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetPrefix(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                GeneralLogTransceiverConfigureNode,
                GeneralLogPrefix
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
    /// 获取 GeneralLogTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetPrefix()
    {
        return GetPrefix(Configuration);
    }

    #endregion

    #endregion
}