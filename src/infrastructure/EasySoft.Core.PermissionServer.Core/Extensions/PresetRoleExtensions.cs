using EasySoft.Core.PermissionServer.Core.DataTransferObjects;
using EasySoft.Core.PermissionServer.Core.Entities;

namespace EasySoft.Core.PermissionServer.Core.Extensions;

/// <summary>
/// AppPublicKeyExtensions
/// </summary>
public static class PresetRoleExtensions
{
    /// <summary>
    /// ToAccessWayModel
    /// </summary>
    /// <param name="presetRole"></param>
    /// <returns></returns>
    public static PresetRoleDto ToPresetRoleDto(this PresetRole presetRole)
    {
        var typeAdapterConfig = new TypeAdapterConfig();

        typeAdapterConfig.ForType<PresetRole, PresetRoleDto>()
            .Map(
                dest => dest.PresetRoleId, src => src.Id
            )
            .Map(
                dest => dest.Competence, src => src.Competence
            )
            .Map(
                dest => dest.WhetherSuper, src => src.WhetherSuper
            )
            .Map(
                dest => dest.Name, src => src.Name
            )
            .Map(
                dest => dest.Description, src => src.Description
            )
            .Map(
                dest => dest.Content, src => src.Content
            );

        var dto = presetRole.Adapt<PresetRoleDto>(typeAdapterConfig);

        presetRole.Adapt(dto);

        return dto;
    }
}