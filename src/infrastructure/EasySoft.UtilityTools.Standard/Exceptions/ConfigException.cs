namespace EasySoft.UtilityTools.Standard.Exceptions;

/// <summary>
/// 配置异常
/// </summary>
public class ConfigException : Exception
{
    /// <summary>
    /// 配置异常
    /// </summary>
    public ConfigException() : this("")
    {
    }

    /// <summary>
    /// 配置异常
    /// </summary>
    /// <param name="message"></param>
    public ConfigException(string message) : base(message)
    {
    }
}