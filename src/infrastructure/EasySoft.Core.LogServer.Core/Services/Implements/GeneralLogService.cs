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
    public async Task SaveAsync(IGeneralLogTransitiveMessage generalLogTransitiveMessage)
    {
        var generalLog = new GeneralLog
        {
            Message = generalLogTransitiveMessage.Message,
            MessageType = generalLogTransitiveMessage.MessageType,
            Content = generalLogTransitiveMessage.Content,
            ContentType = generalLogTransitiveMessage.ContentType,
            Type = generalLogTransitiveMessage.Type,
            TriggerChannel = generalLogTransitiveMessage.TriggerChannel,
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