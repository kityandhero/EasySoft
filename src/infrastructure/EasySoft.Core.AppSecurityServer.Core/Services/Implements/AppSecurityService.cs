using EasySoft.Core.AppSecurityServer.Core.Channels;
using EasySoft.Core.AppSecurityServer.Core.Entities;
using EasySoft.Core.AppSecurityServer.Core.Extensions;
using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Services.Implements;

/// <inheritdoc />
public class AppSecurityService : IAppSecurityService
{
    private readonly IRepository<AppSecurity> _appSecurityRepository;

    /// <summary>
    /// UserService
    /// </summary>
    /// <param name="appSecurityRepository"></param>
    public AppSecurityService(
        IRepository<AppSecurity> appSecurityRepository
    )
    {
        _appSecurityRepository = appSecurityRepository;
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

    /// <inheritdoc />
    public async Task<ExecutiveResult> TryCreateAsync(int channel)
    {
        var result = await _appSecurityRepository.GetAsync(
            o => o.Channel == channel
        );

        if (result.Success)
        {
            if (result.Data != null) return ExecutiveResultAssist.CreateOk();

            return new ExecutiveResult(ReturnCode.DataError.ToMessage("查询无返回"));
        }

        return result.ToExecutiveResult();
    }

    /// <inheritdoc />
    public async Task<ExecutiveResult<AppSecurityDto>> GetMainControlAppSecurity()
    {
        var result = await _appSecurityRepository.GetAsync(
            o => o.Channel == InnerChannel.AppSecurityServerChannel.GetChannel()
        );

        return result.ToExecutiveResult(result.Data?.ToAppSecurityDto());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AppSecurityDto>> SingleListNeedMaintain()
    {
        var time = DateTimeOffset.Now.DateTime;

        var result = await _appSecurityRepository.PageListAsync(
            1,
            20,
            o => o.SuperRoleNextMaintainTime <= time
        );

        return result.List.Select(o => o.ToAppSecurityDto());
    }

    /// <inheritdoc />
    public async Task<ExecutiveResult> MaintainMasterControlAsync()
    {
        var result = await _appSecurityRepository.GetAsync(
            o => o.Channel == InnerChannel.AppSecurityServerChannel.GetChannel()
        );

        if (result.Success) return new ExecutiveResult(ReturnCode.Ok);

        var appSecurity = new AppSecurity
        {
            AppId = UniqueIdAssist.CreateUUID(),
            AppSecret = UniqueIdAssist.CreateUUID(),
            Channel = InnerChannel.AppSecurityServerChannel.GetChannel()
        };

        var resultAdd = await _appSecurityRepository.AddAsync(appSecurity);

        return resultAdd.ToExecutiveResult();
    }

    /// <inheritdoc />
    public async Task SetSuperRoleNextMaintainTime(AppSecurityDto appSecurityDto)
    {
        var result = await _appSecurityRepository.GetAsync(appSecurityDto.AppSecurityId);

        if (result.Success && result.Data != null)
        {
            var data = result.Data;

            data.SuperRoleRecentlyMaintainTime = DateTimeOffset.Now.DateTime;
            data.SuperRoleNextMaintainTime = DateTimeOffset.Now.DateTime.AddHours(12);
            data.ModifyTime = DateTimeOffset.Now.DateTime;

            await _appSecurityRepository.UpdateAsync(data);
        }
    }
}