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
    public async Task SaveAsync(IErrorLogMessage errorLogMessage)
    {
        var errorLog = new ErrorLog
        {
            OperatorId = errorLogMessage.OperatorId,
            Url = errorLogMessage.Url,
            Message = errorLogMessage.Message,
            StackTrace = errorLogMessage.StackTrace,
            Source = errorLogMessage.Source,
            Scene = errorLogMessage.Scene,
            Type = errorLogMessage.Type,
            Degree = errorLogMessage.Degree,
            Header = errorLogMessage.Header,
            UrlParams = errorLogMessage.UrlParams,
            PayloadParams = errorLogMessage.PayloadParams,
            FormParams = errorLogMessage.FormParams,
            Host = errorLogMessage.Host,
            Port = errorLogMessage.Port,
            CustomLog = errorLogMessage.CustomLog,
            CustomData = errorLogMessage.CustomData,
            CustomDataType = errorLogMessage.CustomDataType,
            ExceptionTypeName = errorLogMessage.ExceptionTypeName,
            ExceptionTypeFullName = errorLogMessage.ExceptionTypeFullName,
            Channel = ChannelAssist.GetCurrentChannel().ToValue(),
            Status = 0,
            CreateBy = errorLogMessage.OperatorId,
            CreateTime = DateTimeOffset.Now.DateTime,
            ModifyBy = errorLogMessage.OperatorId,
            ModifyTime = DateTimeOffset.Now.DateTime
        };

        var resultAdd = await _errorLogRepository.AddAsync(errorLog);

        if (!resultAdd.Success)
        {
            throw new UnknownException(resultAdd.Message);
        }
    }
}