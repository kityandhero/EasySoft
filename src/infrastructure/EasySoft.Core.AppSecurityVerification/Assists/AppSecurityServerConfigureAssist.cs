using EasySoft.UtilityTools.Standard.Models;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.AppSecurityVerification.Assists;

/// <summary>
/// AppSecurityServerConfigureAssist
/// </summary>
public class AppSecurityServerConfigureAssist
{
    #region Fields

    /// <summary>
    /// AppSecurityServerConfigureNode
    /// </summary>
    private const string AppSecurityServerConfigureNode = "AppSecurityServer";

    /// <summary>
    /// HostUrlConfigureNode
    /// </summary>
    private const string HostUrlConfigureNode = "HostUrl";

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
    public AppSecurityServerConfigureAssist(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #endregion

    #region Methods

    #region GetHostUrl

    /// <summary>
    /// 获取 HostUrl
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetHostUrl(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AppSecurityServerConfigureNode,
                HostUrlConfigureNode
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
    /// 获取 HostUrl
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetHostUrl()
    {
        return GetHostUrl(Configuration);
    }

    #endregion

    #endregion
}