using EasySoft.Core.LogServer.Core.DataTransferObjects;
using EasySoft.Core.LogServer.Core.Entities;
using EasySoft.Core.LogServer.Core.Services.Interfaces;

namespace EasySoft.Core.LogServer.Core.Services.Implements;

/// <summary>
/// SqlLogService
/// </summary>
public class SqlLogService : ISqlLogService
{
    private readonly IEventPublisher _eventPublisher;

    private readonly IRepository<SqlLog> _sqlLogRepository;

    /// <summary>
    /// UserService
    /// </summary>
    /// <param name="eventPublisher"></param>   
    /// <param name="sqlLogRepository"></param>
    public SqlLogService(
        IEventPublisher eventPublisher,
        IRepository<SqlLog> sqlLogRepository
    )
    {
        _eventPublisher = eventPublisher;

        _sqlLogRepository = sqlLogRepository;
    }

    /// <inheritdoc />
    public async Task<PageListResult<ISqlLogStore>> PageListAsync(
        SqlLogSearchDto sqlLogSearchDto
    )
    {
        var pageListResult = await _sqlLogRepository.PageListAsync(
            sqlLogSearchDto.PageNo,
            sqlLogSearchDto.PageSize
        );

        return new PageListResult<ISqlLogStore>(pageListResult.Code)
        {
            List = pageListResult.List.Cast<ISqlLogStore>().ToList(),
            PageIndex = pageListResult.PageIndex,
            PageSize = pageListResult.PageSize
        };
    }

    /// <inheritdoc />
    public async Task SaveAsync(ISqlLogTransitiveMessage sqlLogTransitiveMessage)
    {
        var log = new SqlLog
        {
            CommandString = sqlLogTransitiveMessage.CommandString,
            ExecuteType = sqlLogTransitiveMessage.ExecuteType,
            StackTraceSnippet = sqlLogTransitiveMessage.StackTraceSnippet,
            StartMilliseconds = sqlLogTransitiveMessage.StartMilliseconds,
            DurationMilliseconds = sqlLogTransitiveMessage.DurationMilliseconds,
            FirstFetchDurationMilliseconds = sqlLogTransitiveMessage.FirstFetchDurationMilliseconds,
            Errored = sqlLogTransitiveMessage.Errored,
            CollectMode = sqlLogTransitiveMessage.CollectMode,
            DatabaseChannel = sqlLogTransitiveMessage.DatabaseChannel,
            TriggerChannel = sqlLogTransitiveMessage.TriggerChannel,
            Status = 0,
            CreateBy = 0,
            CreateTime = DateTimeOffset.Now.DateTime,
            ModifyBy = 0,
            ModifyTime = DateTimeOffset.Now.DateTime
        };

        var resultAdd = await _sqlLogRepository.AddAsync(log);

        if (!resultAdd.Success)
        {
            throw new UnknownException(resultAdd.Message);
        }
    }
}