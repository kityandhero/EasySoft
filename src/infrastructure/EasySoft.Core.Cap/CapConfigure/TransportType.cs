namespace EasySoft.Core.Cap.CapConfigure;

public enum TransportType
{
    AmazonSQS = 100,

    Kafka = 200,

    Pulsar = 300,

    AzureServiceBus = 400,

    NATS = 500,

    RabbitMQ = 600,

    Redis = 700,

    InMemoryMessageQueue = 800,
}