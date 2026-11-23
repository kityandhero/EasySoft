using EasySoft.Core.AdvanceTrace.Options;
using EasySoft.UtilityTools.Standard.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Exporter;
using OpenTelemetry.Trace;

namespace EasySoft.Core.AdvanceTrace.Extensions;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 注入轨迹跟踪
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="tracingSwitch">跟踪全局开关</param>
    /// <param name="trackingSourceName">追踪源名称</param>
    /// <param name="openTelemetryProtocolEndpoint">遥测点</param>
    /// <param name="action">选项配置器</param>
    /// <returns></returns> 
    public static WebApplicationBuilder AddAdvanceTrace(
        this WebApplicationBuilder builder,
        bool tracingSwitch,
        string trackingSourceName,
        string openTelemetryProtocolEndpoint,
        Action<LogRecordTracePerformanceOptions> action = null
    )
    {
        builder.Services.AddAdvanceTrace(trackingSourceName, action);

        builder.Services.AddOpenTelemetry()
            .WithTracing(tracing =>
                {
                    if (!tracingSwitch)
                    {
                        return;
                    }

                    tracing.AddSource(trackingSourceName);

                    if (!string.IsNullOrWhiteSpace(openTelemetryProtocolEndpoint))
                    {
                        if (Uri.TryCreate(
                                openTelemetryProtocolEndpoint,
                                UriKind.Absolute,
                                out var validUri
                            ))
                        {
                            tracing.AddOtlpExporter(o =>
                                {
                                    o.Endpoint = validUri;
                                    o.Protocol = OtlpExportProtocol.HttpProtobuf;
                                }
                            );
                        }
                        else
                        {
                            throw new ConfigErrorException(
                                $"无法解析 OtlpEndpoint 为合法绝对 Uri：{openTelemetryProtocolEndpoint}"
                            );
                        }
                    }
                }
            );

        return builder;
    }
}