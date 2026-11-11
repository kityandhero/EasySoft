using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Messages;

namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// ErrorLogMessageAssist
/// </summary>
public static class ErrorLogMessageAssist
{
    /// <summary>
    /// CreateApplicationErrorLog
    /// </summary>
    /// <param name="channel"></param>
    /// <returns></returns>
    public static IErrorLogTransitiveMessage CreateApplicationErrorLog(IChannel channel)
    {
        var c = channel.ToValue();

        var result = new ErrorLogTransitiveMessage
        {
            TriggerChannel = c
        };

        return result;
    }
}