using EasySoft.UtilityTools.Core;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Core.Exceptionless.Assists;

public class ExceptionlessConfigureAssist
{
    #region Properties

    public IConfiguration Configuration { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// LogConfigureAssist
    /// </summary>
    /// <param name="configuration"></param>
    public ExceptionlessConfigureAssist(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #endregion

    #region Methods

    #region GetExceptionlessTeleport

    /// <summary>
    /// GetExceptionlessTeleport
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetExceptionlessTeleport(IConfiguration configuration)
    {
        return ConfigureNodeCollection.GetExceptionlessTeleport(configuration);
    }

    /// <summary>
    /// GetExceptionlessTeleport
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetExceptionlessTeleport()
    {
        return GetExceptionlessTeleport(Configuration);
    }

    #endregion

    #region GetExceptionlessServerUrl

    /// <summary>
    /// GetExceptionlessServerUrl
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetExceptionlessServerUrl(IConfiguration configuration)
    {
        return ConfigureNodeCollection.GetExceptionlessServerUrl(configuration);
    }

    /// <summary>
    /// GetExceptionlessServerUrl
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetExceptionlessServerUrl()
    {
        return GetExceptionlessServerUrl(Configuration);
    }

    #endregion

    #region GetExceptionlessApiKey

    /// <summary>
    /// GetExceptionlessServerUrl
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetExceptionlessApiKey(IConfiguration configuration)
    {
        return ConfigureNodeCollection.GetExceptionlessApiKey(configuration);
    }

    /// <summary>
    /// GetExceptionlessServerUrl
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetExceptionlessApiKey()
    {
        return GetExceptionlessApiKey(Configuration);
    }

    #endregion

    #endregion
}