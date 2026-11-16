using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Models;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Infrastructure.Assists;

/// <summary>
/// 基础配置
/// </summary>
public class InfrastructureConfigureAssist
{
    #region Fields

    /// <summary>
    /// InfrastructureConfigureNode
    /// </summary>
    private const string InfrastructureConfigureNode = "Infrastructure";

    /// <summary>
    /// ApplicationIdConfigureNode
    /// </summary>
    private const string ApplicationIdConfigureNode = "ApplicationId";

    /// <summary>
    /// ApplicationSecretConfigureNode
    /// </summary>
    private const string ApplicationSecretConfigureNode = "ApplicationSecret";

    /// <summary>
    /// TransmissionErrorConfigureNode
    /// </summary>
    public const string TransmissionErrorConfigureNode = "TransmissionError";

    /// <summary>
    /// TransmissionSqlLogConfigureNode
    /// </summary>
    public const string TransmissionSqlLogConfigureNode = "TransmissionSqlLog";

    /// <summary>
    /// UseStaticFilesConfigureNode
    /// </summary>
    private const string UseStaticFilesConfigureNode = "UseStaticFiles";

    /// <summary>
    /// WebRootPathConfigureNode
    /// </summary>
    private const string WebRootPathConfigureNode = "WebRootPath";

    #endregion

    #region Properties

    /// <summary>
    /// 配置集
    /// </summary>
    public IConfiguration Configuration { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// 基础配置
    /// </summary>
    /// <param name="configuration"></param>
    public InfrastructureConfigureAssist(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #endregion

    #region Methods

    #region GetApplicationId

    /// <summary>
    /// 获取 ApplicationId
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetApplicationId(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                InfrastructureConfigureNode,
                ApplicationIdConfigureNode
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
    /// 获取 ApplicationId
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetApplicationId()
    {
        return GetApplicationId(Configuration);
    }

    #endregion

    #region GetApplicationSecret

    /// <summary>
    /// 获取 ApplicationSecret
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetApplicationSecret(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                InfrastructureConfigureNode,
                ApplicationSecretConfigureNode
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
    /// 获取 ApplicationSecret
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetApplicationSecret()
    {
        return GetApplicationSecret(Configuration);
    }

    #endregion

    #region GetTransmissionError

    /// <summary>
    /// 是否开启错误传输
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>|
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetTransmissionError(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                InfrastructureConfigureNode,
                TransmissionErrorConfigureNode
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
    /// 是否开启错误传输
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>|
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetTransmissionError()
    {
        return GetTransmissionError(Configuration);
    }

    #endregion

    #region GetTransmissionSqlLog

    /// <summary>
    /// 是否开启 SQL 日志传输
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>|
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetTransmissionSqlLog(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                InfrastructureConfigureNode,
                TransmissionSqlLogConfigureNode
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
    /// 是否开启 SQL 日志传输
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>|
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetTransmissionSqlLog()
    {
        return GetTransmissionSqlLog(Configuration);
    }

    #endregion

    #region GetUseStaticFiles

    /// <summary>
    /// GetUseStaticFiles
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>|
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetUseStaticFiles(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                InfrastructureConfigureNode,
                UseStaticFilesConfigureNode
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
    /// GetUseStaticFiles
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>|
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetUseStaticFiles()
    {
        return GetUseStaticFiles(Configuration);
    }

    #endregion

    #region GetWebRootPath

    /// <summary>
    /// GetWebRootPath
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <returns>Cors Policies</returns>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetWebRootPath(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                InfrastructureConfigureNode,
                WebRootPathConfigureNode
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
    /// GetWebRootPath
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetWebRootPath()
    {
        return GetWebRootPath(Configuration);
    }

    #endregion

    #endregion
}