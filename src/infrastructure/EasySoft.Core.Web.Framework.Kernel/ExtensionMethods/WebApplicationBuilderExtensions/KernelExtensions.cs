using Asp.Versioning;
using EasySoft.Core.AdvanceNLog.ExtensionMethods;
using EasySoft.Core.AdvanceTrace.Assists;
using EasySoft.Core.AdvanceTrace.Extensions;
using EasySoft.Core.AuthenticationCore.Assists;
using EasySoft.Core.CacheCore.ExtensionMethods;
using EasySoft.Core.CacheImplementation.ExtensionMethods;
using EasySoft.Core.EasyToken.ExtensionMethods;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.JsonWebToken.ExtensionMethods;
using EasySoft.Core.Mapster.ExtensionMethods;
using EasySoft.Core.MediatR.ExtensionMethods;
using EasySoft.Core.Messaging.SlimMessageBus.Extensions;
using EasySoft.Core.MiniProfiler.ExtensionMethods;
using EasySoft.Core.Web.Framework.Kernel.Assists;
using EasySoft.UtilityTools.Core.Extensions;
using EasySoft.UtilityTools.Standard.Interfaces;
using Masuit.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;

namespace EasySoft.Core.Web.Framework.Kernel.ExtensionMethods.WebApplicationBuilderExtensions;

public static class KernelExtensions
{
    private const string UniqueIdentifierAddFrameworkKernel = "4f30df645f594b45a63c5dfc955f34f8";
    private const string IdentifierAddAdvanceApplicationChannel = "ea712530180e4c6580172ef9f9f4a794";

    /// <summary>
    /// 注入框架核心
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="channel">应用通道</param>
    /// <returns></returns>
    internal static WebApplicationBuilder AddFrameworkKernel(
        this WebApplicationBuilder builder,
        IChannel channel
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddFrameworkKernel))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddFrameworkKernel)}()"
        );

        builder.AddAdvanceNLog();

        builder.Services.AddKernelConfigureAssist(builder.Configuration);

        builder.AddAdvanceUrls();
        builder.AddAdvanceApplicationChannel(channel);

        builder.Services.AddSingleton<IHostEnvironment, HostingEnvironment>();

        #region HttpContextAccessor

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddHttpContextAccessor();

        #endregion

        builder.Services.AddHttpClient();

        #region CorsPolicies

        var corsPoliciesConfigValue = KernelConfigureAssist.GetCorsPolicies(builder.Configuration);

        StartupConfigMessageAssist.AddConfig(
            corsPoliciesConfigValue.ToArrowDescription()
        );

        if (!string.IsNullOrWhiteSpace(corsPoliciesConfigValue.Value))
        {
            builder.Services.AddCors(options =>
                {
                    options.AddPolicy(
                        "specificOrigins",
                        configPolicy =>
                        {
                            configPolicy.WithOrigins(corsPoliciesConfigValue.Value);
                            configPolicy.AllowAnyHeader();
                            configPolicy.AllowAnyMethod();
                            configPolicy.AllowCredentials();
                        }
                    );
                }
            );
        }

        #endregion

        #region Compression

        var compressionSwitchConfigValue = KernelConfigureAssist.GetCompression(builder.Configuration);

        StartupConfigMessageAssist.AddConfig(
            compressionSwitchConfigValue.ToArrowDescription()
        );

        if (compressionSwitchConfigValue.Value)
        {
            builder.Services.AddAdvanceResponseCompression();
        }

        #endregion

        builder.Services.AddRouting(o => { o.LowercaseUrls = true; });

        #region ApiVersion

        builder.Services.AddApiVersioning(o =>
            {
                //return versions in a response header
                o.ReportApiVersions = true;

                //default version select 
                o.DefaultApiVersion = new ApiVersion(1, 0);

                //if not specifying an api version,show the default version
                o.AssumeDefaultVersionWhenUnspecified = true;
            }
        );

        #endregion

        builder.Services.AddEndpointsApiExplorer();

        #region Cache

        builder.Services.AddCacheCore(builder.Configuration);

        builder.AddAdvanceCacheImplementation();

        #endregion

        builder.AddAdvanceMapster();

        builder.AddAdvanceMediatR();

        builder.AddAdvanceMiniProfile();

        builder.Services.AddKernelMessagingWithJson();

        #region TokenMode

        var tokenModeConfigValue = AuthenticationConfigureAssist.GetTokenMode(builder.Configuration);

        StartupConfigMessageAssist.AddConfig(
            tokenModeConfigValue.ToArrowDescription()
        );

        if (tokenModeConfigValue.Value == UtilityTools.Standard.ConstCollection.EasyToken)
        {
            builder.AddEasyToken();
        }

        if (tokenModeConfigValue.Value == UtilityTools.Standard.ConstCollection.JsonWebToken)
        {
            builder.AddAdvanceJsonWebToken();
        }

        #endregion

        #region AdvanceTrace

        var trackingServiceNameConfigValue = AdvanceTraceConfigureAssist.GetTrackingServiceName(
            builder.Configuration
        );

        StartupConfigMessageAssist.AddConfig(
            trackingServiceNameConfigValue.ToArrowDescription()
        );

        var trackingSourceNameConfigValue = AdvanceTraceConfigureAssist.GetTrackingSourceName(
            builder.Configuration
        );

        StartupConfigMessageAssist.AddConfig(
            trackingSourceNameConfigValue.ToArrowDescription()
        );

        var openTelemetryProtocolEndpointConfigValue = AdvanceTraceConfigureAssist.GetOpenTelemetryProtocolEndpoint(
            builder.Configuration
        );

        StartupConfigMessageAssist.AddConfig(
            openTelemetryProtocolEndpointConfigValue.ToArrowDescription()
        );

        var tracingSwitchConfigValue = AdvanceTraceConfigureAssist.GetTracingSwitch(
            builder.Configuration
        );

        StartupConfigMessageAssist.AddConfig(
            tracingSwitchConfigValue.ToArrowDescription()
        );

        var attributeCacheSwitchConfigValue = AdvanceTraceConfigureAssist.GetAttributeCacheSwitch(
            builder.Configuration
        );

        StartupConfigMessageAssist.AddConfig(
            attributeCacheSwitchConfigValue.ToArrowDescription()
        );

        var capturePayloadSwitchConfigValue = AdvanceTraceConfigureAssist.GetCapturePayloadSwitch(
            builder.Configuration
        );

        StartupConfigMessageAssist.AddConfig(
            capturePayloadSwitchConfigValue.ToArrowDescription()
        );

        var sensitiveMaskingSwitchConfigValue = AdvanceTraceConfigureAssist.GetSensitiveMaskingSwitch(
            builder.Configuration
        );

        StartupConfigMessageAssist.AddConfig(
            sensitiveMaskingSwitchConfigValue.ToArrowDescription()
        );

        var payloadTruncateSwitchConfigValue = AdvanceTraceConfigureAssist.GetPayloadTruncateSwitch(
            builder.Configuration
        );

        StartupConfigMessageAssist.AddConfig(
            payloadTruncateSwitchConfigValue.ToArrowDescription()
        );

        var recordExceptionTypeTagSwitchConfigValue = AdvanceTraceConfigureAssist.GetRecordExceptionTypeTagSwitch(
            builder.Configuration
        );

        StartupConfigMessageAssist.AddConfig(
            recordExceptionTypeTagSwitchConfigValue.ToArrowDescription()
        );

        var methodTagSwitchConfigValue = AdvanceTraceConfigureAssist.GetMethodTagSwitch(
            builder.Configuration
        );

        StartupConfigMessageAssist.AddConfig(
            methodTagSwitchConfigValue.ToArrowDescription()
        );

        var maxPayloadLengthConfigValue = AdvanceTraceConfigureAssist.GetMaxPayloadLength(
            builder.Configuration
        );

        StartupConfigMessageAssist.AddConfig(
            maxPayloadLengthConfigValue.ToArrowDescription()
        );

        builder.AddAdvanceTrace(
            trackingServiceNameConfigValue.Value,
            trackingSourceNameConfigValue.Value,
            openTelemetryProtocolEndpointConfigValue.Value,
            tracingSwitchConfigValue.Value,
            o =>
            {
                o.TracingSwitch = tracingSwitchConfigValue.Value;
                o.AttributeCacheSwitch = attributeCacheSwitchConfigValue.Value;
                o.CapturePayloadSwitch = capturePayloadSwitchConfigValue.Value;
                o.SensitiveMaskingSwitch = sensitiveMaskingSwitchConfigValue.Value;
                o.PayloadTruncateSwitch = payloadTruncateSwitchConfigValue.Value;
                o.RecordExceptionTypeTagSwitch = recordExceptionTypeTagSwitchConfigValue.Value;
                o.MethodTagSwitch = methodTagSwitchConfigValue.Value;
                o.MaxPayloadLength = maxPayloadLengthConfigValue.Value;
            }
        );

        #endregion

        return builder;
    }

    /// <summary>
    /// AddAdvanceUrls
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private static WebApplicationBuilder AddAdvanceUrls(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceUrls)}()"
        );

        FlagAssist.StartupUrls.Clear();
        FlagAssist.StartupDisplayUrls.Clear();

        var httpPortConfigValue = KernelConfigureAssist.GetHttpPort(builder.Configuration);
        var httpsPortConfigValue = KernelConfigureAssist.GetHttpsPort(builder.Configuration);

        if (httpPortConfigValue.Value > 0)
        {
            FlagAssist.StartupUrls.Add($"http://*:{httpPortConfigValue.Value}");
            FlagAssist.StartupDisplayUrls.Add($"http://localhost:{httpPortConfigValue.Value}");

            StartupConfigMessageAssist.AddConfig(
                httpPortConfigValue.ToArrowDescription()
            );
        }

        if (httpsPortConfigValue.Value > 0)
        {
            FlagAssist.StartupUrls.Add($"https://*:{httpsPortConfigValue.Value}");
            FlagAssist.StartupDisplayUrls.Add($"https://localhost:{httpsPortConfigValue.Value}");

            StartupConfigMessageAssist.AddConfig(
                httpsPortConfigValue.ToArrowDescription()
            );
        }

        builder.WebHost.UseUrls(FlagAssist.StartupUrls.ToArray());

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access application at {FlagAssist.StartupDisplayUrls.Join(" ")}."
        );

        return builder;
    }

    /// <summary>
    /// 标记当前应用通道值, 用于远程日志等数据中, 便于数据辨认, 不使用此方法标记, 框架将采用内置值 0 代替
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="channel">应用通道</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private static WebApplicationBuilder AddAdvanceApplicationChannel(
        this WebApplicationBuilder builder,
        IChannel channel
    )
    {
        if (builder.HasRegistered(IdentifierAddAdvanceApplicationChannel))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceApplicationChannel)}."
        );

        builder.Services.AddAdvanceApplicationChannel(channel);

        return builder;
    }

    private static WebApplicationBuilder AddAdvanceApplicationChannel<T>(
        this WebApplicationBuilder builder,
        T channel
    ) where T : IChannel
    {
        if (builder.HasRegistered(IdentifierAddAdvanceApplicationChannel))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceApplicationChannel)}<{typeof(T).Name}>."
        );

        builder.Services.AddAdvanceApplicationChannel(channel);

        return builder;
    }
}