using EasySoft.Core.Cap.Assists;
using EasySoft.Core.Cap.CapConfigure;
using EasySoft.Core.Config.ConfigAssist;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Savorboard.CAP.InMemoryMessageQueue;

namespace EasySoft.Core.Cap.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceCap(this WebApplicationBuilder builder)
    {
        if (!GeneralConfigAssist.GetCapSwitch())
        {
            return builder;
        }

        var capConfig = CapAssist.GetConfig();

        builder.Services.AddCap(capOptions =>
        {
            var transportType = Enum.Parse<TransportType>(capConfig.Transport.Type);

            switch (transportType)
            {
                case TransportType.InMemoryMessageQueue:
                    capOptions.UseInMemoryMessageQueue();
                    break;

                case TransportType.RabbitMQ:

                    if (capConfig.Transport.RabbitMQ == null)
                    {
                        throw new Exception(
                            "Cap transport mode need config,use CapAssist.GetConfig().Transport.RabbitMQ to set it"
                        );
                    }

                    capOptions.UseRabbitMQ(o =>
                    {
                        o.HostName = capConfig.Transport.RabbitMQ.HostName;
                        o.UserName = capConfig.Transport.RabbitMQ.UserName;
                        o.Password = capConfig.Transport.RabbitMQ.Password;
                        o.Port = capConfig.Transport.RabbitMQ.Port;
                        o.ExchangeName = capConfig.Transport.RabbitMQ.ExchangeName;
                        o.VirtualHost = capConfig.Transport.RabbitMQ.VirtualHost;
                        o.CustomHeaders = capConfig.Transport.RabbitMQ.CustomHeaders;
                        o.ConnectionFactoryOptions = capConfig.Transport.RabbitMQ.ConnectionFactoryOptions;
                        o.QueueArguments = capConfig.Transport.RabbitMQ.QueueArguments;
                    });
                    break;

                default:
                    throw new Exception($"cap {capConfig.Transport.Type} not support");
            }

            var persistentType = Enum.Parse<PersistentType>(capConfig.Persistent.Type);

            switch (persistentType)
            {
                case PersistentType.ImMemory:
                    capOptions.UseInMemoryStorage();
                    break;

                case PersistentType.SqlServer:
                    if (capConfig.Persistent.SqlServer == null)
                    {
                        throw new Exception(
                            "Cap persistent mode SqlServer need config,use CapAssist.GetConfig().Persistent.RabbitMQ to set it"
                        );
                    }

                    capOptions.UseSqlServer(o =>
                    {
                        o.ConnectionString = capConfig.Persistent.SqlServer.ConnectionString;
                        o.Schema = capConfig.Persistent.SqlServer.Schema;
                    });
                    break;

                default:
                    throw new Exception($"cap {capConfig.Persistent.Type} not support");
            }
        });

        return builder;
    }
}