using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Core.MiniProfiler.Assists;

/// <summary>
/// MiniProFileConfigureAssist
/// </summary>
public class MiniProFileConfigureAssist
{
    #region Fields

    /// <summary>
    /// MiniProFileConfigureNode
    /// </summary>
    private const string MiniProFileConfigureNode = "MiniProFile";

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
    public MiniProFileConfigureAssist(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #endregion

    #region Methods

    #region GetMiniProFile

    /// <summary>
    /// MiniProFile 开关
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetMiniProFile(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                MiniProFileConfigureNode
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
    /// MiniProFile 开关
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetMiniProFile()
    {
        return GetMiniProFile(Configuration);
    }

    #endregion

    #endregion
}