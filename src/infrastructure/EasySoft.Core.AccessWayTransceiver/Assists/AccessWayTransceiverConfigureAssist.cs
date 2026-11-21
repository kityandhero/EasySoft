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
    /// UseTransmissionConfigureNode
    /// </summary>
    public const string UseTransmissionConfigureNode = "UseTransmission";

    /// <summary>
    /// UseLocalRecordConfigureNode
    /// </summary>
    public const string UseLocalRecordConfigureNode = "UseLocalRecord";

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

    #region GetUseLocalRecord

    /// <summary>
    /// 是否适用本地记录
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetUseLocalRecord(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AccessWayTransceiverConfigureNode,
                UseLocalRecordConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "false";

        var configValue = new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true",
            EffectiveValueDescription = "true|false"
        };

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription()
            );
        }

        return configValue;
    }

    /// <summary>
    /// 是否适用本地记录
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetUseLocalRecord()
    {
        return GetUseLocalRecord(Configuration);
    }

    #endregion

    #region GetUseTransmission

    /// <summary>
    /// 是否适用传输
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetUseTransmission(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AccessWayTransceiverConfigureNode,
                UseTransmissionConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "false";

        var configValue = new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true",
            EffectiveValueDescription = "true|false"
        };

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription()
            );
        }

        return configValue;
    }

    /// <summary>
    /// 是否适用传输
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetUseTransmission()
    {
        return GetUseTransmission(Configuration);
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
        var useTransmissionConfigValue = GetUseTransmission(configuration);

        var node = StringAssist.Join(
            [
                AccessWayTransceiverConfigureNode,
                "RabbitMQ",
                "ConnectionString"
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        var configValue = new ConfigValue<string>
        {
            Node = node,
            Value = v,
            EffectiveValueDescription = "开启情况下，需要配置 RabbitMQ 连接字符串"
        };

        if (useTransmissionConfigValue.Value && string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription()
            );
        }

        return configValue;
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