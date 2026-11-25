using EasySoft.Core.PermissionServer.Core.DataTransferObjects;

namespace EasySoft.Core.PermissionServer.Core.Services.Interfaces;

/// <summary>
/// 预设角色服务
/// </summary>
public interface IPresetRoleService
{
    /// <summary>
    /// PageListAsync
    /// </summary>
    /// <param name="presetRoleSearchDto"></param>
    /// <returns></returns>
    public Task<PageListResult<PresetRoleDto>> PageListAsync(PresetRoleSearchDto presetRoleSearchDto);
}