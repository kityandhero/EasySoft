using EasySoft.UtilityTools.Standard.Entities.Implements;

namespace EasySoft.Core.EntityFramework.Interceptors;

/// <summary>
/// LogCommandInterceptor
/// </summary>
public class LogCommandInterceptor : DbCommandInterceptor
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// LogCommandInterceptor
    /// </summary>
    /// <param name="serviceProvider"></param>
    public LogCommandInterceptor(
        IServiceProvider serviceProvider
    )
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override DbCommand CommandCreated(CommandEndEventData eventData, DbCommand result)
    {
        ManipulateCommand(result);

        return result;
    }

    /// <inheritdoc />
    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result,
        CancellationToken cancellationToken = default
    )
    {
        ManipulateCommand(command);

        return new ValueTask<InterceptionResult<DbDataReader>>(result);
    }

    private void ManipulateCommand(DbCommand command)
    {
        if (!GeneralConfigAssist.GetRemoteSqlExecutionRecordSwitch())
        {
            return;
        }

        var sql = command.CommandText.Replace("\r\n", " ");

        if (string.IsNullOrWhiteSpace(sql))
        {
            return;
        }

        if (sql.ToLower().Contains("insert", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        var loggerFactory = _serviceProvider.GetRequiredService<ILoggerFactory>();
        var environment = _serviceProvider.GetRequiredService<IWebHostEnvironment>();
        var applicationChannel = _serviceProvider.GetRequiredService<IApplicationChannel>();

        var parametersCount = command.Parameters.Count;

        var nameValues = new List<object>();

        var sqlTimingParameters = new List<SqlTimingParameter>();

        for (var i = 0; i < parametersCount; i++)
        {
            var commandParameter = command.Parameters[i];

            sqlTimingParameters.Add(
                new SqlTimingParameter
                {
                    Name = commandParameter.ParameterName,
                    Value = commandParameter.Value?.ToString() ?? "",
                    DbType = commandParameter.Value?.GetType().ToDbType().ToString()
                }
            );

            nameValues.Add(
                new
                {
                    name = commandParameter.ParameterName,
                    value = commandParameter.Value ?? ""
                }
            );
        }

        var sqlAdjust = new StackExchange.Profiling.SqlFormatters.InlineFormatter().FormatSql(sql, sqlTimingParameters);

        if (environment.IsDevelopment())
        {
            var logger = loggerFactory.CreateLogger<object>();

            logger.LogAdvancePrompt(sql);
            logger.LogAdvancePrompt(sqlAdjust);
        }

        SqlLogInnerQueue.Enqueue(
            new SqlExecutionRecordExchange
            {
                ExecuteGuid = UniqueIdAssist.CreateUUID(),
                CommandString = JsonConvertAssist.SerializeObject(
                    new
                    {
                        sql,
                        paramList = nameValues,
                        sqlAdjust
                    }
                ),
                Channel = applicationChannel.GetChannel()
            }
        );
    }
}