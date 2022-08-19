using EasySoft.Core.Mvc.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Controllers;

public class RemoteErrorLogController : CustomControllerBase
{
    public IActionResult Test()
    {
        throw new Exception("Test Exception");
    }
}