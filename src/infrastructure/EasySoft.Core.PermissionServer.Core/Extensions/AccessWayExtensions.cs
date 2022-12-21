using EasySoft.Core.PermissionServer.Core.Entities;
using EasySoft.UtilityTools.Standard.Entities;

namespace EasySoft.Core.PermissionServer.Core.Extensions;

/// <summary>
/// AccessWayExtensions
/// </summary>
public static class AccessWayExtensions
{
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