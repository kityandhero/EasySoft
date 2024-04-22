using EasySoft.Core.LogServer.Core.DataTransferObjects;
using EasySoft.Core.LogServer.Core.Entities;
using EasySoft.Core.LogServer.Core.Services.Interfaces;

namespace EasySoft.Core.LogServer.Core.Services.Implements;

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
    public async Task<PageListResult<IGeneralLogStore>> PageListAsync(
        GeneralLogSearchDto generalLogSearchDto
    )
    {
        var pageListResult = await _generalLogRepository.PageListAsync(
            generalLogSearchDto.PageNo,
            generalLogSearchDto.PageSize
        );

        return new PageListResult<IGeneralLogStore>(pageListResult.Code)
        {
            List = pageListResult.List.Cast<IGeneralLogStore>().ToList(),
            PageIndex = pageListResult.PageIndex,
            PageSize = pageListResult.PageSize
        };
    }

    /// <inheritdoc />
    public async Task SaveAsync(IGeneralLogMessage generalLogMessage)
    {
        var generalLog = new GeneralLog
        {
            Message = generalLogMessage.Message,
            MessageType = generalLogMessage.MessageType,
            Content = generalLogMessage.Content,
            ContentType = generalLogMessage.ContentType,
            Type = generalLogMessage.Type,
            TriggerChannel = generalLogMessage.TriggerChannel,
            Channel = ChannelAssist.GetCurrentChannel().ToValue(),
            Status = 0,
            CreateBy = 0,
            CreateTime = DateTimeOffset.Now.DateTime,
            ModifyBy = 0,
            ModifyTime = DateTimeOffset.Now.DateTime
        };

        var resultAdd = await _generalLogRepository.AddAsync(generalLog);

        if (!resultAdd.Success)
        {
            throw new UnknownException(resultAdd.Message);
        }
    }
}