namespace EasySoft.UtilityTools.Standard.Exceptions;

/// <summary>
/// 配置错误异常
/// </summary>
public class ConfigErrorException : Exception
{
    /// <summary>
    /// 配置错误异常
    /// </summary>
    public ConfigErrorException() : this("config error")
    {
    }

    /// <summary>
    /// 配置错误异常
    /// </summary>
    /// <param name="message"></param>
    public ConfigErrorException(string message) : base(message)
    {
    }
}