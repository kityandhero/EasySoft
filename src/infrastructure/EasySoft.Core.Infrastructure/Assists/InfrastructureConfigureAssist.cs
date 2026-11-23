using EasySoft.UtilityTools.Core.Assists;
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
    /// UseAccessWayTransceiverConfigureNode
    /// </summary>
    public const string UseAccessWayTransceiverConfigureNode = "UseAccessWayTransceiver";

    /// <summary>
    /// UseGeneralLogTransceiverConfigureNode
    /// </summary>
    public const string UseGeneralLogTransceiverConfigureNode = "UseGeneralLogTransceiver";

    /// <summary>
    /// UseChannelCheckTransceiverConfigureNode
    /// </summary>
    public const string UseChannelCheckTransceiverConfigureNode = "UseChannelCheckTransceiver";

    /// <summary>
    /// UseErrorLogTransceiverConfigureNode
    /// </summary>
    public const string UseErrorLogTransceiverConfigureNode = "UseErrorLogTransceiver";

    /// <summary>
    /// UseSqlLogTransceiverConfigureNode
    /// </summary>
    public const string UseSqlLogTransceiverConfigureNode = "UseSqlLogTransceiver";

    /// <summary>
    /// UseStaticFilesConfigureNode
    /// </summary>
    private const string UseStaticFilesConfigureNode = "UseStaticFiles";

    /// <summary>
    /// WebRootPathConfigureNode
    /// </summary>
    private const string WebRootPathConfigureNode = "WebRootPath";

    /// <summary>
    /// DatabaseConfigureNode
    /// </summary>
    private const string DatabaseConfigureNode = "Database";

    /// <summary>
    /// 主库配置节点
    /// </summary>
    private const string MasterDatabaseConfigureNode = "Master";

    /// <summary>
    /// 主库类型配置节点
    /// </summary>
    private const string MasterDatabaseTypeConfigureNode = "Type";

    /// <summary>
    /// 主库连接字符串配置节点
    /// </summary>
    private const string MasterDatabaseConnectionConfigureNode = "Connection";

    /// <summary>
    /// 从库类型配置节点
    /// </summary>
    private const string SlaveDatabaseConfigureNode = "Slave";

    /// <summary>
    /// 从库类型配置节点
    /// </summary>
    private const string SlaveDatabaseTypeConfigureNode = "Type";

    /// <summary>
    /// 从库连接字符串配置节点
    /// </summary>
    private const string SlaveDatabaseConnectionConfigureNode = "Connection";

    /// <summary>
    /// 历史库类型配置节点
    /// </summary>
    private const string HistoryDatabaseConfigureNode = "History";

    /// <summary>
    /// 历史库类型配置节点
    /// </summary>
    private const string HistoryDatabaseTypeConfigureNode = "Type";

    /// <summary>
    /// 历史库连接字符串配置节点
    /// </summary>
    private const string HistoryDatabaseConnectionConfigureNode = "HistoryConnection";

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

    #region GetUseAccessWayTransceiver

    /// <summary>
    /// 是否使用访问收集
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetUseAccessWayTransceiver(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                InfrastructureConfigureNode,
                UseAccessWayTransceiverConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "false";

        var configValue = new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true",
            EffectiveValueDescription = "true|false"
        };

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription()
            );
        }

        return configValue;
    }

    /// <summary>
    /// 是否使用fang'wen收集
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetUseAccessWayTransceiver()
    {
        return GetUseAccessWayTransceiver(Configuration);
    }

    #endregion

    #region GetUseGeneralLogTransceiver

    /// <summary>
    /// 是否使用一般日志收集
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetUseGeneralLogTransceiver(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                InfrastructureConfigureNode,
                UseGeneralLogTransceiverConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "false";

        var configValue = new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true",
            EffectiveValueDescription = "true|false"
        };

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription()
            );
        }

        return configValue;
    }

    /// <summary>
    /// 是否使用一般日志收集
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetUseGeneralLogTransceiver()
    {
        return GetUseGeneralLogTransceiver(Configuration);
    }

    #endregion

    #region GetUseChannelCheckTransceiver

    /// <summary>
    /// 是否使用通道检测收集
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetUseChannelCheckTransceiver(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                InfrastructureConfigureNode,
                UseChannelCheckTransceiverConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "false";

        var configValue = new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true",
            EffectiveValueDescription = "true|false"
        };

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription()
            );
        }

        return configValue;
    }

    /// <summary>
    /// 是否使用通道检测收集
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetUseChannelCheckTransceiver()
    {
        return GetUseChannelCheckTransceiver(Configuration);
    }

    #endregion

    #region GetUseErrorLogTransceiver

    /// <summary>
    /// 是否使用错误日志收集
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetUseErrorLogTransceiver(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                InfrastructureConfigureNode,
                UseErrorLogTransceiverConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "false";

        var configValue = new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true",
            EffectiveValueDescription = "true|false"
        };

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription()
            );
        }

        return configValue;
    }

    /// <summary>
    /// 是否使用错误日志收集
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetUseErrorLogTransceiver()
    {
        return GetUseErrorLogTransceiver(Configuration);
    }

    #endregion

    #region GetUseSqlLogTransceiver

    /// <summary>
    /// 是否使用 SQL 日志收集
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetUseSqlLogTransceiver(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                InfrastructureConfigureNode,
                UseSqlLogTransceiverConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "false";

        var configValue = new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true",
            EffectiveValueDescription = "true|false"
        };

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription()
            );
        }

        return configValue;
    }

    /// <summary>
    /// 是否使用 SQL 日志收集
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetUseSqlLogTransceiver()
    {
        return GetUseSqlLogTransceiver(Configuration);
    }

    #endregion

    #region GetUseStaticFiles

    /// <summary>
    /// GetUseStaticFiles
    /// </summary>
    /// <param name="configuration">配置集</param>
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

        v = string.IsNullOrWhiteSpace(v)
            ? v
            : EnvironmentAssist.GetEnvironment().ContentRootPath.Combine(v);

        var configValue = new ConfigValue<string>
        {
            Node = node,
            Value = v,
            EffectiveValueDescription = ""
        };

        return configValue;
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

    #region GetMasterDatabaseType

    /// <summary>
    /// 获取主库类型
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetMasterDatabaseType(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                DatabaseConfigureNode,
                MasterDatabaseConfigureNode,
                MasterDatabaseTypeConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        var configValue = new ConfigValue<string>
        {
            Node = node,
            Value = v,
            EffectiveValueDescription = ""
        };

        if (!string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription()
            );
        }

        return configValue;
    }

    /// <summary>
    /// 获取主库类型
    /// </summary>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetMasterDatabaseType()
    {
        return GetMasterDatabaseType(Configuration);
    }

    #endregion

    #region GetMasterDatabaseConnection

    /// <summary>
    /// 获取主库连接字符串
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetMasterDatabaseConnection(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                DatabaseConfigureNode,
                MasterDatabaseConfigureNode,
                MasterDatabaseConnectionConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        var configValue = new ConfigValue<string>
        {
            Node = node,
            Value = v,
            EffectiveValueDescription = ""
        };

        if (!string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription()
            );
        }

        return configValue;
    }

    /// <summary>
    /// 获取主库连接字符串
    /// </summary>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetMasterDatabaseConnection()
    {
        return GetMasterDatabaseConnection(Configuration);
    }

    #endregion

    #region GetSlaveDatabaseType

    /// <summary>
    /// 获取从库类型
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetSlaveDatabaseType(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                DatabaseConfigureNode,
                SlaveDatabaseConfigureNode,
                SlaveDatabaseTypeConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        var configValue = new ConfigValue<string>
        {
            Node = node,
            Value = v,
            EffectiveValueDescription = ""
        };

        if (!string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription()
            );
        }

        return configValue;
    }

    /// <summary>
    /// 获取从库类型
    /// </summary>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetSlaveDatabaseType()
    {
        return GetSlaveDatabaseType(Configuration);
    }

    #endregion

    #region GetSlaveDatabaseConnection

    /// <summary>
    /// 获取从库连接字符串
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetSlaveDatabaseConnection(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                DatabaseConfigureNode,
                SlaveDatabaseConfigureNode,
                SlaveDatabaseConnectionConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        var configValue = new ConfigValue<string>
        {
            Node = node,
            Value = v,
            EffectiveValueDescription = ""
        };

        if (!string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription()
            );
        }

        return configValue;
    }

    /// <summary>
    /// 获取从库连接字符串
    /// </summary>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetSlaveDatabaseConnection()
    {
        return GetSlaveDatabaseConnection(Configuration);
    }

    #endregion

    #region GetHistoryDatabaseType

    /// <summary>
    /// 获取从库类型
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetHistoryDatabaseType(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                DatabaseConfigureNode,
                HistoryDatabaseConfigureNode,
                HistoryDatabaseTypeConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        var configValue = new ConfigValue<string>
        {
            Node = node,
            Value = v,
            EffectiveValueDescription = ""
        };

        if (!string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription()
            );
        }

        return configValue;
    }

    /// <summary>
    /// 获取历史库类型
    /// </summary>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetHistoryDatabaseType()
    {
        return GetHistoryDatabaseType(Configuration);
    }

    #endregion

    #region GetHistoryDatabaseConnection

    /// <summary>
    /// 获取历史库连接字符串
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetHistoryDatabaseConnection(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                DatabaseConfigureNode,
                HistoryDatabaseConfigureNode,
                HistoryDatabaseConnectionConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        var configValue = new ConfigValue<string>
        {
            Node = node,
            Value = v,
            EffectiveValueDescription = ""
        };

        if (!string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                configValue.ToErrorDescription()
            );
        }

        return configValue;
    }

    /// <summary>
    /// 获取历史库连接字符串
    /// </summary>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetHistoryDatabaseConnection()
    {
        return GetHistoryDatabaseConnection(Configuration);
    }

    #endregion

    #endregion
}