using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Core.ChannelCheckTransceiver.Assists;

/// <summary>
/// ChannelCheckTransceiverConfigureAssist
/// </summary>
public class ChannelCheckTransceiverConfigureAssist
{
    #region Fields

    /// <summary>
    /// ChannelCheckTransceiverConfigureNode
    /// </summary>
    public const string ChannelCheckTransceiverConfigureNode = "ChannelCheckTransceiver";

    /// <summary>
    /// ChannelCheckMessageServiceKey
    /// </summary>
    public const string ChannelCheckMessageTransceiver = "ChannelCheckMessageTransceiver";

    /// <summary>
    /// ChannelCheckMessageServiceKey
    /// </summary>
    public const string ChannelCheckMessageServiceKey = "ChannelCheckMessageTransceiver";

    /// <summary>
    /// ChannelCheckMessageExchange
    /// </summary>
    public const string ChannelCheckMessageExchange = "ChannelCheckMessageExchange";

    /// <summary>
    /// ChannelCheckMessageRoutingKey
    /// </summary>
    public const string ChannelCheckMessageRoutingKey = "ChannelCheckMessageRoutingKey";

    /// <summary>
    /// ChannelCheckMessageQueue
    /// </summary>
    public const string ChannelCheckMessageQueue = "ChannelCheckMessageQueue";

    /// <summary>
    /// ChannelCheckTransmission
    /// </summary>
    public const string ChannelCheckTransmission = "Transmission";

    /// <summary>
    /// ChannelCheckPrefix
    /// </summary>
    public const string ChannelCheckPrefix = "Prefix";

    #endregion

    #region Properties

    /// <summary>
    /// Configuration
    /// </summary>
    public IConfiguration Configuration { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// ChannelCheckTransceiverConfigureAssist
    /// </summary>
    /// <param name="configuration"></param>
    public ChannelCheckTransceiverConfigureAssist(IConfiguration configuration)
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
                ChannelCheckTransceiverConfigureNode,
                ChannelCheckTransmission
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
    /// 获取 ChannelCheckTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetRabbitMQConnectionString(IConfiguration configuration)
    {
        var transmissionConfigValue = GetTransmission(configuration);

        var node = StringAssist.Join(
            [
                ChannelCheckTransceiverConfigureNode,
                "RabbitMQ",
                "ConnectionString"
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        if (transmissionConfigValue.Value && string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                $"ChannelCheck 已经配置为开启，[{node}]配置无效，请设置"
            );
        }

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// 获取 ChannelCheckTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetRabbitMQConnectionString()
    {
        return GetRabbitMQConnectionString(Configuration);
    }

    #endregion

    #region GetPrefix

    /// <summary>
    /// 获取 ChannelCheckTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetPrefix(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                ChannelCheckTransceiverConfigureNode,
                ChannelCheckPrefix
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
    /// 获取 ChannelCheckTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetPrefix()
    {
        return GetPrefix(Configuration);
    }

    #endregion

    #endregion
}