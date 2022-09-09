using DotNetCore.CAP;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Cap.Persistent;

public static class CapOptionsExtensions
{
    public static CapOptions UsePersistent(
        this CapOptions capOptions,
        PersistentType persistentType,
        string connection
    )
    {
        if (persistentType.In(PersistentType.SqlServer, PersistentType.MySql, PersistentType.PostgreSql,
                PersistentType.MongoDB))
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                throw new Exception("RabbitMQ persistent connection disallow empty");
            }
        }

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"CAP persistent target is {persistentType.ToString()}{(string.IsNullOrWhiteSpace(connection) ? "." : $", connection is \"{connection}\"")}."
                )
        );

        switch (persistentType)
        {
            case PersistentType.SqlServer:
                capOptions.UseSqlServer(options => { options.ConnectionString = connection; });

                return capOptions;

            case PersistentType.MySql:
                capOptions.UseMySql(options => { options.ConnectionString = connection; });

                return capOptions;

            case PersistentType.PostgreSql:
                capOptions.UsePostgreSql(options => { options.ConnectionString = connection; });

                return capOptions;

            case PersistentType.MongoDB:
                capOptions.UseMongoDB(options => { options.DatabaseConnection = connection; });

                return capOptions;

            case PersistentType.ImMemory:
                capOptions.UseInMemoryStorage();

                return capOptions;

            default:
                throw new Exception("unknown persistent mode");
        }
    }
}