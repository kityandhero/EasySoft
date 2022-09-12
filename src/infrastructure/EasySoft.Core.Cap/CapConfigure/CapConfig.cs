using DotNetCore.CAP;
using DotNetCore.CAP.Dashboard.NodeDiscovery;
using DotNetCore.CAP.MongoDB;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Cap.CapConfigure;

public class CapConfig
{
    public string Prefix { get; set; }

    public Transport Transport { get; set; }

    public Persistent Persistent { get; set; }

    public DashboardOptions DashboardOptions { get; set; }

    public DiscoveryOptions DiscoveryOptions { get; set; }

    public CapConfig()
    {
        Prefix = GeneralConfigAssist.GetCapPrefix();

        Transport = new Transport();

        Persistent = new Persistent();

        DashboardOptions = new DashboardOptions();

        DiscoveryOptions = new DiscoveryOptions();
    }
}

public class Transport
{
    public AmazonSQSOptions? AmazonSQS { get; set; }

    public KafkaOptions? Kafka { get; set; }

    public PulsarOptions? Pulsar { get; set; }

    public AzureServiceBusOptions? AzureServiceBus { get; set; }

    public NATSOptions? NATS { get; set; }

    public RabbitMQOptions? RabbitMQ { get; set; }

    public CapRedisOptions? Redis { get; set; }

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

public class Persistent
{
    public SqlServerOptions? SqlServer { get; set; }

    public PostgreSqlOptions? PostgreSql { get; set; }

    public MySqlOptions? MySql { get; set; }

    public MongoDBOptions? MongoDB { get; set; }

    public SqliteOptions? Sqlite { get; set; }

    public Persistent()
    {
        SqlServer = new SqlServerOptions();

        PostgreSql = new PostgreSqlOptions();

        MySql = new MySqlOptions();

        MongoDB = new MongoDBOptions();

        Sqlite = new SqliteOptions();
    }
}