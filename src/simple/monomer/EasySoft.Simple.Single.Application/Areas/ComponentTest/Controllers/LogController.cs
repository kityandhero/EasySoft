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
        LogAssist.Info("info");
        LogAssist.Debug("debug");
        LogAssist.Warning("warning");
        LogAssist.Error("error");
        LogAssist.Trace("trace");
        LogAssist.Critical("critical");

        LogAssist.InfoData(new { message = "text" });
        LogAssist.DebugData(new { message = "text" });
        LogAssist.WarningData(new { message = "text" });
        LogAssist.ErrorData(new { message = "text" });
        LogAssist.TraceData(new { message = "text" });
        LogAssist.CriticalData(new { message = "text" });

        return this.Success();
    }
}