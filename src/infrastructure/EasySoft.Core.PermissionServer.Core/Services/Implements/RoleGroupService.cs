using EasySoft.Core.PermissionServer.Core.DataTransferObjects;
using EasySoft.Core.PermissionServer.Core.Entities;
using EasySoft.Core.PermissionServer.Core.Extensions;
using EasySoft.Core.PermissionServer.Core.Services.Interfaces;

namespace EasySoft.Core.PermissionServer.Core.Services.Implements;

/// <inheritdoc />
public class RoleGroupService : IRoleGroupService
{
    private readonly IRepository<RoleGroup> _roleGroupRepository;

    /// <summary>
    /// UserService
    /// </summary>
    /// <param name="roleGroupRepository"></param>
    public RoleGroupService(
        IRepository<RoleGroup> roleGroupRepository
    )
    {
        _roleGroupRepository = roleGroupRepository;
    }

    /// <inheritdoc />
    public async Task<PageListResult<RoleGroupDto>> PageListAsync(RoleGroupSearchDto roleGroupSearchDto)
    {
        var pageListResult = await _roleGroupRepository.PageListAsync(
            roleGroupSearchDto.PageNo,
            roleGroupSearchDto.PageSize
        );

        return pageListResult.ToPageListResult(o => o.ToRoleGroupDto());
    }
}