using EasySoft.UtilityTools.Core.Extensions;

namespace EasySoft.UtilityTools.AspNetCore.Extensions;

/// <summary>
/// LoggerExtensions
/// </summary>
public static class LoggerExtensions
{
    #region Middleware

    /// <summary>
    /// LogMiddlewareInvokeAsyncBefore
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="logs"></param>
    /// <typeparam name="TMiddleware"></typeparam>
    public static void LogMiddlewareInvokeAsyncBefore<TMiddleware>(
        this ILogger logger,
        IHostEnvironment environment,
        params string[] logs
    ) where TMiddleware : IMiddleware
    {
        if (!environment.IsDevelopment())
        {
            return;
        }

        logger.LogAdvanceExecute($"{typeof(TMiddleware).Name} InvokeAsync Before");

        logger.LogAdvanceTrace(logs);
    }

    /// <summary>
    /// LogMiddlewareInvokeAsyncAfter
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="logs"></param>
    /// <typeparam name="TMiddleware"></typeparam>
    public static void LogMiddlewareInvokeAsyncAfter<TMiddleware>(
        this ILogger logger,
        IWebHostEnvironment environment,
        params string[] logs
    ) where TMiddleware : IMiddleware
    {
        if (!environment.IsDevelopment())
        {
            return;
        }

        logger.LogAdvanceExecute($"{typeof(TMiddleware).Name} InvokeAsync After");

        logger.LogAdvanceTrace(logs);
    }

    #endregion
}