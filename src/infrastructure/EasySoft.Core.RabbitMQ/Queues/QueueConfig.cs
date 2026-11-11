namespace EasySoft.Core.RabbitMQ.Queues;

public class QueueConfig
{
    /// <summary>
    /// 队列名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 全局前缀，用于做同类系统不同部署的数据隔离
    /// </summary>
    public string GlobalPrefix { get; set; } = string.Empty;

    /// <summary>
    /// 死信交换机名称
    /// </summary>
    public string DeadExchangeName => $"dead-letter-exchange-{Name}";

    /// <summary>
    /// 死信路由key
    /// </summary>
    public string DeadRoutingKey => $"dead-letter-routing-key-{Name}";

    /// <summary>
    /// 死信队列名称,需要先配置死信交互机
    /// </summary>
    public string DeadQueueName => $"dead-letter-queue-{Name}";

    /// <summary>
    /// 是否持久化队列
    /// </summary>
    public bool Durable { get; set; } = true;

    /// <summary>
    /// 队列消息上限，先进先出，0代表不限制，慎用
    /// </summary>
    public int MaxLength { get; set; } = 500000;

    /// <summary>
    /// 是否独占队列，true 表示队列仅被当前创建它的 Connection 使用，适合临时队列，业务队列慎用；false 表示普通共享队列，多个连接、多个 Worker 实例都可以监听消费，适用生产业务队列。
    /// </summary>
    public bool Exclusive { get; set; } = false;

    /// <summary>
    /// 是否自动删除，当最后一个消费者断开连接之后队列是否自动被删除
    /// </summary>
    public bool AutoDelete { get; set; } = false;

    /// <summary>
    /// 队列扩展参数配置
    /// x-dead-letter-exchange 设置当前队列的DLX(死信交机)
    /// x-dead-letter-routing-key 设置DLX的路由key，DLX会根据该值去找到死信消息存放的队列
    /// x-transitiveMessage-ttl 设置消息的存活时间，即过期时间(毫秒)
    /// </summary>
    public IDictionary<string, object> Arguments { get; set; }

    /// <summary>
    /// 是否开启自动确认
    /// </summary>
    public bool AutoAck { get; set; } = false;

    /// <summary>
    /// false 表示按单个 Consumer 限流（常用），每个消费者独立窗口；true 表示限制整个 Channel 上所有消费者合计未确认消息，慎用。
    /// </summary>
    public bool Global { get; set; } = false;

    /// <summary>
    /// true 表示只确认当前这一条消息，false 表示一次性确认【deliveryTag ≤ 当前 tag】所有未确认的消息（批量确认）。
    /// </summary>
    public bool AckMultiple { get; set; } = false;

    /// <summary>
    /// 消费失败时是否重试，为 true 重新放回队列；为 false，如果配置死信队列，会转义到死信队列，没有则丢弃。
    /// </summary>
    public bool WhetherRetryWhenConsumeFail { get; set; } = false;

    /// <summary>
    /// 单个消费者最多允许有多少条【已推送到客户端、还没有 Ack】的消息，生产严禁用 0，容易内存打爆
    /// </summary>
    public ushort PrefetchCount { get; set; } = 1;

    /// <summary>
    /// 重排队列最大重试次数
    /// </summary>
    public ushort RequeueMaxRetryCount { get; set; } = 3;

    /// <summary>
    /// 本地策略最大重试次数
    /// </summary>
    public ushort LocalMaxRetryCount { get; set; } = 3;

    /// <summary>
    /// 本地重试时间间隔
    /// </summary>
    public ushort LocalRetryDelayMillisecond { get; set; } = 600;
}