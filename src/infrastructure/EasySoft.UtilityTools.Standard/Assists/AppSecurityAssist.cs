using EasySoft.UtilityTools.Standard.DataTransferObjects;
using EasySoft.UtilityTools.Standard.Entities.Interfaces;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Media.Image;

namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// AppSecurityAssist
/// </summary>
public static class AppSecurityAssist
{
    /// <summary>
    /// 内嵌模式公共AppId
    /// </summary>
    public const string EmbedAppId = "EmbedModeAppId";

    /// <summary>
    /// 内嵌模式公共AppId
    /// </summary>
    public const string EmbedAppSecret = "EmbedModeSecret";

    /// <summary>
    /// get salt
    /// </summary>
    /// <returns></returns>
    public static string GetSalt()
    {
        return new Captcha().SetLetterCount(4).GetRandomString();
    }

    /// <summary>
    /// get unixTime
    /// </summary>
    /// <returns></returns>
    public static long GetUnixTime()
    {
        return DateTime.Now.ToUnixTime();
    }

    /// <summary>
    /// 签名
    /// </summary>
    /// <param name="appSecurityDto"></param>
    /// <returns></returns>
    public static AppSecurityDto Sign(AppSecurityDto appSecurityDto)
    {
        var sign = Sign(appSecurityDto, appSecurityDto.UnixTime, appSecurityDto.Salt);

        appSecurityDto.Sign = sign;

        return appSecurityDto;
    }

    /// <summary>
    /// 签名
    /// </summary>
    /// <param name="appSecurity"></param>
    /// <param name="unixTime"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    public static string Sign(IAppSecurity appSecurity, long unixTime, string salt)
    {
        if (unixTime < 0) throw new UnknownException("unixTime error");

        if (string.IsNullOrWhiteSpace(salt)) throw new UnknownException("salt error");

        if (string.IsNullOrWhiteSpace(appSecurity.AppId)) throw new UnknownException("AppId not allow empty");

        if (string.IsNullOrWhiteSpace(appSecurity.AppSecret)) throw new UnknownException("AppSecret not allow empty");

        var appIdAdjust = appSecurity.AppId.Trim().Remove(" ");

        if (appIdAdjust != appSecurity.AppId)
            throw new UnknownException("appid length error");

        var appSecretAdjust = appSecurity.AppSecret.Trim().Remove(" ");

        if (appSecretAdjust != appSecurity.AppSecret)
            throw new UnknownException("appSecret length error");

        return $"{appSecurity.AppId}{appSecurity}{unixTime.ToString()}{salt}".ToMd5();
    }
}