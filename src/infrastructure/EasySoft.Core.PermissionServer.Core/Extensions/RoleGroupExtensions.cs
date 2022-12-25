using EasySoft.Core.PermissionServer.Core.DataTransferObjects;
using EasySoft.Core.PermissionServer.Core.Entities;

namespace EasySoft.Core.PermissionServer.Core.Extensions;

/// <summary>
/// RoleGroupExtensions
/// </summary>
public static class RoleGroupExtensions
{
    /// <summary>
    /// ToAccessWayModel
    /// </summary>
    /// <param name="roleGroup"></param>
    /// <returns></returns>
    public static RoleGroupDto ToRoleGroupDto(this RoleGroup roleGroup)
    {
        var typeAdapterConfig = new TypeAdapterConfig();

        typeAdapterConfig.ForType<RoleGroup, RoleGroupDto>()
            .Map(
                dest => dest.RoleGroupId, src => src.Id
            )
            .Map(
                dest => dest.Name, src => src.Name
            );

        var dto = roleGroup.Adapt<RoleGroupDto>(typeAdapterConfig);

        roleGroup.Adapt(dto);

        return dto;
    }
}