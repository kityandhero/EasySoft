using EasySoft.Core.PermissionServer.Core.DataTransferObjects;
using EasySoft.Core.PermissionServer.Core.Entities;
using EasySoft.Core.PermissionServer.Core.Extensions;
using EasySoft.Core.PermissionServer.Core.Services.Interfaces;

namespace EasySoft.Core.PermissionServer.Core.Services.Implements;

/// <inheritdoc />
public class PresetRoleService : IPresetRoleService
{
    private readonly IRepository<PresetRole> _presetRoleRepository;

    /// <summary>
    /// UserService
    /// </summary>
    /// <param name="presetRoleRepository"></param>
    public PresetRoleService(
        IRepository<PresetRole> presetRoleRepository
    )
    {
        _presetRoleRepository = presetRoleRepository;
    }

    /// <inheritdoc />
    public async Task<PageListResult<PresetRoleDto>> PageListAsync(PresetRoleSearchDto presetRoleSearchDto)
    {
        var pageListResult = await _presetRoleRepository.PageListAsync(
            presetRoleSearchDto.PageNo,
            presetRoleSearchDto.PageSize
        );

        return pageListResult.ToPageListResult(o => o.ToPresetRoleDto());
    }
}