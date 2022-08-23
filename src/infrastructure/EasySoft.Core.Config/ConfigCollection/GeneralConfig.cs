﻿using EasySoft.Core.Config.ConfigInterface;
using EasySoft.UtilityTools.Standard;

namespace EasySoft.Core.Config.ConfigCollection;

public class GeneralConfig : IConfig
{
    public static readonly GeneralConfig Instance = new();

    public string CacheMode { get; set; }

    public string TokenName { get; set; }

    public string AccessWayDetectSwitch { get; set; }

    public string RemoteGeneralLogSwitch { get; set; }

    public string RemoteErrorLogSwitch { get; set; }

    public string UseStaticFiles { get; set; }

    public string UseAuthentication { get; set; }

    public string UseAuthorization { get; set; }

    public string CorsSwitch { get; set; }

    public string CorsPolicies { get; set; }

    /// <summary>
    /// Token的时间偏移量 (秒)
    /// </summary>
    public string JsonWebTokenClockSkew { get; set; }

    /// <summary>
    /// 访问群体
    /// </summary>
    public string JsonWebTokenValidAudience { get; set; }

    /// <summary>
    /// 是否验证访问群体
    /// </summary>
    public string JsonWebTokenValidateAudience { get; set; }

    /// <summary>
    /// 颁发者
    /// </summary>
    public string JsonWebTokenValidIssuer { get; set; }

    /// <summary>
    /// 是否验证颁发者
    /// </summary>
    public string JsonWebTokenValidateIssuer { get; set; }

    /// <summary>
    /// 是否验证生存期
    /// </summary>
    public string JsonWebTokenValidateLifetime { get; set; }

    /// <summary>
    /// 安全密钥
    /// </summary>
    public string JsonWebTokenIssuerSigningKey { get; set; }

    /// <summary>
    /// 是否验证安全密钥
    /// </summary>
    public string JsonWebTokenValidateIssuerSigningKey { get; set; }

    /// <summary>
    /// 过期时间 (秒)
    /// </summary>
    public string TokenExpires { get; set; }

    public GeneralConfig()
    {
        CacheMode = "InMemory";
        AccessWayDetectSwitch = "0";
        RemoteGeneralLogSwitch = "0";
        RemoteErrorLogSwitch = "0";
        UseStaticFiles = "1";
        UseAuthentication = "0";
        UseAuthorization = "0";
        CorsSwitch = "0";
        CorsPolicies = "*";
        TokenExpires = "7200";
        TokenName = "token";
        JsonWebTokenClockSkew = "30";
        JsonWebTokenValidateAudience = "1";
        JsonWebTokenValidateIssuer = "1";
        JsonWebTokenValidateLifetime = "1";
        JsonWebTokenValidateIssuerSigningKey = "1";
        JsonWebTokenValidAudience = "";
        JsonWebTokenValidIssuer = "";
        JsonWebTokenIssuerSigningKey = "";
    }
}