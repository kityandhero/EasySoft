using EasySoft.Core.PermissionServer.Core.DataTransferObjects;

namespace EasySoft.Core.PermissionServer.Core.Services.Interfaces;

/// <summary>
/// role group service
/// </summary>
public interface IRoleGroupService
{
    /// <summary>
    /// PageListAsync
    /// </summary>
    /// <param name="roleGroupSearchDto"></param>
    /// <returns></returns>
    public Task<PageListResult<RoleGroupDto>> PageListAsync(RoleGroupSearchDto roleGroupSearchDto);
}