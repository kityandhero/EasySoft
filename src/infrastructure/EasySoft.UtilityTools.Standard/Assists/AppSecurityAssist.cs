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
    public static AppSecurityDto SignVerify(AppSecurityDto appSecurityDto)
    {
        var sign = SignVerify(appSecurityDto, appSecurityDto.UnixTime, appSecurityDto.Salt);

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
    public static string SignVerify(IAppSecurity appSecurity, long unixTime, string salt)
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

    /// <summary>
    /// 请求签名
    /// </summary>
    /// <param name="appSecurityDto"></param>
    /// <returns></returns>
    public static AppSecurityDto SignRequest(AppSecurityDto appSecurityDto)
    {
        var sign = SignRequest(appSecurityDto, appSecurityDto.PublicKey, appSecurityDto.Salt);

        appSecurityDto.Sign = sign;

        return appSecurityDto;
    }

    /// <summary>
    /// 请求签名
    /// </summary>
    /// <param name="appSecurity"></param>
    /// <param name="publicKey"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    public static string SignRequest(IAppSecurity appSecurity, string publicKey, string salt)
    {
        return SignRequest(appSecurity.AppId, publicKey, salt);
    }

    /// <summary>
    /// 请求签名
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="publicKey"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    public static string SignRequest(string appId, string publicKey, string salt)
    {
        if (string.IsNullOrWhiteSpace(salt)) throw new UnknownException("salt error");

        if (string.IsNullOrWhiteSpace(appId)) throw new UnknownException("AppId not allow empty");

        if (string.IsNullOrWhiteSpace(publicKey)) throw new UnknownException("publicKey not allow empty");

        var appIdAdjust = appId.Trim().Remove(" ");

        if (appIdAdjust != appId)
            throw new UnknownException("appid length error");

        var publicKeyAdjust = publicKey.Trim().Remove(" ");

        if (publicKeyAdjust != publicKey)
            throw new UnknownException("publicKey length error");

        return $"{appId}{publicKey}{salt}".ToMd5();
    }
}