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
    /// UseTransmissionConfigureNode
    /// </summary>
    public const string UseTransmissionConfigureNode = "UseTransmission";

    /// <summary>
    /// UseLocalRecordConfigureNode
    /// </summary>
    public const string UseLocalRecordConfigureNode = "UseLocalRecord";

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
                ErrorLogTransceiverConfigureNode,
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
                ErrorLogTransceiverConfigureNode,
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
    /// 获取 ErrorLogTransceiver RabbitMQ 连接字符串
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetRabbitMQConnectionString(IConfiguration configuration)
    {
        var useTransmissionConfigValue = GetUseTransmission(
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

        var configValue = new ConfigValue<string>
        {
            Node = node,
            Value = v,
            EffectiveValueDescription = ""
        };

        return configValue;
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