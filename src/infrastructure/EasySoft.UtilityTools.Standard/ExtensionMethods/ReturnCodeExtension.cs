using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods;

/// <summary>
/// ReturnCodeExtension
/// </summary>
public static class ReturnCodeExtension
{
    /// <summary>
    /// ToMessage
    /// </summary>
    /// <param name="returnCode"></param>
    /// <returns></returns>
    public static ReturnMessage ToMessage(this ReturnCode returnCode)
    {
        return new ReturnMessage(returnCode);
    }

    /// <summary>
    /// ToMessage
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static ReturnMessage ToMessage(this ReturnCode returnCode, string message)
    {
        return new ReturnMessage(returnCode).ToMessage(message);
    }

    /// <summary>
    /// ToMessage
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="success"></param>
    /// <returns></returns>
    public static ReturnMessage ToMessage(this ReturnCode returnCode, bool success)
    {
        return new ReturnMessage(returnCode).ToMessage(success);
    }

    /// <summary>
    /// ToMessage
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public static ReturnMessage ToMessage(this ReturnCode returnCode, int code)
    {
        return new ReturnMessage(returnCode).ToMessage(code);
    }
}