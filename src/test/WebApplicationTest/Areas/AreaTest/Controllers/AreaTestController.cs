using EasySoft.Core.Web.Framework.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.AreaTest.Controllers;

// [ApiController]
// [Route("Home")]
[Area("AreaTest")]
public class AreaTestController : Controller
{
    // [HttpGet("doTest", Name = "DoTest")]
    public ActionResult DoTest()
    {
        var a = this.Param<int>("a", 0);

        return this.Success(new
        {
            value = a
        });
    }
}