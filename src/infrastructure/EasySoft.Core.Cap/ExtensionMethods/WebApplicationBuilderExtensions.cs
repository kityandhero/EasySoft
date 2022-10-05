using DotNetCore.CAP;
using DotNetCore.CAP.Dashboard.NodeDiscovery;
using DotNetCore.CAP.MongoDB;
using EasySoft.Core.Cap.Assists;
using EasySoft.Core.Config.Cap;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Savorboard.CAP.InMemoryMessageQueue;

namespace EasySoft.Core.Cap.ExtensionMethods;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// AddAdvanceCap
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static WebApplicationBuilder AddAdvanceCap(this WebApplicationBuilder builder)
    {
        var capSwitch = GeneralConfigAssist.GetCapSwitch();

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetMessage(
                    $"CapSwitch: {(capSwitch == 1.ToString() ? "enable" : capSwitch == 0.ToString() ? "disable" : capSwitch)}"
                )
                .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
        );

        if (!GeneralConfigAssist.CheckCapSwitch())
        {
            return builder;
        }

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetMessage(
                    $"CapDashboardSwitch: {(GeneralConfigAssist.GetCapDashboardSwitch() ? "enable" : "disable")}"
                )
                .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
        );

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetMessage(
                    $"CapDiscoverySwitch: {(GeneralConfigAssist.GetCapDiscoverySwitch() ? "enable" : "disable")}"
                )
                .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
        );

        var capConfig = CapAssist.GetConfig();

        builder.Services.AddCap(capOptions =>
        {
            if (!string.IsNullOrWhiteSpace(capConfig.Prefix))
            {
                capOptions.GroupNamePrefix = capConfig.Prefix;
            }

            var transportType = Enum.Parse<TransportType>(GeneralConfigAssist.GetCapTransportType());

            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetMessage(
                        $"CapTransportType: {GeneralConfigAssist.GetCapTransportType()}"
                    )
                    .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
            );

            switch (transportType)
            {
                case TransportType.InMemoryMessageQueue:
                    capOptions.UseInMemoryMessageQueue();

                    break;

                case TransportType.RabbitMQ:

                    var configFileHostName = RabbitMQConfigAssist.GetHostName();
                    var configFileUserName = RabbitMQConfigAssist.GetUserName();
                    var configFilePassword = RabbitMQConfigAssist.GetPassword();
                    var configFileVirtualHost = RabbitMQConfigAssist.GetVirtualHost();

                    if (capConfig.Transport.RabbitMQ == null && (string.IsNullOrWhiteSpace(configFileHostName) ||
                                                                 string.IsNullOrWhiteSpace(configFileUserName) ||
                                                                 string.IsNullOrWhiteSpace(configFilePassword) ||
                                                                 string.IsNullOrWhiteSpace(configFileVirtualHost)))
                    {
                        throw new Exception(
                            $"Cap transport type is RabbitMQ, it need config,use config file in {(RabbitMQConfigAssist.GetConfigFileInfo())} or use CapAssist.GetConfig().Transport.RabbitMQ to set it"
                        );
                    }

                    if (capConfig.Transport.RabbitMQ == null)
                    {
                        capConfig.Transport.RabbitMQ = new RabbitMQOptions();
                    }

                    capOptions.UseRabbitMQ(o =>
                    {
                        var prefix = GeneralConfigAssist.GetCapPrefix().Remove(" ").Trim().ToLower();

                        o.HostName = string.IsNullOrWhiteSpace(capConfig.Transport.RabbitMQ.HostName)
                            ? RabbitMQConfigAssist.GetHostName()
                            : capConfig.Transport.RabbitMQ.HostName;

                        o.UserName = string.IsNullOrWhiteSpace(capConfig.Transport.RabbitMQ.UserName)
                            ? RabbitMQConfigAssist.GetUserName()
                            : capConfig.Transport.RabbitMQ.UserName;

                        o.Password = string.IsNullOrWhiteSpace(capConfig.Transport.RabbitMQ.Password)
                            ? RabbitMQConfigAssist.GetPassword()
                            : capConfig.Transport.RabbitMQ.Password;

                        o.Port = capConfig.Transport.RabbitMQ.Port;

                        o.ExchangeName = string.IsNullOrWhiteSpace(capConfig.Transport.RabbitMQ.ExchangeName)
                            ? $"cap.{(string.IsNullOrWhiteSpace(prefix) ? "default" : prefix)}.topic"
                            : capConfig.Transport.RabbitMQ.ExchangeName;

                        o.VirtualHost = string.IsNullOrWhiteSpace(capConfig.Transport.RabbitMQ.VirtualHost)
                            ? RabbitMQConfigAssist.GetVirtualHost()
                            : capConfig.Transport.RabbitMQ.VirtualHost;

                        o.CustomHeaders = capConfig.Transport.RabbitMQ.CustomHeaders;

                        if (capConfig.Transport.RabbitMQ.ConnectionFactoryOptions == null)
                        {
                            o.ConnectionFactoryOptions = one =>
                            {
                                one.RequestedConnectionTimeout = TimeSpan.FromSeconds(
                                    RabbitMQConfigAssist.GetConnectionTimeout()
                                );
                            };
                        }
                        else
                        {
                            o.ConnectionFactoryOptions = capConfig.Transport.RabbitMQ.ConnectionFactoryOptions;
                        }

                        o.QueueArguments = capConfig.Transport.RabbitMQ.QueueArguments;
                    });
                    break;

                case TransportType.AmazonSQS:

                    if (capConfig.Transport.AmazonSQS == null)
                    {
                        throw new Exception(
                            "Cap transport type is AmazonSQS, it need config,use CapAssist.GetConfig().Transport.AmazonSQS to set it"
                        );
                    }

                    capOptions.UseAmazonSQS(o =>
                    {
                        o.SQSServiceUrl = capConfig.Transport.AmazonSQS.SQSServiceUrl;
                        o.SNSServiceUrl = capConfig.Transport.AmazonSQS.SNSServiceUrl;
                        o.Credentials = capConfig.Transport.AmazonSQS.Credentials;
                        o.Region = capConfig.Transport.AmazonSQS.Region;
                    });
                    break;

                case TransportType.Kafka:

                    if (capConfig.Transport.Kafka == null)
                    {
                        throw new Exception(
                            "Cap transport type is Kafka, it need config,use CapAssist.GetConfig().Transport.Kafka to set it"
                        );
                    }

                    capOptions.UseKafka(o =>
                    {
                        o.Servers = capConfig.Transport.Kafka.Servers;
                        o.ConnectionPoolSize = capConfig.Transport.Kafka.ConnectionPoolSize;
                        o.CustomHeaders = capConfig.Transport.Kafka.CustomHeaders;
                        o.RetriableErrorCodes = capConfig.Transport.Kafka.RetriableErrorCodes;
                    });
                    break;

                case TransportType.Pulsar:

                    if (capConfig.Transport.Pulsar == null)
                    {
                        throw new Exception(
                            "Cap transport type is Pulsar, it need config,use CapAssist.GetConfig().Transport.Pulsar to set it"
                        );
                    }

                    capOptions.UsePulsar(o =>
                    {
                        o.ServiceUrl = capConfig.Transport.Pulsar.ServiceUrl;
                        o.EnableClientLog = capConfig.Transport.Pulsar.EnableClientLog;
                        o.TlsOptions = capConfig.Transport.Pulsar.TlsOptions;
                    });
                    break;

                case TransportType.AzureServiceBus:

                    if (capConfig.Transport.AzureServiceBus == null)
                    {
                        throw new Exception(
                            "Cap transport type is AzureServiceBus, it need config,use CapAssist.GetConfig().Transport.AzureServiceBus to set it"
                        );
                    }

                    capOptions.UseAzureServiceBus(o =>
                    {
                        o.ConnectionString = capConfig.Transport.AzureServiceBus.ConnectionString;
                        o.CustomHeaders = capConfig.Transport.AzureServiceBus.CustomHeaders;
                        o.TopicPath = capConfig.Transport.AzureServiceBus.TopicPath;
                        o.EnableSessions = capConfig.Transport.AzureServiceBus.EnableSessions;
                        o.ManagementTokenProvider = capConfig.Transport.AzureServiceBus.ManagementTokenProvider;
                    });
                    break;

                case TransportType.NATS:

                    if (capConfig.Transport.NATS == null)
                    {
                        throw new Exception(
                            "Cap transport type is NATS, it need config,use CapAssist.GetConfig().Transport.NATS to set it"
                        );
                    }

                    capOptions.UseNATS(o =>
                    {
                        o.Servers = capConfig.Transport.NATS.Servers;
                        o.ConnectionPoolSize = capConfig.Transport.NATS.ConnectionPoolSize;
                        o.Options = capConfig.Transport.NATS.Options;
                        o.NormalizeStreamName = capConfig.Transport.NATS.NormalizeStreamName;
                        o.StreamOptions = capConfig.Transport.NATS.StreamOptions;
                    });
                    break;

                case TransportType.Redis:

                    if (capConfig.Transport.Redis == null)
                    {
                        throw new Exception(
                            "Cap transport type is Redis, it need config,use CapAssist.GetConfig().Transport.Redis to set it"
                        );
                    }

                    capOptions.UseRedis(o =>
                    {
                        o.Configuration = capConfig.Transport.Redis.Configuration;
                        o.StreamEntriesCount = capConfig.Transport.Redis.StreamEntriesCount;
                        o.ConnectionPoolSize = capConfig.Transport.Redis.ConnectionPoolSize;
                    });
                    break;

                default:
                    throw new Exception($"cap transport type {transportType.ToString()} not support");
            }

            var persistentType = Enum.Parse<PersistentType>(GeneralConfigAssist.GetCapPersistentType());

            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetMessage(
                        $"CapPersistentType: {GeneralConfigAssist.GetCapPersistentType()}"
                    )
                    .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
            );

            var capPersistentConnection = GeneralConfigAssist.GetCapPersistentConnection();

            switch (persistentType)
            {
                case PersistentType.ImMemory:
                    capOptions.UseInMemoryStorage();
                    break;

                case PersistentType.SqlServer:
                    if (!string.IsNullOrWhiteSpace(capPersistentConnection) && capConfig.Persistent.SqlServer == null)
                    {
                        capConfig.Persistent.SqlServer = new SqlServerOptions
                        {
                            ConnectionString = capPersistentConnection
                        };
                    }

                    if (capConfig.Persistent.SqlServer == null)
                    {
                        throw new Exception(
                            "Cap persistent type is SqlServer, it need config,use CapAssist.GetConfig().Persistent.SqlServer to set it"
                        );
                    }

                    capOptions.UseSqlServer(o =>
                    {
                        o.ConnectionString = capConfig.Persistent.SqlServer.ConnectionString;
                        o.Schema = capConfig.Persistent.SqlServer.Schema;
                    });
                    break;

                case PersistentType.MySql:
                    if (!string.IsNullOrWhiteSpace(capPersistentConnection) && capConfig.Persistent.MySql == null)
                    {
                        capConfig.Persistent.MySql = new MySqlOptions
                        {
                            ConnectionString = capPersistentConnection
                        };
                    }

                    if (capConfig.Persistent.MySql == null)
                    {
                        throw new Exception(
                            "Cap persistent type is MySql, it need config,use CapAssist.GetConfig().Persistent.MySql to set it"
                        );
                    }

                    capOptions.UseMySql(o =>
                    {
                        o.ConnectionString = capConfig.Persistent.MySql.ConnectionString;
                        o.TableNamePrefix = capConfig.Persistent.MySql.TableNamePrefix;
                    });
                    break;

                case PersistentType.PostgreSql:
                    if (!string.IsNullOrWhiteSpace(capPersistentConnection) && capConfig.Persistent.PostgreSql == null)
                    {
                        capConfig.Persistent.PostgreSql = new PostgreSqlOptions
                        {
                            ConnectionString = capPersistentConnection
                        };
                    }

                    if (capConfig.Persistent.PostgreSql == null)
                    {
                        throw new Exception(
                            "Cap persistent type is PostgreSql, it need config,use CapAssist.GetConfig().Persistent.PostgreSql to set it"
                        );
                    }

                    capOptions.UsePostgreSql(o =>
                    {
                        o.ConnectionString = capConfig.Persistent.PostgreSql.ConnectionString;
                        o.Schema = capConfig.Persistent.PostgreSql.Schema;
                    });
                    break;

                case PersistentType.MongoDB:
                    if (!string.IsNullOrWhiteSpace(capPersistentConnection) && capConfig.Persistent.MongoDB == null)
                    {
                        capConfig.Persistent.MongoDB = new MongoDBOptions()
                        {
                            DatabaseConnection = capPersistentConnection
                        };
                    }

                    if (capConfig.Persistent.MongoDB == null)
                    {
                        throw new Exception(
                            "Cap persistent type is MongoDB, it need config,use CapAssist.GetConfig().Persistent.MongoDB to set it"
                        );
                    }

                    capOptions.UseMongoDB(o =>
                    {
                        o.DatabaseConnection = capConfig.Persistent.MongoDB.DatabaseConnection;
                        o.DatabaseName = capConfig.Persistent.MongoDB.DatabaseName;
                        o.ReceivedCollection = capConfig.Persistent.MongoDB.ReceivedCollection;
                        o.PublishedCollection = capConfig.Persistent.MongoDB.PublishedCollection;
                    });
                    break;

                case PersistentType.Sqlite:
                    if (!string.IsNullOrWhiteSpace(capPersistentConnection) && capConfig.Persistent.Sqlite == null)
                    {
                        capConfig.Persistent.Sqlite = new SqliteOptions()
                        {
                            ConnectionString = capPersistentConnection
                        };
                    }

                    if (capConfig.Persistent.Sqlite == null)
                    {
                        throw new Exception(
                            "Cap persistent type is Sqlite, it need config,use CapAssist.GetConfig().Persistent.Sqlite to set it"
                        );
                    }

                    capOptions.UseSqlite(o =>
                    {
                        o.ConnectionString = capConfig.Persistent.Sqlite.ConnectionString;
                        o.TableNamePrefix = capConfig.Persistent.Sqlite.TableNamePrefix;
                    });
                    break;

                default:
                    throw new Exception($"cap persistent type {persistentType.ToString()} not support");
            }

            if (GeneralConfigAssist.GetCapDashboardSwitch())
            {
                capOptions.UseDashboard(o =>
                {
                    o.UseChallengeOnAuth = capConfig.DashboardOptions.UseChallengeOnAuth;
                    o.UseAuth = capConfig.DashboardOptions.UseAuth;
                    o.DefaultChallengeScheme = capConfig.DashboardOptions.DefaultChallengeScheme;
                    o.PathMatch = capConfig.DashboardOptions.PathMatch;
                    o.AuthorizationPolicy = capConfig.DashboardOptions.AuthorizationPolicy;
                    o.PathBase = capConfig.DashboardOptions.PathBase;
                    o.DefaultAuthenticationScheme = capConfig.DashboardOptions.DefaultAuthenticationScheme;
                    o.StatsPollingInterval = capConfig.DashboardOptions.StatsPollingInterval;
                });
            }

            if (GeneralConfigAssist.GetCapDiscoverySwitch())
            {
                capOptions.UseDiscovery(o =>
                {
                    o.DiscoveryServerHostName = capConfig.DiscoveryOptions.DiscoveryServerHostName;
                    o.DiscoveryServerPort = capConfig.DiscoveryOptions.DiscoveryServerPort;
                    o.CurrentNodeHostName = capConfig.DiscoveryOptions.CurrentNodeHostName;
                    o.CurrentNodePort = capConfig.DiscoveryOptions.CurrentNodePort;
                    o.NodeId = capConfig.DiscoveryOptions.NodeId;
                    o.NodeName = capConfig.DiscoveryOptions.NodeName;
                    o.Scheme = capConfig.DiscoveryOptions.Scheme;
                    o.MatchPath = capConfig.DiscoveryOptions.MatchPath;
                    o.CustomTags = capConfig.DiscoveryOptions.CustomTags;
                });
            }
        });

        var startupMessage = new StartupMessage()
            .SetLevel(LogLevel.Information)
            .SetMessage(
                $"Cap transport mode is {GeneralConfigAssist.GetCapTransportType()}, persistent mode is {GeneralConfigAssist.GetCapPersistentType()}, if you need to customize it, use CapAssist.GetConfig() to set."
            );

        if (GeneralConfigAssist.GetCapDashboardSwitch())
        {
            var pathBase = string.IsNullOrWhiteSpace(capConfig.DashboardOptions.PathBase)
                ? "/cap"
                : capConfig.DashboardOptions.PathBase;

            startupMessage.SetExtraNewLie(true)
                .SetExtra(
                    $"Cap dashboard is {pathBase}, you can access {(!FlagAssist.StartupUrls.Any() ? $"https://[host]:[port]{capConfig.DashboardOptions.PathBase}" : FlagAssist.StartupUrls.Select(o => $"{o}{pathBase}").Join(" "))} to visit it."
                );

            StartupDescriptionMessageAssist.Add(
                startupMessage
            );
        }

        if (GeneralConfigAssist.GetCapDiscoverySwitch())
        {
            var nodeId = capConfig.DiscoveryOptions.NodeId;
            var nodeName = capConfig.DiscoveryOptions.NodeName;

            nodeId = string.IsNullOrWhiteSpace(nodeId) ? "not set" : nodeId;
            nodeName = string.IsNullOrWhiteSpace(nodeName) ? "not set" : nodeName;

            StartupDescriptionMessageAssist.Add(
                new StartupMessage()
                    .SetMessage(
                        $"Cap discovery is open, discoveryServerHostName is {capConfig.DiscoveryOptions.DiscoveryServerHostName}, discoveryServerPort is {capConfig.DiscoveryOptions.DiscoveryServerPort}, nodeId is {nodeId}, nodeName is {nodeName}."
                    )
            );
        }

        return builder;
    }
}