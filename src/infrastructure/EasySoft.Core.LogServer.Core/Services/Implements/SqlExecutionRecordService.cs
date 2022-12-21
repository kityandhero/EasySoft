using EasySoft.Core.LogServer.Core.DataTransferObjects;
using EasySoft.Core.LogServer.Core.Entities;
using EasySoft.Core.LogServer.Core.Services.Interfaces;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.LogServer.Core.Services.Implements;

/// <summary>
/// SqlExecutionRecordService
/// </summary>
public class SqlExecutionRecordService : ISqlExecutionRecordService
{
    private readonly IEventPublisher _eventPublisher;

    private readonly IRepository<SqlExecutionRecord> _sqlExecutionRecordRepository;

    /// <summary>
    /// UserService
    /// </summary>
    /// <param name="eventPublisher"></param>   
    /// <param name="sqlExecutionRecordRepository"></param>
    public SqlExecutionRecordService(
        IEventPublisher eventPublisher,
        IRepository<SqlExecutionRecord> sqlExecutionRecordRepository
    )
    {
        _eventPublisher = eventPublisher;

        _sqlExecutionRecordRepository = sqlExecutionRecordRepository;
    }

    /// <inheritdoc />
    public async Task<PageListResult<SqlExecutionRecord>> PageListAsync(
        SqlExecutionRecordSearchDto sqlExecutionRecordSearchDto
    )
    {
        return await _sqlExecutionRecordRepository.PageListAsync(
            sqlExecutionRecordSearchDto.PageNo,
            sqlExecutionRecordSearchDto.PageSize
        );
    }

    /// <inheritdoc />
    public async Task SaveAsync(ISqlExecutionRecordExchange sqlExecutionRecord)
    {
        var log = new SqlExecutionRecord
        {
            CommandString = sqlExecutionRecord.CommandString,
            ExecuteType = sqlExecutionRecord.ExecuteType,
            StackTraceSnippet = sqlExecutionRecord.StackTraceSnippet,
            StartMilliseconds = sqlExecutionRecord.StartMilliseconds,
            DurationMilliseconds = sqlExecutionRecord.DurationMilliseconds,
            FirstFetchDurationMilliseconds = sqlExecutionRecord.FirstFetchDurationMilliseconds,
            Errored = sqlExecutionRecord.Errored,
            CollectMode = sqlExecutionRecord.CollectMode,
            DatabaseChannel = sqlExecutionRecord.DatabaseChannel,
            Channel = sqlExecutionRecord.Channel,
            Ip = sqlExecutionRecord.Ip,
            Status = 0,
            CreateBy = sqlExecutionRecord.CreateBy,
            CreateTime = sqlExecutionRecord.CreateTime,
            ModifyBy = sqlExecutionRecord.CreateBy,
            ModifyTime = sqlExecutionRecord.CreateTime
        };

        var resultAdd = await _sqlExecutionRecordRepository.AddAsync(log);

        if (!resultAdd.Success) throw new UnknownException(resultAdd.Message);
    }
}