using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Models;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.AuthenticationCore.Assists;

/// <summary>
/// AuthenticationConfigureAssist
/// </summary>
public class AuthenticationConfigureAssist
{
    #region Fields

    /// <summary>
    /// AuthenticationConfigureNode
    /// </summary>
    private const string AuthenticationConfigureNode = "Authentication";

    /// <summary>
    /// TokenModeConfigureNode
    /// </summary>
    private const string TokenModeConfigureNode = "TokenMode";

    /// <summary>
    /// TokenParseFromUrlSwitchConfigureNode
    /// </summary>
    private const string TokenParseFromUrlSwitchConfigureNode = "TokenParseFromUrlSwitch";

    /// <summary>
    /// TokenParseFromCookieSwitchConfigureNode
    /// </summary>
    private const string TokenParseFromCookieSwitchConfigureNode = "TokenParseFromCookieSwitch";

    /// <summary>
    /// TokenServerDumpSwitchConfigureNode
    /// </summary>
    private const string TokenServerDumpSwitchConfigureNode = "TokenServerDumpSwitch";

    /// <summary>
    /// TokenNameConfigureNode
    /// </summary>
    public const string TokenNameConfigureNode = "TokenName";

    /// <summary>
    /// TokenExpiresConfigureNode
    /// </summary>
    public const string TokenExpiresConfigureNode = "TokenExpires";

    /// <summary>
    /// EasyTokenSecretKeyConfigureNode
    /// </summary>
    public const string EasyTokenSecretKeyConfigureNode = "EasyTokenSecretKey";

    /// <summary>
    /// JsonWebTokenClockSkewConfigureNode
    /// </summary>
    public const string JsonWebTokenClockSkewConfigureNode = "JsonWebTokenClockSkew";

    /// <summary>
    /// JsonWebTokenValidIssuerConfigureNode
    /// </summary>
    public const string JsonWebTokenValidIssuerConfigureNode = "JsonWebTokenValidIssuer";

    /// <summary>
    /// JsonWebTokenValidateIssuerConfigureNode
    /// </summary>
    public const string JsonWebTokenValidateIssuerConfigureNode = "JsonWebTokenValidateIssuer";

    /// <summary>
    /// JsonWebTokenValidAudienceConfigureNode
    /// </summary>
    public const string JsonWebTokenValidAudienceConfigureNode = "JsonWebTokenValidAudience";

    /// <summary>
    /// JsonWebTokenValidateAudienceConfigureNode
    /// </summary>
    public const string JsonWebTokenValidateAudienceConfigureNode = "JsonWebTokenValidateAudience";

    /// <summary>
    /// JsonWebTokenIssuerSigningKeyConfigureNode
    /// </summary>
    public const string JsonWebTokenIssuerSigningKeyConfigureNode = "JsonWebTokenIssuerSigningKey";

    /// <summary>
    /// JsonWebTokenValidateIssuerSigningKeyConfigureNode
    /// </summary>
    public const string JsonWebTokenValidateIssuerSigningKeyConfigureNode = "JsonWebTokenValidateIssuerSigningKey";

    /// <summary>
    /// JsonWebTokenValidateLifetimeConfigureNode
    /// </summary>
    public const string JsonWebTokenValidateLifetimeConfigureNode = "JsonWebTokenValidateLifetime";

    #endregion

    #region Properties

    /// <summary>
    /// Configuration
    /// </summary>
    public IConfiguration Configuration { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// SqlLogTransceiverConfigureAssist
    /// </summary>
    /// <param name="configuration"></param>
    public AuthenticationConfigureAssist(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #endregion

    #region Methods

    #region GetTokenMode

    /// <summary>
    /// GetTokenMode
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetTokenMode(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthenticationConfigureNode,
                TokenModeConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? UtilityTools.Standard.ConstCollection.EasyToken;

        if (!v.In(UtilityTools.Standard.ConstCollection.EasyToken, UtilityTools.Standard.ConstCollection.JsonWebToken))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 {UtilityTools.Standard.ConstCollection.EasyToken}|{UtilityTools.Standard.ConstCollection.JsonWebToken}"
            );
        }

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// GetTokenMode
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetTokenMode()
    {
        return GetTokenMode(Configuration);
    }

    #endregion

    #region GetTokenParseFromUrlSwitch

    /// <summary>
    /// 支持从Url解析Token, 特定场景下无法使用 Http Header 时候可开启, 有安全隐患
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetTokenParseFromUrlSwitch(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthenticationConfigureNode,
                TokenParseFromUrlSwitchConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "false";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// 支持从Url解析Token, 特定场景下无法使用 Http Header 时候可开启, 有安全隐患
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetTokenParseFromUrlSwitch()
    {
        return GetTokenParseFromUrlSwitch(Configuration);
    }

    #endregion

    #region GetTokenParseFromCookieSwitch

    /// <summary>
    /// 支持从Cookie解析Token, 使用场景为传统MVC开发等场景, 适用于单体部署
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetTokenParseFromCookieSwitch(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthenticationConfigureNode,
                TokenParseFromCookieSwitchConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "false";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// 支持从Cookie解析Token, 使用场景为传统MVC开发等场景, 适用于单体部署
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetTokenParseFromCookieSwitch()
    {
        return GetTokenParseFromCookieSwitch(Configuration);
    }

    #endregion

    #region GetTokenServerDumpSwitch

    /// <summary>
    /// 服务端使用缓存存储键值 token, 仅返回前端键名，默认开启
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetTokenServerDumpSwitch(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthenticationConfigureNode,
                TokenServerDumpSwitchConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "true";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// 服务端使用缓存存储键值 token, 仅返回前端键名
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetTokenServerDumpSwitch()
    {
        return GetTokenServerDumpSwitch(Configuration);
    }

    #endregion

    #region GetTokenName

    /// <summary>
    /// GetTokenName
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetTokenName(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthenticationConfigureNode,
                TokenNameConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "token";

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// GetTokenName
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetTokenName()
    {
        return GetTokenName(Configuration);
    }

    #endregion

    #region GetTokenExpires

    /// <summary>
    /// Token 过期时间 (秒), 默认7200
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<int> GetTokenExpires(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthenticationConfigureNode,
                TokenExpiresConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "7200";

        var errorMessage = $"[{node}]配置无效，如需要配置，请设置数字 value > 0";

        if (!v.IsInt())
        {
            throw new ConfigErrorException(
                errorMessage
            );
        }

        var valueAdjust = v.ToInt();

        if (valueAdjust < 0)
        {
            throw new ConfigErrorException(
                errorMessage
            );
        }

        return new ConfigValue<int>
        {
            Node = node,
            Value = valueAdjust
        };
    }

    /// <summary>
    /// Token 过期时间 (秒), 默认7200
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<int> GetTokenExpires()
    {
        return GetTokenExpires(Configuration);
    }

    #endregion

    #region GetEasyTokenSecretKey

    /// <summary>
    /// 获取 EasyToken 加密密钥
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetEasyTokenSecretKey(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthenticationConfigureNode,
                EasyTokenSecretKeyConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，请设置"
            );
        }

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// 获取 EasyToken 加密密钥
    /// </summary>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetEasyTokenSecretKey()
    {
        return GetEasyTokenSecretKey(Configuration);
    }

    #endregion

    #region GetJsonWebTokenClockSkew

    /// <summary>
    /// JsonWebToken 的时间偏移量 (秒)，默认 30
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<int> GetJsonWebTokenClockSkew(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthenticationConfigureNode,
                JsonWebTokenClockSkewConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "30";

        var errorMessage = $"[{node}]配置无效，如需要配置，请设置数字 value > 0";

        if (!v.IsInt())
        {
            throw new ConfigErrorException(
                errorMessage
            );
        }

        var valueAdjust = v.ToInt();

        if (valueAdjust < 0)
        {
            throw new ConfigErrorException(
                errorMessage
            );
        }

        return new ConfigValue<int>
        {
            Node = node,
            Value = valueAdjust
        };
    }

    /// <summary>
    /// JsonWebToken 的时间偏移量 (秒)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<int> GetJsonWebTokenClockSkew()
    {
        return GetJsonWebTokenClockSkew(Configuration);
    }

    #endregion

    #region GetJsonWebTokenValidIssuer

    /// <summary>
    /// JsonWebToken 颁发者
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetJsonWebTokenValidIssuer(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthenticationConfigureNode,
                JsonWebTokenValidIssuerConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// JsonWebToken 颁发者
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetJsonWebTokenValidIssuer()
    {
        return GetJsonWebTokenValidIssuer(Configuration);
    }

    #endregion

    #region GetJsonWebTokenValidateIssuer

    /// <summary>
    /// JsonWebToken 是否验证颁发者, 默认开启
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetJsonWebTokenValidateIssuer(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthenticationConfigureNode,
                JsonWebTokenValidateIssuerConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "true";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// JsonWebToken 是否验证颁发者, 默认开启
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetJsonWebTokenValidateIssuer()
    {
        return GetJsonWebTokenValidateIssuer(Configuration);
    }

    #endregion

    #region GetJsonWebTokenValidAudience

    /// <summary>
    /// JsonWebToken 访问群体
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetJsonWebTokenValidAudience(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthenticationConfigureNode,
                JsonWebTokenValidAudienceConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// JsonWebToken 访问群体
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetJsonWebTokenValidAudience()
    {
        return GetJsonWebTokenValidAudience(Configuration);
    }

    #endregion

    #region GetJsonWebTokenValidateAudience

    /// <summary>
    /// JsonWebToken 是否验证访问群体, 默认开启
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetJsonWebTokenValidateAudience(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthenticationConfigureNode,
                JsonWebTokenValidateAudienceConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "true";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// JsonWebToken 是否验证访问群体, 默认开启
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetJsonWebTokenValidateAudience()
    {
        return GetJsonWebTokenValidateAudience(Configuration);
    }

    #endregion

    #region GetJsonWebTokenIssuerSigningKey

    /// <summary>
    /// JsonWebToken 安全密钥
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetJsonWebTokenIssuerSigningKey(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthenticationConfigureNode,
                JsonWebTokenIssuerSigningKeyConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效"
            );
        }

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// JsonWebToken 安全密钥
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetJsonWebTokenIssuerSigningKey()
    {
        return GetJsonWebTokenIssuerSigningKey(Configuration);
    }

    #endregion

    #region GetJsonWebTokenValidateIssuerSigningKey

    /// <summary>
    /// JsonWebToken 是否验证安全密钥, 默认开启
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetJsonWebTokenValidateIssuerSigningKey(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthenticationConfigureNode,
                JsonWebTokenValidateIssuerSigningKeyConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "true";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// JsonWebToken 是否验证安全密钥, 默认开启
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetJsonWebTokenValidateIssuerSigningKey()
    {
        return GetJsonWebTokenValidateIssuerSigningKey(Configuration);
    }

    #endregion

    #region GetJsonWebTokenValidateLifetime

    /// <summary>
    /// JsonWebToken 是否验证生存期, 默认开启
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetJsonWebTokenValidateLifetime(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthenticationConfigureNode,
                JsonWebTokenValidateLifetimeConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "true";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// JsonWebToken 是否验证生存期, 默认开启
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetJsonWebTokenValidateLifetime()
    {
        return GetJsonWebTokenValidateLifetime(Configuration);
    }

    #endregion

    #endregion
}