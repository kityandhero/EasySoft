using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Core.PermissionVerification.Assists;

/// <summary>
/// AuthorizationConfigureAssist
/// </summary>
public class AuthorizationConfigureAssist
{
    #region Fields

    /// <summary>
    /// AuthorizationConfigureNode
    /// </summary>
    private const string AuthorizationConfigureNode = "Authorization";

    /// <summary>
    /// AccessWayDetectSwitchConfigureNode
    /// </summary>
    private const string AccessWayDetectSwitchConfigureNode = "AccessWayDetectSwitch";

    /// <summary>
    /// ServerHostUrlConfigureNode
    /// </summary>
    private const string ServerHostUrlConfigureNode = "ServerHostUrl";

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
    public AuthorizationConfigureAssist(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #endregion

    #region Methods

    #region GetAccessWayDetectSwitch

    /// <summary>
    /// GetAccessWayDetectSwitch
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetAccessWayDetectSwitch(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthorizationConfigureNode,
                AccessWayDetectSwitchConfigureNode
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
    /// GetAccessWayDetectSwitch
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetAccessWayDetectSwitch()
    {
        return GetAccessWayDetectSwitch(Configuration);
    }

    #endregion

    #region GetServerHostUrl

    /// <summary>
    /// 获取 HostUrl
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetServerHostUrl(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                AuthorizationConfigureNode,
                ServerHostUrlConfigureNode
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
    public ConfigValue<string> GetServerHostUrl()
    {
        return GetServerHostUrl(Configuration);
    }

    #endregion

    #endregion
}