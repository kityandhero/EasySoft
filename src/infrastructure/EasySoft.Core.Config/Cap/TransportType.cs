using System.ComponentModel;

namespace EasySoft.Core.Config.Cap;

/// <summary>
/// 传输方式
/// </summary>
[Description("传输方式")]
public enum TransportType
{
    /// <summary>
    /// AmazonSQS
    /// </summary>
    [Description("AmazonSQS")]
    AmazonSQS = 100,

    /// <summary>
    /// Kafka
    /// </summary>
    [Description("Kafka")]
    Kafka = 200,

    /// <summary>
    /// Pulsar
    /// </summary>
    [Description("Pulsar")]
    Pulsar = 300,

    /// <summary>
    /// AzureServiceBus
    /// </summary>
    [Description("AzureServiceBus")]
    AzureServiceBus = 400,

    /// <summary>
    /// NATS
    /// </summary>
    [Description("NATS")]
    NATS = 500,

    /// <summary>
    /// RabbitMQ
    /// </summary>
    [Description("RabbitMQ")]
    RabbitMQ = 600,

    /// <summary>
    /// Redis
    /// </summary>
    [Description("Redis")]
    Redis = 700,

    /// <summary>
    /// InMemoryMessageQueue
    /// </summary>
    [Description("InMemoryMessageQueue")]
    InMemoryMessageQueue = 800
}