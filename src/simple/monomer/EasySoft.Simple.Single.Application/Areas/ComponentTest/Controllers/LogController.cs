using EasySoft.UtilityTools.AspNetCore.Extensions;

namespace EasySoft.Simple.Single.Application.Areas.ComponentTest.Controllers;

/// <summary>
/// LogController
/// </summary>
public class LogController : AreaControllerCore
{
    /// <summary>
    /// Test
    /// </summary>
    /// <returns></returns>
    public IActionResult Test()
    {
        LoggerAssist.Info("info");
        LoggerAssist.Debug("debug");
        LoggerAssist.Warning("warning");
        LoggerAssist.Error("error");
        LoggerAssist.Trace("trace");
        LoggerAssist.Critical("critical");

        LoggerAssist.InfoData(new { message = "text" });
        LoggerAssist.DebugData(new { message = "text" });
        LoggerAssist.WarningData(new { message = "text" });
        LoggerAssist.ErrorData(new { message = "text" });
        LoggerAssist.TraceData(new { message = "text" });
        LoggerAssist.CriticalData(new { message = "text" });

        return this.Success();
    }
}