using EasySoft.Core.AppSecurityServer.Core.DataTransferObjects;
using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;
using EasySoft.Core.AppSecurityServer.Core.Entities;
using EasySoft.Core.AppSecurityServer.Core.Extensions;
using EasySoft.UtilityTools.Standard.DataTransferObjects;

namespace EasySoft.Core.AppSecurityServer.Core.Services.Implementations;

/// <inheritdoc />
public class AppSecurityService : IAppSecurityService
{
    private readonly IEventPublisher _eventPublisher;

    private readonly IRepository<AppSecurity> _appSecurityRepository;

    /// <summary>
    /// UserService
    /// </summary>
    /// <param name="eventPublisher"></param>
    /// <param name="appSecurityRepository"></param>
    public AppSecurityService(
        IEventPublisher eventPublisher,
        IRepository<AppSecurity> appSecurityRepository
    )
    {
        _eventPublisher = eventPublisher;

        _appSecurityRepository = appSecurityRepository;
    }

    /// <inheritdoc />
    public async Task<ExecutiveResult<AppSecurityDto>> VerifyAsync(AppSecurityDto appSecurityDto)
    {
        if (appSecurityDto.UnixTime < 0)
            return new ExecutiveResult<AppSecurityDto>(ReturnCode.ParamError.ToMessage("unixTime error"));

        if (string.IsNullOrWhiteSpace(appSecurityDto.Salt))
            return new ExecutiveResult<AppSecurityDto>(ReturnCode.ParamError.ToMessage("salt error"));

        if (string.IsNullOrWhiteSpace(appSecurityDto.AppId))
            return new ExecutiveResult<AppSecurityDto>(ReturnCode.ParamError.ToMessage("appId error"));

        if (appSecurityDto.Channel < 0)
            return new ExecutiveResult<AppSecurityDto>(ReturnCode.ParamError.ToMessage("channel error"));

        if (string.IsNullOrWhiteSpace(appSecurityDto.Sign))
            return new ExecutiveResult<AppSecurityDto>(ReturnCode.ParamError.ToMessage("sign error"));

        var result = await _appSecurityRepository.GetAsync(
            o => o.AppId == appSecurityDto.AppId
                 && o.Channel == appSecurityDto.Channel
        );

        if (!result.Success || result.Data == null) return new ExecutiveResult<AppSecurityDto>(result.Code);

        var sign = AppSecurityAssist.Sign(result.Data, appSecurityDto.UnixTime, appSecurityDto.Salt);

        if (appSecurityDto.Sign != sign)
            return new ExecutiveResult<AppSecurityDto>(ReturnCode.VerifyError.ToMessage("sign error"));

        return result.ToExecutiveResult(result.Data?.ToAppSecurityDto());
    }

    /// <inheritdoc />
    public async Task<ExecutiveResult<AppSecurityDto>> CreateAsync(AppSecurityDto appSecurityDto)
    {
        if (appSecurityDto.Channel < 0)
            return new ExecutiveResult<AppSecurityDto>(
                ReturnCode.ParamError.ToMessage(
                    "channel must greater than 0 or equal to 0."
                )
            );

        var appSecurity = new AppSecurity
        {
            Channel = appSecurityDto.Channel
        };

        var result = await _appSecurityRepository.AddAsync(appSecurity);

        return result.ToExecutiveResult(result.Data?.ToAppSecurityDto());
    }
}