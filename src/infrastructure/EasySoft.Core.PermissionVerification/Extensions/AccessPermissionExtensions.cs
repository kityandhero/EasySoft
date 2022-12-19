using EasySoft.Core.PermissionVerification.Entities;

namespace EasySoft.Core.PermissionVerification.Extensions;

/// <summary>
/// AccessPermissionExtensions
/// </summary>
public static class AccessPermissionExtensions
{
    /// <summary>
    /// ToAccessWayModel
    /// </summary>
    /// <param name="accessWay"></param>
    /// <returns></returns>
    public static AccessWayModel ToAccessWayModel(this AccessPermission accessWay)
    {
        var typeAdapterConfig = new TypeAdapterConfig();

        typeAdapterConfig.ForType<AccessPermission, AccessWayModel>()
            .Map(
                dest => dest.Name, src => src.Name
            ).Map(
                dest => dest.GuidTag, src => src.GuidTag
            ).Map(
                dest => dest.RelativePath, src => src.Path
            ).Map(
                dest => dest.Expand, src => src.Competence
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