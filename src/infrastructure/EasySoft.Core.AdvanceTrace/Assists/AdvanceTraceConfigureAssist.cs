using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Core.AdvanceTrace.Assists;

/// <summary>
/// AdvanceTraceConfigureAssist
/// </summary>
public class AdvanceTraceConfigureAssist
{
    #region Fields

    /// <summary>
    /// AdvanceTrace 配置节点
    /// </summary>
    private const string AdvanceTraceConfigureNode = "AdvanceTrace";

    /// <summary>
    /// 服务资源名 配置节点
    /// </summary>
    /// <remarks>
    /// <para> 作用：标识当前整个进程/服务实例，输出到 trace 的 Resource 顶层属性 </para>
    /// <para> 用途：Zipkin / Jaeger / 监控平台用来归类、筛选整条服务的链路 </para>
    /// <para> 归属：部署维度，适合放配置文件，同一份框架代码部署不同业务时修改 </para>
    /// <para> 和 ActivitySource 不是一回事：它不区分埋点模块，代表“这个应用本身” </para>
    /// </remarks>
    private const string TrackingServiceNameConfigureNode = "ServiceName";

    /// <summary>
    /// 埋点源名称 配置节点
    /// </summary>
    /// <remarks>
    /// <para> 作用：标识一组埋点、自定义 Span 的来源，对应 trace 里的 InstrumentationScope </para>
    /// <para> 用途：区分 Span 属于哪个业务模块/组件，用来过滤埋点、控制采样 </para>
    /// <para> 归属：代码/模块维度，属于静态约定，一般不做成配置项 </para>
    /// <para> 示例：EasySoft.Order、EasySoft.Payment，一个服务内可以存在多个不同 Source </para>
    /// </remarks>
    private const string TrackingSourceNameConfigureNode = "SourceName";

    /// <summary>
    /// 遥测点 配置节点
    /// </summary>
    private const string OpenTelemetryProtocolEndpointConfigureNode = "OpenTelemetryProtocolEndpoint";

    /// <summary>
    /// 总开关 配置节点
    /// </summary>
    private const string TracingSwitchConfigureNode = "Tracing";

    /// <summary>
    /// 开启特性缓存 配置节点
    /// </summary>
    private const string AttributeCacheSwitchConfigureNode = "AttributeCache";

    /// <summary>
    /// 常态关闭载荷 配置节点
    /// </summary>
    public const string CapturePayloadSwitchConfigureNode = "CapturePayload";

    /// <summary>
    /// 脱敏按需开启 配置节点
    /// </summary>
    public const string SensitiveMaskingSwitchConfigureNode = "SensitiveMasking";

    /// <summary>
    /// 保留截断保护 配置节点
    /// </summary>
    public const string PayloadTruncateSwitchConfigureNode = "PayloadTruncate";

    /// <summary>
    /// 记录异常类型标记 配置节点
    /// </summary>
    public const string RecordExceptionTypeTagSwitchConfigureNode = "RecordExceptionTypeTag";

    /// <summary>
    /// 开启方法标记 配置节点
    /// </summary>
    public const string MethodTagSwitchConfigureNode = "MethodTag";

    /// <summary>
    /// 载荷序上限 配置节点
    /// </summary>
    private const string MaxPayloadLengthConfigureNode = "MaxPayloadLength";

    #endregion

    #region Properties

    /// <summary>
    /// 配置集
    /// </summary>
    public IConfiguration Configuration { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// 跟踪配置
    /// </summary>
    /// <param name="configuration"></param>
    public AdvanceTraceConfigureAssist(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #endregion

    #region Methods

    #region GetTrackingServiceName

    /// <summary>
    /// 获取埋点源名称
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetTrackingServiceName(IConfiguration configuration)
    {
        var tracingSwitchConfigValue = GetTracingSwitch(configuration);

        var node = StringAssist.Join(
            [
                AdvanceTraceConfigureNode,
                TrackingServiceNameConfigureNode
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

        if (tracingSwitchConfigValue.Value && string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription("启用跟踪情况下，需要配置")
            );
        }

        return configValue;
    }

    /// <summary>
    /// 获取埋点源名称
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetTrackingServiceName()
    {
        return GetTrackingServiceName(Configuration);
    }

    #endregion

    #region GetTrackingSourceName

    /// <summary>
    /// 获取追踪源名称
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetTrackingSourceName(IConfiguration configuration)
    {
        var tracingSwitchConfigValue = GetTracingSwitch(configuration);

        var node = StringAssist.Join(
            [
                AdvanceTraceConfigureNode,
                TrackingSourceNameConfigureNode
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

        if (tracingSwitchConfigValue.Value && string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription("启用跟踪情况下，需要配置")
            );
        }

        return configValue;
    }

    /// <summary>
    /// 获取追踪源名称
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetTrackingSourceName()
    {
        return GetTrackingSourceName(Configuration);
    }

    #endregion

    #region GetOpenTelemetryProtocolEndpoint

    /// <summary>
    /// 获取遥测点
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetOpenTelemetryProtocolEndpoint(IConfiguration configuration)
    {
        var tracingSwitchConfigValue = GetTracingSwitch(configuration);

        var node = StringAssist.Join(
            [
                AdvanceTraceConfigureNode,
                OpenTelemetryProtocolEndpointConfigureNode
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

        if (tracingSwitchConfigValue.Value && string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription("启用跟踪情况下，需要配置")
            );
        }

        return configValue;
    }

    /// <summary>
    /// 获取遥测点
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetOpenTelemetryProtocolEndpoint()
    {
        return GetOpenTelemetryProtocolEndpoint(Configuration);
    }

    #endregion

    #region GetTracingSwitch

    /// <summary>
    /// 获取总开关
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetTracingSwitch(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AdvanceTraceConfigureNode,
                TracingSwitchConfigureNode
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
    /// 获取总开关
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetTracingSwitch()
    {
        return GetTracingSwitch(Configuration);
    }

    #endregion

    #region GetAttributeCacheSwitch

    /// <summary>
    /// 获取特性缓存开关
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetAttributeCacheSwitch(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AdvanceTraceConfigureNode,
                AttributeCacheSwitchConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "true";

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
    /// 获取特性缓存开关
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetAttributeCacheSwitch()
    {
        return GetAttributeCacheSwitch(Configuration);
    }

    #endregion

    #region GetCapturePayloadSwitch

    /// <summary>
    /// 获取常态载荷开关
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetCapturePayloadSwitch(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AdvanceTraceConfigureNode,
                CapturePayloadSwitchConfigureNode
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
    /// 获取常态载荷开关
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetCapturePayloadSwitch()
    {
        return GetCapturePayloadSwitch(Configuration);
    }

    #endregion

    #region GetSensitiveMaskingSwitch

    /// <summary>
    /// 获取脱敏开关
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetSensitiveMaskingSwitch(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AdvanceTraceConfigureNode,
                SensitiveMaskingSwitchConfigureNode
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
    /// 获取脱敏开关
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetSensitiveMaskingSwitch()
    {
        return GetSensitiveMaskingSwitch(Configuration);
    }

    #endregion

    #region GetPayloadTruncateSwitch

    /// <summary>
    /// 获取截断保护开关
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetPayloadTruncateSwitch(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AdvanceTraceConfigureNode,
                PayloadTruncateSwitchConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "true";

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
    /// 获取截断保护开关
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetPayloadTruncateSwitch()
    {
        return GetPayloadTruncateSwitch(Configuration);
    }

    #endregion

    #region GetRecordExceptionTypeTagSwitch

    /// <summary>
    /// 获取异常类型标签记录开关
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetRecordExceptionTypeTagSwitch(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AdvanceTraceConfigureNode,
                RecordExceptionTypeTagSwitchConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "true";

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
    /// 获取异常类型标签记录开关
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetRecordExceptionTypeTagSwitch()
    {
        return GetRecordExceptionTypeTagSwitch(Configuration);
    }

    #endregion

    #region GetMethodTagSwitch

    /// <summary>
    /// 获取方法标签开关
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetMethodTagSwitch(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AdvanceTraceConfigureNode,
                MethodTagSwitchConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "true";

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
    /// 获取异常类型标签记录开关
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetMethodTagSwitch()
    {
        return GetMethodTagSwitch(Configuration);
    }

    #endregion

    #region GetMaxPayloadLength

    /// <summary>
    /// 获取最大荷载配置
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<int> GetMaxPayloadLength(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AdvanceTraceConfigureNode,
                MaxPayloadLengthConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "2048";

        var executiveResult = v.TryPraseInt(2048);

        if (!executiveResult.Success)
        {
            throw new ConfigErrorException(
                ConfigValueAssist.BuildErrorDescription(
                    node,
                    v,
                    "大于零的数字",
                    ""
                )
            );
        }

        var configValue = new ConfigValue<int>
        {
            Node = node,
            Value = executiveResult.Data,
            EffectiveValueDescription = "大于零的数字"
        };

        return configValue;
    }

    /// <summary>
    /// 获取异常类型标签记录开关
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<int> GetMaxPayloadLength()
    {
        return GetMaxPayloadLength(Configuration);
    }

    #endregion

    #endregion
}