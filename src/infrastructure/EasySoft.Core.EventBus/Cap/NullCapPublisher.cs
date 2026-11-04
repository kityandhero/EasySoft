namespace EasySoft.Core.EventBus.Cap;

// using DotNetCore.CAP;
// using Microsoft.Extensions.Logging;
// using System.Collections.Generic;
// using System.Threading;
// using System.Threading.Tasks;

/// <summary>
/// 空发布器【带日志完整版，补齐全部接口成员】
/// 适用场景：单元测试 / 本地开发环境，不实际持久化消息、不投递到MQ，消息直接丢弃
/// 增加日志输出，用于调试：确认业务代码是否调用了CAP发布/事务接口
/// 重点：
/// 1. CAP10 ICapPublisher 包含同步 PublishDelay 两个重载，必须实现
/// 2. 接口保留 ServiceProvider、Transaction 属性，需要实现getter
/// 3. ICapTransaction.DbTransaction 为 get;set; 读写属性
/// </summary>
public class NullCapPublisher : ICapPublisher
{
    private readonly ILogger<NullCapPublisher> _logger;

    /// <summary>
    /// 构造函数，注入日志记录器
    /// </summary>
    /// <param name="logger">日志实例</param>
    public NullCapPublisher(ILogger<NullCapPublisher> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 服务提供者，CAP接口要求属性，空发布器返回null
    /// </summary>
    public IServiceProvider ServiceProvider => null!;

    /// <summary>
    /// 当前事务上下文，CAP接口要求属性，空发布器返回null
    /// </summary>
    public ICapTransaction Transaction { get; set; } = null;

    #region 同步 Publish 基础方法

    /// <summary>
    /// 同步发布消息（带回调名称），空实现，消息丢弃
    /// </summary>
    /// <typeparam name="T">消息实体类型</typeparam>
    /// <param name="name">消息主题名称</param>
    /// <param name="contentObj">消息内容</param>
    /// <param name="callbackName">回调订阅名称</param>
    public void Publish<T>(string name, T contentObj, string callbackName = null)
    {
        _logger.LogInformation(
            "[NullCap][同步Publish] EventName={Name}, CallbackName={Callback}",
            name,
            callbackName
        );

        // 空实现：不做任何消息存储与投递
    }

    /// <summary>
    /// 同步发布消息（自定义消息头），空实现，消息丢弃
    /// </summary>
    /// <typeparam name="T">消息实体类型</typeparam>
    /// <param name="name">消息主题名称</param>
    /// <param name="contentObj">消息内容</param>
    /// <param name="headers">自定义消息头字典</param>
    public void Publish<T>(string name, T contentObj, IDictionary<string, string> headers)
    {
        _logger.LogInformation(
            "[NullCap][同步Publish] EventName={Name}, HeadersCount={Cnt}",
            name,
            headers?.Count ?? 0
        );

        // 空实现：不做任何消息存储与投递
    }

    #endregion

    #region 同步延迟消息

    /// <summary>
    /// 同步延时发布消息（指定延迟时长 + 回调名称），空实现，消息丢弃
    /// </summary>
    /// <typeparam name="T">消息实体类型</typeparam>
    /// <param name="delay">延迟时长</param>
    /// <param name="name">消息主题名称</param>
    /// <param name="contentObj">消息内容</param>
    /// <param name="callbackName">回调订阅名称</param>
    public void PublishDelay<T>(
        TimeSpan delay,
        string name,
        T contentObj,
        string callbackName = null
    )
    {
        _logger.LogInformation(
            "[NullCap][同步PublishDelay] EventName={Name}, Delay={Delay}, CallbackName={Callback}",
            name,
            delay,
            callbackName
        );

        // 空实现
    }

    /// <summary>
    /// 同步延时发布消息（指定延迟时长 + 自定义消息头），空实现，消息丢弃
    /// </summary>
    /// <typeparam name="T">消息实体类型</typeparam>
    /// <param name="delay">延迟时长</param>
    /// <param name="name">消息主题名称</param>
    /// <param name="contentObj">消息内容</param>
    /// <param name="headers">自定义消息头字典</param>
    public void PublishDelay<T>(
        TimeSpan delay,
        string name,
        T contentObj,
        IDictionary<string, string> headers
    )
    {
        _logger.LogInformation(
            "[NullCap][同步PublishDelay] EventName={Name}, Delay={Delay}, HeadersCount={Cnt}",
            name,
            delay,
            headers?.Count ?? 0
        );

        // 空实现
    }

    #endregion

    #region 基础异步 Publish 方法

    /// <summary>
    /// 异步发布消息（带回调名称），空实现，消息丢弃
    /// </summary>
    /// <typeparam name="T">消息实体类型</typeparam>
    /// <param name="name">消息主题名称</param>
    /// <param name="contentObj">消息内容</param>
    /// <param name="callbackName">回调订阅名称</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>已完成任务</returns>
    public Task PublishAsync<T>(
        string name,
        T contentObj,
        string callbackName = null,
        CancellationToken cancellationToken = default
    )
    {
        _logger.LogInformation(
            "[NullCap][PublishAsync] EventName={Name}, CallbackName={Callback}",
            name,
            callbackName
        );
        return Task.CompletedTask;
    }

    /// <summary>
    /// 异步发布消息（自定义消息头），空实现，消息丢弃
    /// </summary>
    /// <typeparam name="T">消息实体类型</typeparam>
    /// <param name="name">消息主题名称</param>
    /// <param name="contentObj">消息内容</param>
    /// <param name="headers">自定义消息头字典</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>已完成任务</returns>
    public Task PublishAsync<T>(
        string name,
        T contentObj,
        IDictionary<string, string> headers,
        CancellationToken cancellationToken = default
    )
    {
        _logger.LogInformation(
            "[NullCap][PublishAsync] EventName={Name}, HeadersCount={Cnt}",
            name,
            headers?.Count ?? 0
        );
        return Task.CompletedTask;
    }

    #endregion

    #region 异步延迟消息接口

    /// <summary>
    /// 异步延时发布消息（指定延迟时长），空实现，消息丢弃
    /// </summary>
    /// <typeparam name="T">消息实体类型</typeparam>
    /// <param name="delay">需要延迟多久之后投递</param>
    /// <param name="name">消息主题名称</param>
    /// <param name="contentObj">消息内容</param>
    /// <param name="callbackName">回调订阅名称</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>已完成任务</returns>
    public Task PublishDelayAsync<T>(
        TimeSpan delay,
        string name,
        T contentObj,
        string callbackName = null,
        CancellationToken cancellationToken = default
    )
    {
        _logger.LogInformation(
            "[NullCap][PublishDelayAsync] EventName={Name}, Delay={Delay}, CallbackName={Callback}",
            name,
            delay,
            callbackName
        );
        return Task.CompletedTask;
    }

    /// <summary>
    /// 异步延时发布消息（指定延迟时长 + 自定义消息头），空实现，消息丢弃
    /// </summary>
    /// <typeparam name="T">消息实体类型</typeparam>
    /// <param name="delay">需要延迟多久之后投递</param>
    /// <param name="name">消息主题名称</param>
    /// <param name="contentObj">消息内容</param>
    /// <param name="headers">自定义消息头字典</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>已完成任务</returns>
    public Task PublishDelayAsync<T>(
        TimeSpan delay,
        string name,
        T contentObj,
        IDictionary<string, string> headers,
        CancellationToken cancellationToken = default
    )
    {
        _logger.LogInformation(
            "[NullCap][PublishDelayAsync] EventName={Name}, Delay={Delay}, HeadersCount={Cnt}",
            name,
            delay,
            headers?.Count ?? 0
        );
        return Task.CompletedTask;
    }

    /// <summary>
    /// 异步定时发布消息（指定绝对发布时间点），空实现，消息丢弃
    /// </summary>
    /// <typeparam name="T">消息实体类型</typeparam>
    /// <param name="publishTime">计划投递的绝对时间</param>
    /// <param name="name">消息主题名称</param>
    /// <param name="contentObj">消息内容</param>
    /// <param name="callbackName">回调订阅名称</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>已完成任务</returns>
    public Task PublishAtAsync<T>(
        DateTimeOffset publishTime,
        string name,
        T contentObj,
        string callbackName = null,
        CancellationToken cancellationToken = default
    )
    {
        _logger.LogInformation(
            "[NullCap][PublishAtAsync] EventName={Name}, PublishTime={Time}, CallbackName={Callback}",
            name,
            publishTime,
            callbackName
        );
        return Task.CompletedTask;
    }

    /// <summary>
    /// 异步定时发布消息（指定绝对发布时间点 + 自定义消息头），空实现，消息丢弃
    /// </summary>
    /// <typeparam name="T">消息实体类型</typeparam>
    /// <param name="publishTime">计划投递的绝对时间</param>
    /// <param name="name">消息主题名称</param>
    /// <param name="contentObj">消息内容</param>
    /// <param name="headers">自定义消息头字典</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>已完成任务</returns>
    public Task PublishAtAsync<T>(
        DateTimeOffset publishTime,
        string name,
        T contentObj,
        IDictionary<string, string> headers,
        CancellationToken cancellationToken = default
    )
    {
        _logger.LogInformation(
            "[NullCap][PublishAtAsync] EventName={Name}, PublishTime={Time}, HeadersCount={Cnt}",
            name,
            publishTime,
            headers?.Count ?? 0
        );
        return Task.CompletedTask;
    }

    #endregion

    #region CAP事务相关方法

    /// <summary>
    /// 开启CAP本地事务（无数据库事务对象），返回空事务对象
    /// </summary>
    /// <returns>NullCapTransaction空事务实例</returns>
    public ICapTransaction BeginTransaction()
    {
        _logger.LogDebug("[NullCap][BeginTransaction] 创建空CAP事务（无外部DB事务）");
        return new NullCapTransaction();
    }

    /// <summary>
    /// 开启CAP事务，传入外部数据库事务对象（如EF、DbTransaction），返回空事务对象
    /// </summary>
    /// <param name="dbTransaction">外部数据库事务</param>
    /// <returns>NullCapTransaction空事务实例，内部DbTransaction赋值为传入对象</returns>
    public ICapTransaction BeginTransaction(object dbTransaction)
    {
        _logger.LogDebug("[NullCap][BeginTransaction] 创建空CAP事务，传入外部DB事务对象");
        return new NullCapTransaction { DbTransaction = dbTransaction };
    }

    #endregion
}