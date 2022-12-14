using EasySoft.Core.PermissionVerification.Entities;

namespace EasySoft.Core.PermissionVerification.ExtensionMethods;

/// <summary>
/// AccessWayExchangeExtensions
/// </summary>
public static class AccessWayExchangeExtensions
{
    /// <summary>
    /// ToAccessWayModel
    /// </summary>
    /// <param name="accessWay"></param>
    /// <returns></returns>
    public static AccessWayModel ToAccessWayModel(this AccessWayExchange accessWay)
    {
        var typeAdapterConfig = new TypeAdapterConfig();

        typeAdapterConfig.ForType<AccessWayExchange, AccessWayModel>()
            .Map(
                dest => dest.Name, src => src.Name
            ).Map(
                dest => dest.GuidTag, src => src.GuidTag
            ).Map(
                dest => dest.RelativePath, src => src.RelativePath
            ).Map(
                dest => dest.Expand, src => src.Expand
            ).Map(
                dest => dest.Channel, src => src.Channel
            );

        var dto = accessWay.Adapt<AccessWayModel>(typeAdapterConfig);

        accessWay.Adapt(dto);

        return dto;
    }
}