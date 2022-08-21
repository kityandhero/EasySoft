using EasySoft.Core.Web.Framework.Controllers;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.AreaTest.Controllers;

// [ApiController]
// [Route("Home")]
[Area("AreaTest")]
public class AreaTestController : CustomControllerBase
{
    // [HttpGet("doTest", Name = "DoTest")]
    public ActionResult DoTest()
    {
        var a = this.Param("a", 0);

        return this.Success(new
        {
            value = a
        });
    }
}