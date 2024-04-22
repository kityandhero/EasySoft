using EasySoft.Core.PermissionVerification.Entities;

namespace EasySoft.Core.PermissionVerification.Extensions;

/// <summary>
/// AccessWayMessageExtensions
/// </summary>
public static class AccessWayMessageExtensions
{
    /// <summary>
    /// ToAccessWayModel
    /// </summary>
    /// <param name="accessWay"></param>
    /// <returns></returns>
    public static AccessWayModel ToAccessWayModel(this IAccessWayMessage accessWay)
    {
        var typeAdapterConfig = new TypeAdapterConfig();

        typeAdapterConfig.ForType<IAccessWayMessage, AccessWayModel>()
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

        var dto = accessWay.Adapt<AccessWayModel>(typeAdapterConfig);

        accessWay.Adapt(dto);

        return dto;
    }
}