using EasySoft.Core.AppSecurityServer.Core.DataTransferObjects;
using EasySoft.Core.AppSecurityServer.Core.Entities;
using EasySoft.UtilityTools.Standard.DataTransferObjects;

namespace EasySoft.Core.AppSecurityServer.Core.Extensions;

/// <summary>
/// AppPublicKeyExtensions
/// </summary>
public static class AppPublicKeyExtensions
{
    /// <summary>
    /// ToAccessWayModel
    /// </summary>
    /// <param name="appSecurity"></param>
    /// <returns></returns>
    public static AppPublicKeyDto ToAppPublicKeyDto(this AppPublicKey appSecurity)
    {
        var typeAdapterConfig = new TypeAdapterConfig();

        typeAdapterConfig.ForType<AppPublicKey, AppPublicKeyDto>()
            .Map(
                dest => dest.Key, src => src.Key
            );

        var dto = appSecurity.Adapt<AppPublicKeyDto>(typeAdapterConfig);

        appSecurity.Adapt(dto);

        return dto;
    }
}