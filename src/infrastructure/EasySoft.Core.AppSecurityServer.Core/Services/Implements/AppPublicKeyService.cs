﻿using EasySoft.Core.AppSecurityServer.Core.Entities;
using EasySoft.Core.AppSecurityServer.Core.Extensions;
using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Services.Implements;

/// <inheritdoc />
public class AppPublicKeyService : IAppPublicKeyService
{
    private readonly IRepository<AppPublicKey> _appPublicKeyRepository;

    /// <summary>
    /// UserService
    /// </summary>
    /// <param name="appPublicKeyRepository"></param>
    public AppPublicKeyService(
        IRepository<AppPublicKey> appPublicKeyRepository
    )
    {
        _appPublicKeyRepository = appPublicKeyRepository;
    }

    /// <inheritdoc />
    public async Task<ExecutiveResult<AppPublicKeyDto>> GetAsync()
    {
        var pageListResult = await _appPublicKeyRepository.PageListAsync(
            1,
            1
        );

        if (!pageListResult.Success) return new ExecutiveResult<AppPublicKeyDto>(pageListResult.Code);

        if (pageListResult.Count() > 0)
        {
            var list = pageListResult.List;

            return new ExecutiveResult<AppPublicKeyDto>(ReturnCode.Ok)
            {
                Data = list.First().ToAppPublicKeyDto()
            };
        }

        var appPublicKey = new AppPublicKey
        {
            Key = UniqueIdAssist.CreateUUID()
        };

        var result = await _appPublicKeyRepository.AddAsync(
            appPublicKey
        );

        return result.ToExecutiveResult(appPublicKey.ToAppPublicKeyDto());
    }

    /// <inheritdoc />
    public async Task<ExecutiveResult<AppPublicKeyDto>> RefreshAsync()
    {
        var pageListResult = await _appPublicKeyRepository.PageListAsync(
            1,
            1
        );

        if (!pageListResult.Success) return new ExecutiveResult<AppPublicKeyDto>(pageListResult.Code);

        AppPublicKey appPublicKey;

        if (pageListResult.Count() > 0)
        {
            var list = pageListResult.List;

            appPublicKey = list.First();

            appPublicKey.Key = UniqueIdAssist.CreateUUID();

            var resultUpdate = await _appPublicKeyRepository.UpdateAsync(
                appPublicKey
            );

            return resultUpdate.ToExecutiveResult(appPublicKey.ToAppPublicKeyDto());
        }

        appPublicKey = new AppPublicKey
        {
            Key = UniqueIdAssist.CreateUUID()
        };

        var resultAdd = await _appPublicKeyRepository.AddAsync(
            appPublicKey
        );

        return resultAdd.ToExecutiveResult(appPublicKey.ToAppPublicKeyDto());
    }

    /// <inheritdoc />
    public async Task DetectionAsync()
    {
        var pageListResult = await _appPublicKeyRepository.PageListAsync(
            1,
            1
        );

        if (!pageListResult.Success) return;

        if (pageListResult.Count() > 0) return;

        await RefreshAsync();
    }
}