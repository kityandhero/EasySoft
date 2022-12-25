using EasySoft.Core.PermissionServer.Core.DataTransferObjects;
using EasySoft.Core.PermissionServer.Core.Entities;

namespace EasySoft.Core.PermissionServer.Core.Extensions;

/// <summary>
/// AccessWayExtensions
/// </summary>
public static class AccessWayExtensions
{
    /// <summary>
    /// ToAccessWayModel
    /// </summary>
    /// <param name="presetRole"></param>
    /// <returns></returns>
    public static AccessWayDto ToAccessWayDto(this AccessWay presetRole)
    {
        var typeAdapterConfig = new TypeAdapterConfig();

        typeAdapterConfig.ForType<AccessWay, AccessWayDto>()
            .Map(
                dest => dest.AccessWayId, src => src.Id
            )
            .Map(
                dest => dest.Name, src => src.Name
            );

        var dto = presetRole.Adapt<AccessWayDto>(typeAdapterConfig);

        presetRole.Adapt(dto);

        return dto;
    }

    /// <summary>
    /// ToAccessWayModel
    /// </summary>
    /// <param name="accessWay"></param>
    /// <returns></returns>
    public static AccessWayModel ToAccessWayModel(this AccessWay accessWay)
    {
        var typeAdapterConfig = new TypeAdapterConfig();

        typeAdapterConfig.ForType<AccessWay, AccessWayModel>()
            .Map(
                dest => dest.Name, src => src.Name
            ).Map(
                dest => dest.GuidTag, src => src.GuidTag
            ).Map(
                dest => dest.RelativePath, src => src.RelativePath
            ).Map(
                dest => dest.Expand, src => src.Expand
            ).Map(
                dest => dest.Group, src => src.Group
            ).Map(
                dest => dest.Channel, src => src.Channel
            );

        var dto = accessWay.Adapt<AccessWayModel>(typeAdapterConfig);

        accessWay.Adapt(dto);

        return dto;
    }
}