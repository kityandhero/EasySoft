using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Core.Hangfire.Assists;

/// <summary>
/// HangfireConfigAssist
/// </summary>
public class HangfireConfigAssist
{
    #region Fields

    /// <summary>
    /// HangfireConfigureNode
    /// </summary>
    private const string HangfireConfigureNode = "Hangfire";

    /// <summary>
    /// StorageTypeConfigureNode
    /// </summary>
    private const string StorageTypeConfigureNode = "StorageType";

    /// <summary>
    /// StorageConnectionConfigureNode
    /// </summary>
    private const string StorageConnectionConfigureNode = "StorageConnection";

    #endregion

    #region Properties

    public IConfiguration Configuration { get; private set; }

    #endregion

    #region Constructors

    public HangfireConfigAssist(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #endregion

    #region Methods

    #region GetStorageType

    /// <summary>
    /// GetStorageType
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetStorageType(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                HangfireConfigureNode,
                StorageTypeConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效, 有效值 MemoryStorage|SqlServer"
            );
        }

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// GetStorageType
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetStorageType()
    {
        return GetStorageType(Configuration);
    }

    #endregion

    #region GetStorageType

    /// <summary>
    /// GetStorageConnection
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetStorageConnection(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                HangfireConfigureNode,
                StorageConnectionConfigureNode
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
    /// GetStorageConnection
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetStorageConnection()
    {
        return GetStorageConnection(Configuration);
    }

    #endregion

    #endregion
}