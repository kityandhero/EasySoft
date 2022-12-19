using EasySoft.Core.LogServer.Core.DataTransferObjects;
using EasySoft.Core.LogServer.Core.Entities;
using EasySoft.Core.LogServer.Core.Services.Interfaces;

namespace EasySoft.Core.LogServer.Core.Services.Implementations;

/// <inheritdoc />
public class GeneralLogService : IGeneralLogService
{
    private readonly IEventPublisher _eventPublisher;

    private readonly IRepository<GeneralLog> _generalLogRepository;

    /// <summary>
    /// UserService
    /// </summary>
    /// <param name="eventPublisher"></param>
    /// <param name="generalLogRepository"></param>
    public GeneralLogService(
        IEventPublisher eventPublisher,
        IRepository<GeneralLog> generalLogRepository
    )
    {
        _eventPublisher = eventPublisher;

        _generalLogRepository = generalLogRepository;
    }

    /// <inheritdoc />
    public async Task<PageListResult<GeneralLog>> PageListAsync(GeneralLogSearchDto blogSearchDto)
    {
        return await _generalLogRepository.PageListAsync(
            blogSearchDto.PageNo,
            blogSearchDto.PageSize
        );
    }

    /// <inheritdoc />
    public async Task SaveAsync(IGeneralLogExchange generalLogExchange)
    {
        var generalLog = new GeneralLog
        {
            Message = generalLogExchange.Message,
            MessageType = generalLogExchange.MessageType,
            Content = generalLogExchange.Content,
            ContentType = generalLogExchange.ContentType,
            Type = generalLogExchange.Type,
            Channel = generalLogExchange.Channel,
            Ip = generalLogExchange.Ip,
            Status = 0,
            CreateBy = generalLogExchange.CreateBy,
            CreateTime = generalLogExchange.CreateTime,
            ModifyBy = generalLogExchange.CreateBy,
            ModifyTime = generalLogExchange.CreateTime
        };

        var resultAdd = await _generalLogRepository.AddAsync(generalLog);

        if (!resultAdd.Success) throw new UnknownException(resultAdd.Message);
    }
}