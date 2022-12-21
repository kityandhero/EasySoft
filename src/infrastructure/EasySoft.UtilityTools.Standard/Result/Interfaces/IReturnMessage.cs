using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.UtilityTools.Standard.Result.Interfaces;

/// <summary>
/// return message
/// </summary>
public interface IReturnMessage
{
    /// <summary>
    /// Success
    /// </summary>
    bool Success { get; set; }

    /// <summary>
    /// Code
    /// </summary>
    int Code { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    string Message { get; set; }

    /// <summary>
    /// Extra
    /// </summary>
    object Extra { get; set; }

    /// <summary>
    /// AppendMessage
    /// </summary>
    /// <param name="messages"></param>
    /// <returns></returns>
    IReturnMessage AppendMessage(params string[] messages);

    /// <summary>
    /// ToMessage
    /// </summary>
    /// <returns></returns>
    IReturnMessage ToMessage();

    /// <summary>
    /// ToMessage
    /// </summary>
    /// <param name="success"></param>
    /// <returns></returns>
    IReturnMessage ToMessage(bool success);

    /// <summary>
    /// ToMessage
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    IReturnMessage ToMessage(string message);

    /// <summary>
    /// ToMessage
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    IReturnMessage ToMessage(int code);
}