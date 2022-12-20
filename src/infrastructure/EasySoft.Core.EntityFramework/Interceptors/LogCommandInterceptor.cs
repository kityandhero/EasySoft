namespace EasySoft.Core.EntityFramework.Interceptors;

/// <summary>
/// LogCommandInterceptor
/// </summary>
public class LogCommandInterceptor : DbCommandInterceptor
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IApplicationChannel _applicationChannel;
    private readonly IWebHostEnvironment _environment;

    /// <summary>
    /// LogCommandInterceptor
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="applicationChannel"></param>
    /// <param name="environment"></param>
    public LogCommandInterceptor(
        ILoggerFactory loggerFactory,
        IWebHostEnvironment environment,
        IApplicationChannel applicationChannel
    )
    {
        _loggerFactory = loggerFactory;
        _applicationChannel = applicationChannel;
        _environment = environment;
    }

    /// <inheritdoc />
    public override InterceptionResult<DbDataReader> ReaderExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result
    )
    {
        ManipulateCommand(command);

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
        if (!GeneralConfigAssist.GetRemoteSqlExecutionRecordSwitch()) return;

        var sql = command.CommandText;

        var sqlAdjust = sql;

        var parametersCount = command.Parameters.Count;

        var nameValues = new List<object>();

        var sqlTimingParameters = new List<SqlTimingParameter>();

        for (var i = 0; i < parametersCount; i++)
        {
            var commandParameter = command.Parameters[i];

            sqlTimingParameters.Add(new SqlTimingParameter
            {
                Name = commandParameter.ParameterName,
                Value = commandParameter.Value?.ToString() ?? "",
                DbType = commandParameter.Value?.GetType().ToDbType().ToString()
            });

            nameValues.Add(new
            {
                name = commandParameter.ParameterName,
                value = commandParameter.Value ?? ""
            });

            sqlAdjust = sqlAdjust.Replace(commandParameter.ParameterName, commandParameter.Value?.ToString() ?? "");
        }

        if (_environment.IsDevelopment())
        {
            var logger = _loggerFactory.CreateLogger<object>();

            logger.LogAdvancePrompt(sql);
            logger.LogAdvancePrompt(
                new StackExchange.Profiling.SqlFormatters.InlineFormatter().FormatSql(sql, sqlTimingParameters)
            );
        }

        SqlLogInnerQueue.Enqueue(
            new SqlExecutionRecordExchange
            {
                ExecuteGuid = UniqueIdAssist.CreateUUID(),
                CommandString = JsonConvertAssist.Serialize(new
                {
                    sql,
                    paramList = nameValues,
                    sqlAdjust
                }),
                Channel = _applicationChannel.GetChannel()
            }
        );
    }
}