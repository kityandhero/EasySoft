﻿using EasySoft.Core.Permission.Server.Entities;
using EasySoft.Core.PermissionVerification.Entities;

namespace EasySoft.Core.Permission.Server.ExtensionMethods;

public static class BlogExtensions
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