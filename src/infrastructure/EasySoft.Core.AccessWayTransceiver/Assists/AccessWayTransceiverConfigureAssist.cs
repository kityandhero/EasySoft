using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Core.AccessWayTransceiver.Assists;

/// <summary>
/// AccessWayTransceiverConfigureAssist
/// </summary>
public class AccessWayTransceiverConfigureAssist
{
    #region Fields

    /// <summary>
    /// AccessWayTransceiverConfigureNode
    /// </summary>
    public const string AccessWayTransceiverConfigureNode = "AccessWayTransceiver";

    /// <summary>
    /// AccessWayMessageServiceKey
    /// </summary>
    public const string AccessWayMessageTransceiver = "AccessWayMessageTransceiver";

    /// <summary>
    /// AccessWayMessageServiceKey
    /// </summary>
    public const string AccessWayMessageServiceKey = "AccessWayMessageTransceiver";

    /// <summary>
    /// AccessWayMessageExchange
    /// </summary>
    public const string AccessWayMessageExchange = "AccessWayMessageExchange";

    /// <summary>
    /// AccessWayMessageRoutingKey
    /// </summary>
    public const string AccessWayMessageRoutingKey = "AccessWayMessageRoutingKey";

    /// <summary>
    /// AccessWayMessageQueue
    /// </summary>
    public const string AccessWayMessageQueue = "AccessWayMessageQueue";

    /// <summary>
    /// AccessWayTransmission
    /// </summary>
    public const string AccessWayTransmission = "Transmission";

    /// <summary>
    /// AccessWayPrefix
    /// </summary>
    public const string AccessWayPrefix = "Prefix";

    #endregion

    #region Properties

    /// <summary>
    /// Configuration
    /// </summary>
    public IConfiguration Configuration { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// AccessWayTransceiverConfigureAssist
    /// </summary>
    /// <param name="configuration"></param>
    public AccessWayTransceiverConfigureAssist(IConfiguration configuration)
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
                AccessWayTransceiverConfigureNode,
                AccessWayTransmission
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
    /// 获取 AccessWayTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetRabbitMQConnectionString(IConfiguration configuration)
    {
        var transmissionConfigValue = GetTransmission(configuration);

        var node = StringAssist.Join(
            [
                AccessWayTransceiverConfigureNode,
                "RabbitMQ",
                "ConnectionString"
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        if (transmissionConfigValue.Value && string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                $"AccessWay 已经配置为开启，[{node}]配置无效，请设置"
            );
        }

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// 获取 AccessWayTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetRabbitMQConnectionString()
    {
        return GetRabbitMQConnectionString(Configuration);
    }

    #endregion

    #region GetPrefix

    /// <summary>
    /// 获取 AccessWayTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetPrefix(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AccessWayTransceiverConfigureNode,
                AccessWayPrefix
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
    /// 获取 AccessWayTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetPrefix()
    {
        return GetPrefix(Configuration);
    }

    #endregion

    #endregion
}