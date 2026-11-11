using EasySoft.Core.LogServer.Core.DataTransferObjects;
using EasySoft.Core.LogServer.Core.Entities;
using EasySoft.Core.LogServer.Core.Services.Interfaces;

namespace EasySoft.Core.LogServer.Core.Services.Implements;

/// <summary>
/// SecurityService
/// </summary>
public class ErrorLogService : IErrorLogService
{
    private readonly IEventPublisher _eventPublisher;

    private readonly IRepository<ErrorLog> _errorLogRepository;

    /// <summary>
    /// UserService
    /// </summary>
    /// <param name="eventPublisher"></param>
    /// <param name="errorLogRepository"></param>
    public ErrorLogService(
        IEventPublisher eventPublisher,
        IRepository<ErrorLog> errorLogRepository
    )
    {
        _eventPublisher = eventPublisher;

        _errorLogRepository = errorLogRepository;
    }

    /// <inheritdoc />
    public async Task<PageListResult<IErrorLogStore>> PageListAsync(
        ErrorLogSearchDto errorLogSearchDto
    )
    {
        var pageListResult = await _errorLogRepository.PageListAsync(
            errorLogSearchDto.PageNo,
            errorLogSearchDto.PageSize
        );

        return new PageListResult<IErrorLogStore>(pageListResult.Code)
        {
            List = pageListResult.List.Cast<IErrorLogStore>().ToList(),
            PageIndex = pageListResult.PageIndex,
            PageSize = pageListResult.PageSize
        };
    }

    /// <inheritdoc />
    public async Task SaveAsync(IErrorLogTransitiveMessage errorLogTransitiveMessage)
    {
        var errorLog = new ErrorLog
        {
            OperatorId = errorLogTransitiveMessage.OperatorId,
            Url = errorLogTransitiveMessage.Url,
            Message = errorLogTransitiveMessage.Message,
            StackTrace = errorLogTransitiveMessage.StackTrace,
            Source = errorLogTransitiveMessage.Source,
            Scene = errorLogTransitiveMessage.Scene,
            Type = errorLogTransitiveMessage.Type,
            Degree = errorLogTransitiveMessage.Degree,
            Header = errorLogTransitiveMessage.Header,
            UrlParams = errorLogTransitiveMessage.UrlParams,
            PayloadParams = errorLogTransitiveMessage.PayloadParams,
            FormParams = errorLogTransitiveMessage.FormParams,
            Host = errorLogTransitiveMessage.Host,
            Port = errorLogTransitiveMessage.Port,
            CustomLog = errorLogTransitiveMessage.CustomLog,
            CustomData = errorLogTransitiveMessage.CustomData,
            CustomDataType = errorLogTransitiveMessage.CustomDataType,
            ExceptionTypeName = errorLogTransitiveMessage.ExceptionTypeName,
            ExceptionTypeFullName = errorLogTransitiveMessage.ExceptionTypeFullName,
            Channel = ChannelAssist.GetCurrentChannel().ToValue(),
            Status = 0,
            CreateBy = errorLogTransitiveMessage.OperatorId,
            CreateTime = DateTimeOffset.Now.DateTime,
            ModifyBy = errorLogTransitiveMessage.OperatorId,
            ModifyTime = DateTimeOffset.Now.DateTime
        };

        var resultAdd = await _errorLogRepository.AddAsync(errorLog);

        if (!resultAdd.Success)
        {
            throw new UnknownException(resultAdd.Message);
        }
    }
}