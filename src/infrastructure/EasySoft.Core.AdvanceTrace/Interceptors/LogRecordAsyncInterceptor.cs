using EasySoft.Core.AdvanceTrace.Attributes;
using EasySoft.Core.AdvanceTrace.Configs;
using EasySoft.Core.AdvanceTrace.Options;
using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.AdvanceTrace.Interceptors;

/// <summary>
/// Castle DynamicProxy 标准异步拦截器，实现 IAsyncInterceptor。
/// 支持同步方法、Task 异步、ValueTask 异步三类拦截分支。
///<para>
/// 解析优先级（由高到低）：
/// 1. 全局 TracingSwitch = false → 直接放行；
/// 2. 方法标记 [LogRecordExclude] → 不追踪；
/// 3. 方法本身标记 [LogRecord] → 使用方法配置；
/// 4. 类标记 [LogRecord]，配合 Inherit 向上遍历基类；
/// 5. 无任何标记 → 不追踪。
///
/// 备注：接口上的特性不自动回溯，Castle 接口代理反射结果不稳定；
/// 需要追踪请标记到实现类或具体方法上。
/// </para>
/// </summary>
public sealed class LogRecordAsyncInterceptor : IAsyncInterceptor
{
    /// <summary>
    /// 方法配置缓存：MethodInfo 作为 Key。
    /// 仅在 AttributeCacheSwitch = true 时写入；关闭后每次重新解析。
    /// </summary>
    private static readonly ConcurrentDictionary<MethodInfo, MethodTraceConfig> MethodConfigCache = new();

    /// <summary>
    /// OpenTelemetry 链路源，所有 Activity 由此创建。
    /// </summary>
    private readonly ActivitySource _activitySource;

    /// <summary>
    /// 性能开关配置。
    /// </summary>
    private readonly LogRecordTracePerformanceOptions _options;

    /// <summary>
    /// Newtonsoft 序列化器，复用实例避免每次新建。
    /// </summary>
    private static readonly JsonSerializerSettings DefaultJsonSettings = JsonConvertAssist
        .CreateJsonSerializerSettings(false);

    /// <summary>
    /// 敏感字段关键字，命中则脱敏。  
    /// </summary>
    private static readonly HashSet<string> SensitiveKeys = new(StringComparer.OrdinalIgnoreCase)
    {
        "password",
        "pwd",
        "secret",
        "token",
        "accessToken",
        "idCard",
        "phone",
        "mobile",
        "bankCard",
        "creditCard"
    };

    /// <summary>
    /// 构造拦截器。
    /// </summary>
    /// <param name="sourceName">ActivitySource 名称，链路分析工具识别使用。</param>
    /// <param name="options">性能开关配置；传 null 使用默认低开销配置。</param>
    public LogRecordAsyncInterceptor(
        string sourceName,
        LogRecordTracePerformanceOptions options = null
    )
    {
        _activitySource = new ActivitySource(sourceName);
        _options = options ?? new LogRecordTracePerformanceOptions();
    }

    #region IAsyncInterceptor 入口

    /// <summary>
    /// 同步方法拦截分支。
    /// </summary>
    public void InterceptSynchronous(IInvocation invocation)
    {
        // 全局关闭：直接放行，不做任何反射与 Activity 创建。
        if (!_options.TracingSwitch)
        {
            invocation.Proceed();

            return;
        }

        var cfg = ResolveMethodConfig(invocation.Method);

        if (cfg.Excluded || !cfg.ShouldTrace)
        {
            invocation.Proceed();
            return;
        }

        using var activity = StartActivity(invocation, cfg);

        try
        {
            invocation.Proceed();

            SetReturnPayload(
                activity,
                invocation.ReturnValue,
                cfg
            );
        }
        catch (Exception ex)
        {
            MarkException(activity, ex);

            throw;
        }
    }

    /// <summary>
    /// 返回 Task 的异步方法拦截分支。
    /// </summary>
    public void InterceptAsynchronous(IInvocation invocation)
    {
        invocation.ReturnValue = InternalInterceptTask(invocation);
    }

    /// <summary>
    /// 返回 ValueTask&lt;TResult&gt; 的异步方法拦截分支。
    /// </summary>
    public void InterceptAsynchronous<TResult>(IInvocation invocation)
    {
        invocation.ReturnValue = InternalInterceptValueTask<TResult>(invocation);
    }

    #endregion

    #region 异步内部实现

    /// <summary>
    /// Task 异步拦截内部实现。
    /// </summary>
    private async Task InternalInterceptTask(IInvocation invocation)
    {
        if (!_options.TracingSwitch)
        {
            invocation.Proceed();

            var ret = invocation.ReturnValue;

            if (ret is Task taskToAwait)
            {
                await taskToAwait;
            }
            else if (ret != null)
            {
                throw new InvalidCastException($"Expected Task, actual {ret.GetType().Name}");
            }

            return;
        }

        var cfg = ResolveMethodConfig(invocation.Method);

        if (cfg.Excluded || !cfg.ShouldTrace)
        {
            invocation.Proceed();

            var ret = invocation.ReturnValue;

            if (ret is Task taskToAwait)
            {
                await taskToAwait;
            }
            else if (ret != null)
            {
                throw new InvalidCastException($"Expected Task, actual {ret.GetType().Name}");
            }

            return;
        }

        using var activity = StartActivity(invocation, cfg);

        try
        {
            invocation.Proceed();

            var returnValue = invocation.ReturnValue;

            if (returnValue is Task task)
            {
                await task;

                SetReturnPayload(
                    activity,
                    returnValue,
                    cfg
                );
            }
            else if (returnValue != null)
            {
                var message = $"Expected Task, actual {returnValue.GetType().Name}";

                activity?.SetStatus(ActivityStatusCode.Error, message);

                throw new InvalidCastException(message);
            }
            else
            {
                var message = "Return value is null, expected Task";

                activity?.SetStatus(ActivityStatusCode.Error, message);

                throw new InvalidCastException(message);
            }
        }
        catch (Exception ex)
        {
            MarkException(activity, ex);

            throw;
        }
    }

    /// <summary>ValueTask 异步拦截内部实现。</summary>
    private async ValueTask<TResult> InternalInterceptValueTask<TResult>(IInvocation invocation)
    {
        if (!_options.TracingSwitch)
        {
            invocation.Proceed();

            var ret = invocation.ReturnValue;

            if (ret is ValueTask<TResult> vt)
            {
                return await vt;
            }

            throw new InvalidCastException(
                $"Expected ValueTask<{typeof(TResult).Name}>, but return value is {(ret == null ? "null" : ret.GetType().Name)}"
            );
        }

        var cfg = ResolveMethodConfig(invocation.Method);

        if (cfg.Excluded || !cfg.ShouldTrace)
        {
            invocation.Proceed();

            var ret = invocation.ReturnValue;

            if (ret is ValueTask<TResult> vt)
            {
                return await vt;
            }

            throw new InvalidCastException(
                $"Expected ValueTask<{typeof(TResult).Name}>, but return value is {(ret == null ? "null" : ret.GetType().Name)}"
            );
        }

        using var activity = StartActivity(invocation, cfg);

        try
        {
            invocation.Proceed();

            var ret = invocation.ReturnValue;

            if (ret is ValueTask<TResult> valueTask)
            {
                var result = await valueTask;

                SetReturnPayload(
                    activity,
                    result,
                    cfg
                );

                return result;
            }

            var message = ret == null
                ? "Return value is null, expected ValueTask<TResult>"
                : $"Return value type mismatch: expected ValueTask<{typeof(TResult).Name}>, actual {ret.GetType().Name}";

            activity?.SetStatus(ActivityStatusCode.Error, message);

            throw new InvalidCastException(message);
        }
        catch (Exception ex)
        {
            MarkException(activity, ex);

            throw;
        }
    }

    #endregion

    #region Activity 写入

    /// <summary>
    /// 创建并初始化 Activity 链路节点。
    /// 基础 Tag 与入参载荷分别受对应开关控制。
    /// </summary>
    private Activity StartActivity(IInvocation invocation, MethodTraceConfig cfg)
    {
        var method = invocation.Method;

        var activityName = $"{method.DeclaringType?.Name}.{method.Name}";

        var activity = _activitySource.StartActivity(
            activityName,
            ActivityKind.Internal
        );

        // 采样关闭时 StartActivity 返回 null，直接跳过后续写入。
        if (activity == null)
        {
            return null;
        }

        // 基础方法 Tag 开关。
        if (_options.MethodTagSwitch)
        {
            activity.SetTag("class", method.DeclaringType?.FullName);
            activity.SetTag("method", method.Name);
        }

        // 入参载荷：全局开关 + 方法特性请求同时满足才序列化。
        if (ShouldCapture(cfg))
        {
            activity.SetTag("arguments", SerializeSafe(invocation.Arguments));
        }

        return activity;
    }

    /// <summary>
    /// 写入返回值载荷，受全局与特性双重开关控制。
    /// </summary>
    private void SetReturnPayload(
        Activity activity,
        object returnValue,
        MethodTraceConfig cfg
    )
    {
        if (activity == null || !ShouldCapture(cfg))
        {
            return;
        }

        activity.SetTag("return_value", SerializeSafe(returnValue));
    }

    /// <summary>
    /// 标记异常状态；异常类型标签受开关控制。
    /// </summary>
    private void MarkException(Activity activity, Exception ex)
    {
        if (activity == null)
        {
            return;
        }

        activity.SetStatus(ActivityStatusCode.Error, ex.Message);

        if (_options.RecordExceptionTypeTagSwitch)
        {
            activity.AddTag("exception_type", ex.GetType().FullName);
        }
    }

    #endregion

    #region 序列化与脱敏

    /// <summary>
    /// 判断是否采集载荷：全局 CapturePayloadSwitch 开启，且方法特性请求开启。
    /// 全局关闭时特性不生效，防止单点误开造成高开销。
    /// </summary>
    private bool ShouldCapture(MethodTraceConfig cfg)
    {
        return _options.CapturePayloadSwitch && cfg.CapturePayload;
    }

    /// <summary>
    /// 安全序列化（Newtonsoft.Json），按开关执行脱敏与截断。
    /// 序列化失败不打断主流程，返回固定标记串。
    /// </summary>
    private string SerializeSafe(object value)
    {
        if (value == null)
        {
            return "null";
        }

        string json;

        try
        {
            json = JsonConvert.SerializeObject(value, DefaultJsonSettings);
        }
        catch
        {
            return "{serialize_failed}";
        }

        // 脱敏开关：关闭则跳过正则替换。
        if (_options.SensitiveMaskingSwitch)
        {
            json = MaskSensitive(json);
        }

        // 截断开关：保护超大对象。
        if (_options.PayloadTruncateSwitch && json.Length > _options.MaxPayloadLength)
        {
            json = json.Substring(
                0,
                _options.MaxPayloadLength
            ) + " ... [truncated]";
        }

        return json;
    }

    /// <summary>
    /// 敏感字段脱敏：对已知关键字执行 JSON 键值正则替换。
    /// 仅在 SensitiveMaskingSwitch = true 时被调用。
    /// </summary>
    private string MaskSensitive(string json)
    {
        foreach (var key in SensitiveKeys)
        {
            var pattern = $"\"{key}\":\"[^\"]+\"";

            json = Regex.Replace(
                json,
                pattern,
                $"\"{key}\":\"******\"",
                RegexOptions.IgnoreCase
            );
        }

        return json;
    }

    #endregion

    #region 特性解析（带缓存开关）

    /// <summary>
    /// 解析当前方法对应的追踪配置。
    /// 缓存开关开启时走 ConcurrentDictionary；关闭时每次重新反射。
    /// </summary>
    private MethodTraceConfig ResolveMethodConfig(MethodInfo methodInfo)
    {
        if (!_options.AttributeCacheSwitch)
        {
            return ResolveCore(methodInfo);
        }

        return MethodConfigCache.GetOrAdd(methodInfo, ResolveCore);
    }

    /// <summary>
    /// 解析核心逻辑：排除标记 → 方法标记 → 类标记 + 基类继承遍历。
    /// </summary>
    private MethodTraceConfig ResolveCore(MethodInfo methodInfo)
    {
        // 第一步：排除标记优先级最高。
        if (methodInfo.GetCustomAttribute<LogRecordExcludeAttribute>() != null)
        {
            return new MethodTraceConfig { Excluded = true };
        }

        // 第二步：方法自身特性优先。
        var methodAttr = methodInfo.GetCustomAttribute<LogRecordAttribute>();

        if (methodAttr != null)
        {
            return new MethodTraceConfig
            {
                ShouldTrace = true,
                CapturePayload = methodAttr.CapturePayload
            };
        }

        // 第三步：类级别，沿基类向上查找，受 Inherit 控制。
        var currentType = methodInfo.DeclaringType;

        while (currentType != null)
        {
            var classAttr = currentType.GetCustomAttribute<LogRecordAttribute>();

            if (classAttr != null)
            {
                return new MethodTraceConfig
                {
                    ShouldTrace = true,
                    CapturePayload = classAttr.CapturePayload
                };
            }

            // 当前类未开启 Inherit，停止向上追溯基类。
            var checkSelf = currentType.GetCustomAttribute<LogRecordAttribute>();

            if (checkSelf == null || !checkSelf.Inherit)
            {
                break;
            }

            currentType = currentType.BaseType;
        }

        // 全程无匹配标记，不追踪。
        return new MethodTraceConfig { ShouldTrace = false };
    }

    #endregion
}