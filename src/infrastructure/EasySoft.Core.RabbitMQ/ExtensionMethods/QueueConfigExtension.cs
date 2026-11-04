using EasySoft.Core.RabbitMQ.Queues;

namespace EasySoft.Core.RabbitMQ.ExtensionMethods;

/// <summary>
/// QueueConfig 扩展方法
/// </summary>
public static class QueueConfigExtension
{
    /// <summary>
    /// 获取装配好的名称
    /// </summary>
    /// <param name="queueConfig">原始配置</param> 
    /// <returns>装配好的名称</returns>
    public static string GetAssembledName(
        this QueueConfig queueConfig
    )
    {
        if (string.IsNullOrWhiteSpace(queueConfig.GlobalPrefix))
        {
            return queueConfig.Name;
        }

        return $"{queueConfig.GlobalPrefix}.{queueConfig.Name}";
    }

    /// <summary>
    /// 获取装配好的死信队列名称
    /// </summary>
    /// <param name="queueConfig">原始配置</param> 
    /// <returns>装配好的名称</returns>
    public static string GetAssembledDeadQueueName(
        this QueueConfig queueConfig
    )
    {
        if (string.IsNullOrWhiteSpace(queueConfig.GlobalPrefix))
        {
            return queueConfig.DeadQueueName;
        }

        return $"{queueConfig.GlobalPrefix}.{queueConfig.DeadQueueName}";
    }

    /// <summary>
    /// 获取装配好的死信交换机名称
    /// </summary>
    /// <param name="queueConfig">原始配置</param> 
    /// <returns>装配好的名称</returns>
    public static string GetAssembledDeadExchangeName(
        this QueueConfig queueConfig
    )
    {
        if (string.IsNullOrWhiteSpace(queueConfig.GlobalPrefix))
        {
            return queueConfig.DeadExchangeName;
        }

        return $"{queueConfig.GlobalPrefix}.{queueConfig.DeadExchangeName}";
    }

    /// <summary>
    /// 获取装配好的死信路由key
    /// </summary>
    /// <param name="queueConfig">原始配置</param> 
    /// <returns>装配好的死信路由key</returns>
    public static string GetAssembledDeadRoutingKey(
        this QueueConfig queueConfig
    )
    {
        if (string.IsNullOrWhiteSpace(queueConfig.GlobalPrefix))
        {
            return queueConfig.DeadRoutingKey;
        }

        return $"{queueConfig.GlobalPrefix}.{queueConfig.DeadRoutingKey}";
    }

    /// <summary>
    /// 获取装配好的参数集合
    /// </summary>
    /// <param name="queueConfig">原始配置</param> 
    /// <returns>装配好的参数集合</returns>
    public static IDictionary<string, object> GetAssembledArguments(
        this QueueConfig queueConfig
    )
    {
        queueConfig.Arguments ??= new Dictionary<string, object>();

        if (queueConfig.MaxLength > 0)
        {
            queueConfig.Arguments["x-max-length"] = queueConfig.MaxLength;
        }
        else
        {
            queueConfig.Arguments.Remove("x-max-length");
        }

        if (!string.IsNullOrWhiteSpace(queueConfig.DeadExchangeName) &&
            !string.IsNullOrWhiteSpace(queueConfig.DeadRoutingKey))
        {
            queueConfig.Arguments["x-dead-letter-exchange"] = queueConfig.DeadExchangeName;
            queueConfig.Arguments["x-dead-letter-routing-key"] = queueConfig.DeadRoutingKey;
        }

        return queueConfig.Arguments;
    }
}