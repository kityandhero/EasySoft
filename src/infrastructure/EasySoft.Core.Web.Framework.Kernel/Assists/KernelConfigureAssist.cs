using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Core.Web.Framework.Kernel.Assists;

public class KernelConfigureAssist
{
    #region Fields

    /// <summary>
    /// KernelConfigureNode
    /// </summary>
    private const string KernelConfigureNode = "Kernel";

    /// <summary>
    /// HttpPortConfigureNode
    /// </summary>
    private const string HttpPortConfigureNode = "HttpPort";

    /// <summary>
    /// HttpsPortConfigureNode
    /// </summary>
    private const string HttpsPortConfigureNode = "HttpsPort";

    /// <summary>
    /// HstsSwitchConfigureNode
    /// </summary>
    private const string HstsSwitchConfigureNode = "Hsts";

    /// <summary>
    /// CompressionConfigureNode
    /// </summary>
    private const string CompressionConfigureNode = "Compression";

    /// <summary>
    /// ForwardedHeadersConfigureNode
    /// </summary>
    private const string ForwardedHeadersConfigureNode = "ForwardedHeaders";

    /// <summary>
    /// HttpRedirectionHttpsConfigureNode
    /// </summary>
    private const string HttpRedirectionHttpsConfigureNode = "HttpRedirectionHttps";

    /// <summary>
    /// CorsPoliciesConfigureNode
    /// </summary>
    private const string CorsPoliciesConfigureNode = "CorsPolicies";

    #endregion

    #region Properties

    public IConfiguration Configuration { get; private set; }

    #endregion

    #region Constructors

    public KernelConfigureAssist(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #endregion

    #region Methods

    #region GetHttpPort

    /// <summary>
    /// 获取 Http Port
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<int> GetHttpPort(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                KernelConfigureNode,
                HttpPortConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "0";

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
    /// 获取 Http Port
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<int> GetHttpPort()
    {
        return GetHttpPort(Configuration);
    }

    #endregion

    #region GetHttpsPort

    /// <summary>
    /// 获取 Https Port
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public static ConfigValue<int> GetHttpsPort(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                KernelConfigureNode,
                HttpsPortConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "0";

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
    /// 获取 Https Port
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<int> GetHttpsPort()
    {
        return GetHttpsPort(Configuration);
    }

    #endregion

    #region GetHstsSwitch

    /// <summary>
    /// 获取 HSTS 开关
    /// </summary>
    /// <remarks>
    /// HSTS 是通过服务器发送一个响应头的方式来控制浏览器操作是否强制使用 Https.
    /// </remarks>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetHstsSwitch(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                KernelConfigureNode,
                HstsSwitchConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "false";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 0/1"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// 获取 HSTS 开关
    /// </summary>
    /// <remarks>
    /// HSTS 是通过服务器发送一个响应头的方式来控制浏览器操作是否强制使用 Https.
    /// </remarks>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetHstsSwitch()
    {
        return GetHstsSwitch(Configuration);
    }

    #endregion

    #region GetCompression

    /// <summary>
    /// 获取压缩传输配置
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetCompression(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                KernelConfigureNode,
                CompressionConfigureNode
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
    /// 获取压缩传输配置
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetCompression()
    {
        return GetCompression(Configuration);
    }

    #endregion

    #region GetForwardedHeaders

    /// <summary>
    /// 反代代理场景下是否转发 Http Header, 默认 true
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetForwardedHeaders(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                KernelConfigureNode,
                ForwardedHeadersConfigureNode
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
    /// 反代代理场景下是否转发 Http Header, 默认 true
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>|
    public ConfigValue<bool> GetForwardedHeaders()
    {
        return GetForwardedHeaders(Configuration);
    }

    #endregion

    #region GetHttpRedirectionHttps

    /// <summary>
    /// 是否将Http请求重定向为Https, 即是否使用 UseHttpsRedirection, 默认关闭
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetHttpRedirectionHttps(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                KernelConfigureNode,
                HttpRedirectionHttpsConfigureNode
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
    /// 是否将Http请求重定向为Https, 即是否使用 UseHttpsRedirection, 默认关闭
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetHttpRedirectionHttps()
    {
        return GetHttpRedirectionHttps(Configuration);
    }

    #endregion

    #region GetCorsPolicies

    /// <summary>
    /// 获取跨域配置
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetCorsPolicies(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                KernelConfigureNode,
                CorsPoliciesConfigureNode
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
    /// 获取跨域配置
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<string> GetCorsPolicies()
    {
        return GetCorsPolicies(Configuration);
    }

    #endregion

    #endregion
}