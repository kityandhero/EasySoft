using EasySoft.Core.PermissionVerification.Entities;

namespace EasySoft.Core.PermissionVerification.Extensions;

/// <summary>
/// AccessWayMessageExtensions
/// </summary>
public static class AccessWayMessageExtensions
{
    /// <summary>
    /// ToAccessWayTransitiveMessage
    /// </summary>
    /// <param name="accessWayTransitive
    /// <returns></returns>
    public static AccessWayModel ToAccessWayModel(
        this IAccessWayTransitiveMessage accessWayTransitive
    )
    {
        var typeAdapterConfig = new TypeAdapterConfig();

        typeAdapterConfig.ForType<IAccessWayTransitiveMessage, AccessWayModel>()
            .Map(
                dest => dest.Name,
                src => src.Name
            )
            .Map(
                dest => dest.GuidTag,
                src => src.GuidTag
            )
            .Map(
                dest => dest.RelativePath,
                src => src.RelativePath
            )
            .Map(
                dest => dest.Expand,
                src => src.Expand
            )
            .Map(
                dest => dest.Group,
                src => src.Group
            )
            .Map(
                dest => dest.TriggerChannel,
                src => src.TriggerChannel
            );

        var dto = accessWayTransitive.Adapt<AccessWayModel>(typeAdapterConfig);

        accessWayTransitive.Adapt(dto);

        return dto;
    }
}