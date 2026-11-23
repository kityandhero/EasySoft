using EasySoft.Core.AdvanceTrace.Attributes;
using EasySoft.Core.Web.Framework.Kernel.Test.Common;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Core.Web.Framework.Kernel.Test.Controllers;

public class AdvanceTraceController : ControllerCore
{
    [LogRecord]
    public ActionResult TestTrace()
    {
        return Content("success");
    }
}