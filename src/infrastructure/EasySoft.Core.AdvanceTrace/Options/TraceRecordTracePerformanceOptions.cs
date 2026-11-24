namespace EasySoft.Core.AdvanceTrace.Options;

/// <summary>
/// 链路拦截性能配置
/// 默认值偏向低开销：载荷关闭、脱敏关闭、缓存开启、截断开启
/// 所有项均可在 DI 配置文件中覆盖
/// </summary>
public sealed class TraceRecordTracePerformanceOptions
{
    /// <summary>
    /// 链路总开关。
    /// false：拦截器不创建任何 Activity，直接放行业务方法，几乎零开销。
    /// true：按特性规则判断是否追踪。
    /// </summary>
    public bool TracingSwitch { get; set; } = true;

    /// <summary>
    /// 是否开启方法特性解析缓存。
    /// true：第一次反射解析后缓存到 ConcurrentDictionary，后续无反射开销；
    /// false：每次拦截都执行 GetCustomAttribute，高频方法下反射成本明显。
    /// 拦截器生命周期较长时建议保持 true。
    /// </summary>
    public bool AttributeCacheSwitch { get; set; } = true;

    /// <summary>
    /// 是否采集入参与返回值载荷。
    /// 这是本拦截器最高风险开销点：Newtonsoft 反射序列化对象、分配字符串与临时对象，
    /// 复杂对象图/集合容易造成 GC 压力。默认关闭，仅应急排查时局部开启。
    /// </summary>
    public bool CapturePayloadSwitch { get; set; } = false;

    /// <summary>
    /// 载荷序列化后是否执行敏感字段正则脱敏。
    /// 关闭则跳过整段正则替换，降低 CPU 与正则编译开销；
    /// 仅在 CapturePayloadSwitch = true 时有意义。
    /// </summary>
    public bool SensitiveMaskingSwitch { get; set; } = false;

    /// <summary>
    /// 载荷是否执行超长截断。
    /// true：序列化结果超过 MaxPayloadLength 时截断并追加标记；
    /// false：完整序列化，超大对象可能直接撑爆内存/字符串。
    /// </summary>
    public bool PayloadTruncateSwitch { get; set; } = true;

    /// <summary>
    /// 异常时是否额外写入 exception_type 标签。
    /// 关闭后仅记录异常状态与消息，少一次 Type.FullName 取值与 Tag 写入。
    /// </summary>
    public bool RecordExceptionTypeTagSwitch { get; set; } = true;

    /// <summary>
    /// 是否写入类名、方法名基础 Tag。
    /// 关闭后 Activity 只保留名称，不写 class/method 两个 Tag。
    /// </summary>
    public bool MethodTagSwitch { get; set; } = true;

    /// <summary>
    /// 载荷序列化后允许的最大字符长度，超出按此值截断。
    /// 仅在 PayloadTruncateSwitch = true 时生效。
    /// </summary>
    public int MaxPayloadLength { get; set; } = 2048;
}