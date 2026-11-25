using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.UtilityTools.Core;

/// <summary>
/// ConfigureNodeCollection
/// </summary>
public static class ConfigureNodeCollection
{
    #region Exceptionless

    /// <summary>
    /// ExceptionlessConfigureNode
    /// </summary>
    private const string ExceptionlessConfigureNode = "Exceptionless";

    /// <summary>
    /// ExceptionlessTeleportConfigureNode
    /// </summary>
    private const string ExceptionlessTeleportConfigureNode = "Teleport";

    /// <summary>
    /// ExceptionlessServerUrlConfigureNode
    /// </summary>
    private const string ExceptionlessServerUrlConfigureNode = "ServerUrl";

    /// <summary>
    /// ExceptionlessApiKeyConfigureNode
    /// </summary>
    private const string ExceptionlessApiKeyConfigureNode = "ApiKey";

    #endregion

    #region GetExceptionlessTeleport

    /// <summary>
    /// GetExceptionlessTeleport
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetExceptionlessTeleport(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                ExceptionlessConfigureNode,
                ExceptionlessTeleportConfigureNode
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

    #endregion

    #region GetExceptionlessServerUrl

    /// <summary>
    /// GetExceptionlessServerUrl
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetExceptionlessServerUrl(IConfiguration configuration)
    {
        var exceptionlessTeleportConfigValue = GetExceptionlessTeleport(configuration);

        var node = StringAssist.Join(
            [
                ExceptionlessConfigureNode,
                ExceptionlessServerUrlConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        if (exceptionlessTeleportConfigValue.Value && string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                $"开启Exceptionless时, 请配置[{node}]"
            );
        }

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    #endregion

    #region GetExceptionlessApiKey

    /// <summary>
    /// GetExceptionlessServerUrl
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetExceptionlessApiKey(IConfiguration configuration)
    {
        var exceptionlessTeleportConfigValue = GetExceptionlessTeleport(configuration);

        var node = StringAssist.Join(
            [
                ExceptionlessConfigureNode,
                ExceptionlessApiKeyConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        if (exceptionlessTeleportConfigValue.Value && string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                $"开启Exceptionless时, 请配置[{node}]"
            );
        }

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    #endregion
}