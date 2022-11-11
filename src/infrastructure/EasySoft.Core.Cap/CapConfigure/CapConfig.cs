namespace EasySoft.Core.Cap.CapConfigure;

/// <summary>
/// Cap 配置
/// </summary>
public class CapConfig
{
    /// <summary>
    /// 前缀
    /// </summary>
    public string Prefix { get; set; }

    /// <summary>
    /// 传输配置
    /// </summary>
    public Transport Transport { get; set; }

    /// <summary>
    /// 持久话配置
    /// </summary>
    public Persistent Persistent { get; set; }

    /// <summary>
    /// Dashboard 配置
    /// </summary>
    public DashboardOptions DashboardOptions { get; set; }

    /// <summary>
    /// Discovery 配置
    /// </summary>
    public DiscoveryOptions DiscoveryOptions { get; set; }

    /// <summary>
    /// Cap 配置
    /// </summary>
    public CapConfig()
    {
        Prefix = GeneralConfigAssist.GetCapPrefix();

        Transport = new Transport();

        Persistent = new Persistent();

        DashboardOptions = new DashboardOptions();

        DiscoveryOptions = new DiscoveryOptions();
    }
}

/// <summary>
/// 传输配置
/// </summary>
public class Transport
{
    /// <summary>
    /// AmazonSQS
    /// </summary>
    public AmazonSQSOptions? AmazonSQS { get; set; }

    
    /// <summary>
    /// Kafka
    /// </summary>
    public KafkaOptions? Kafka { get; set; }

    /// <summary>
    /// Pulsar
    /// </summary>
    public PulsarOptions? Pulsar { get; set; }

    /// <summary>
    /// AzureServiceBus
    /// </summary>
    public AzureServiceBusOptions? AzureServiceBus { get; set; }

    /// <summary>
    /// NATS
    /// </summary>
    public NATSOptions? NATS { get; set; }

    /// <summary>
    /// RabbitMQ
    /// </summary>
    public RabbitMQOptions? RabbitMQ { get; set; }

    /// <summary>
    /// Redis
    /// </summary>
    public CapRedisOptions? Redis { get; set; }

    /// <summary>
    /// 传输配置
    /// </summary>
    public Transport()
    {
        AmazonSQS = new AmazonSQSOptions();

        Kafka = new KafkaOptions();

        Pulsar = new PulsarOptions();

        AzureServiceBus = new AzureServiceBusOptions();

        NATS = new NATSOptions();

        RabbitMQ = new RabbitMQOptions();

        #region RabbitMQ

        var prefix = GeneralConfigAssist.GetCapPrefix().Remove(" ").Trim().ToLower();

        RabbitMQ.HostName = RabbitMQConfigAssist.GetHostName();
        RabbitMQ.UserName = RabbitMQConfigAssist.GetUserName();
        RabbitMQ.Password = RabbitMQConfigAssist.GetPassword();
        RabbitMQ.VirtualHost = RabbitMQConfigAssist.GetVirtualHost();
        RabbitMQ.ConnectionFactoryOptions = o =>
        {
            o.RequestedConnectionTimeout = TimeSpan.FromSeconds(
                RabbitMQConfigAssist.GetConnectionTimeout()
            );
        };

        RabbitMQ.ExchangeName = $"cap.{(string.IsNullOrWhiteSpace(prefix) ? "default" : prefix)}.topic";

        #endregion

        Redis = new CapRedisOptions();
    }
}

/// <summary>
/// 持久话配置
/// </summary>
public class Persistent
{
    /// <summary>
    /// SqlServer
    /// </summary>
    public SqlServerOptions? SqlServer { get; set; }

    /// <summary>
    /// PostgreSql
    /// </summary>
    public PostgreSqlOptions? PostgreSql { get; set; }

    /// <summary>
    /// MySql
    /// </summary>
    public MySqlOptions? MySql { get; set; }

    /// <summary>
    /// MongoDB
    /// </summary>
    public MongoDBOptions? MongoDB { get; set; }

    /// <summary>
    /// Sqlite
    /// </summary>
    public SqliteOptions? Sqlite { get; set; }
}