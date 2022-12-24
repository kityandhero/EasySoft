﻿using EasySoft.Core.AppSecurityServer.Core.Channels;
using EasySoft.Core.AppSecurityServer.Core.Entities;
using EasySoft.Core.AppSecurityServer.Core.Extensions;
using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Services.Implements;

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
            return new ExecutiveResult<AppSecurityDto>(
                ReturnCode.ParamError.ToMessage(
                    $"unixTime params error -> {appSecurityDto.BuildInfo()}"
                )
            );

        if (string.IsNullOrWhiteSpace(appSecurityDto.Salt))
            return new ExecutiveResult<AppSecurityDto>(
                ReturnCode.ParamError.ToMessage(
                    $"salt params error -> {appSecurityDto.BuildInfo()}"
                )
            );

        if (string.IsNullOrWhiteSpace(appSecurityDto.AppId))
            return new ExecutiveResult<AppSecurityDto>(
                ReturnCode.ParamError.ToMessage(
                    $"appId params error -> {appSecurityDto.BuildInfo()}"
                )
            );

        if (appSecurityDto.Channel < 0)
            return new ExecutiveResult<AppSecurityDto>(
                ReturnCode.ParamError.ToMessage(
                    $"channel params error -> {appSecurityDto.BuildInfo()}"
                )
            );

        if (string.IsNullOrWhiteSpace(appSecurityDto.Sign))
            return new ExecutiveResult<AppSecurityDto>(
                ReturnCode.ParamError.ToMessage(
                    $"sign params error -> {appSecurityDto.BuildInfo()}"
                )
            );

        //仅用于内部集成模式，此模式下忽略校验
        if (appSecurityDto.AppId == AppSecurityAssist.EmbedAppId)
            return new ExecutiveResult<AppSecurityDto>(ReturnCode.Ok)
            {
                Data = new AppSecurityDto
                {
                    AppId = appSecurityDto.AppId
                }
            };

        var result = await _appSecurityRepository.GetAsync(
            o => o.AppId == appSecurityDto.AppId
                 && o.AppSecret == appSecurityDto.AppSecret
                 && o.Channel == appSecurityDto.Channel
        );

        if (!result.Success || result.Data == null)
            return new ExecutiveResult<AppSecurityDto>(
                result.Code
            );

        var sign = AppSecurityAssist.SignVerify(result.Data, appSecurityDto.UnixTime, appSecurityDto.Salt);

        if (appSecurityDto.Sign != sign)
            return new ExecutiveResult<AppSecurityDto>(ReturnCode.VerifyError.ToMessage("sign error"));

        return result.ToExecutiveResult(result.Data?.ToAppSecurityDto());
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
    public async Task<ExecutiveResult<AppSecurityDto>> GerMainControlAppSecurity()
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
    public async Task SetSuperRoleNextMaintainTime(AppSecurityDto appSecurityDto)
    {
        var result = await _appSecurityRepository.GetAsync(
            o => o.AppId == appSecurityDto.AppId
                 && o.Channel == appSecurityDto.Channel
        );

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