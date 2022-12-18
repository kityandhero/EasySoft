using EasySoft.Core.LogServer.Core.DataTransferObjects;
using EasySoft.Core.LogServer.Core.Entities;
using EasySoft.Core.LogServer.Core.Services.Interfaces;

namespace EasySoft.Core.LogServer.Core.Services.Implementations;

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
    public async Task<PageListResult<ErrorLog>> PageListAsync(ErrorLogSearchDto blogSearchDto)
    {
        return await _errorLogRepository.PageListAsync(
            blogSearchDto.PageNo,
            blogSearchDto.PageSize
        );
    }

    /// <inheritdoc />
    public async Task SaveAsync(ErrorLogExchange errorLogExchange)
    {
        var errorLog = new ErrorLog
        {
            UserId = errorLogExchange.UserId,
            Url = errorLogExchange.Url,
            Message = errorLogExchange.Message,
            StackTrace = errorLogExchange.StackTrace,
            Source = errorLogExchange.Source,
            Scene = errorLogExchange.Scene,
            Type = errorLogExchange.Type,
            Degree = errorLogExchange.Degree,
            Header = errorLogExchange.Header,
            UrlParams = errorLogExchange.UrlParams,
            PayloadParams = errorLogExchange.PayloadParams,
            FormParams = errorLogExchange.FormParams,
            Host = errorLogExchange.Host,
            Port = errorLogExchange.Port,
            CustomLog = errorLogExchange.CustomLog,
            CustomData = errorLogExchange.CustomData,
            CustomDataType = errorLogExchange.CustomDataType,
            ExceptionTypeName = errorLogExchange.ExceptionTypeName,
            ExceptionTypeFullName = errorLogExchange.ExceptionTypeFullName,
            Channel = errorLogExchange.Channel,
            Ip = errorLogExchange.Ip,
            Status = 0,
            CreateBy = errorLogExchange.CreateBy,
            CreateTime = errorLogExchange.CreateTime,
            ModifyBy = errorLogExchange.CreateBy,
            ModifyTime = errorLogExchange.CreateTime
        };

        var resultAdd = await _errorLogRepository.AddAsync(errorLog);

        if (!resultAdd.Success) throw new UnknownException(resultAdd.Message);
    }
}