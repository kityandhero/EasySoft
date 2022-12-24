using EasySoft.Core.AppSecurityServer.Core.Entities;

namespace EasySoft.Core.AppSecurityServer.Core.Extensions;

/// <summary>
/// AccessWayExtensions
/// </summary>
public static class AppSecurityExtensions
{
    /// <summary>
    /// ToAccessWayModel
    /// </summary>
    /// <param name="appSecurity"></param>
    /// <returns></returns>
    public static AppSecurityDto ToAppSecurityDto(this AppSecurity appSecurity)
    {
        var typeAdapterConfig = new TypeAdapterConfig();

        typeAdapterConfig.ForType<AppSecurity, AppSecurityDto>()
            .Map(
                dest => dest.AppSecurityId, src => src.Id
            )
            .Map(
                dest => dest.AppId, src => src.AppId
            ).Map(
                dest => dest.AppSecret, src => src.AppSecret
            ).Map(
                dest => dest.Channel, src => src.Channel
            );

        var dto = appSecurity.Adapt<AppSecurityDto>(typeAdapterConfig);

        appSecurity.Adapt(dto);

        return dto;
    }
}