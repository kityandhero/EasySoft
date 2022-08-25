using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

public class LogController : AreaControllerCore
{
    public IActionResult Test()
    {
        LogAssist.Debug("info");
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