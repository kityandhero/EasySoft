using EasySoft.Core.AdvanceTrace.Options;
using EasySoft.UtilityTools.Standard.Exceptions;

namespace EasySoft.Core.AdvanceTrace.Extensions;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 注入轨迹跟踪
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="serviceName">服务资源名</param>
    /// <param name="sourceName">埋点源名称</param>
    /// <param name="openTelemetryProtocolEndpoint">遥测点</param>
    /// <param name="tracingSwitch">跟踪全局开关</param>
    /// <param name="action">选项配置器</param>
    /// <returns></returns> 
    public static WebApplicationBuilder AddAdvanceTrace(
        this WebApplicationBuilder builder,
        string serviceName,
        string sourceName,
        string openTelemetryProtocolEndpoint,
        bool tracingSwitch,
        Action<TraceRecordTracePerformanceOptions> action = null
    )
    {
        builder.Services.AddAdvanceTrace(sourceName, action);

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(r => { r.AddService(serviceName); }
            )
            .WithTracing(tracing =>
                {
                    if (!tracingSwitch)
                    {
                        return;
                    }

                    // 跟踪自动链
                    tracing.AddAspNetCoreInstrumentation();

                    // 调试强制全采样
                    tracing.SetSampler(new AlwaysOnSampler());

                    // 做本地判定
                    tracing.AddConsoleExporter();

                    // tracing.AddAspNetCoreInstrumentation()
                    //     .AddHttpClientInstrumentation();

                    tracing.AddSource(sourceName);

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

                                    // 集成调试用：立刻发送，不批量排队
                                    // o.BatchExportProcessorOptions = new BatchExportProcessorOptions<Activity>
                                    // {
                                    //     MaxQueueSize = 512,
                                    //     MaxExportBatchSize = 1,
                                    //     ScheduledDelayMilliseconds = 1
                                    // };

                                    // 集成调试专用, 与BatchExportProcessorOptions 二选一
                                    // o.ExportProcessorType = ExportProcessorType.Simple;
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