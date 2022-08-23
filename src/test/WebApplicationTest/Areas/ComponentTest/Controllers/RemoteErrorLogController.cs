using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

public class RemoteErrorLogController : AreaControllerCore
{
    public IActionResult Test()
    {
        throw new Exception("Test Exception");
    }
}