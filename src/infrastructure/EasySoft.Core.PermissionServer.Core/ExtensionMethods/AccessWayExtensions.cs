using EasySoft.Core.PermissionServer.Core.Entities;

namespace EasySoft.Core.PermissionServer.Core.ExtensionMethods;

public static class AccessWayExtensions
{
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
                dest => dest.Channel, src => src.Channel
            );

        var dto = accessWay.Adapt<AccessWayModel>(typeAdapterConfig);

        accessWay.Adapt(dto);

        return dto;
    }
}