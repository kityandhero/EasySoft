using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

public class MiniProFileController : AreaControllerCore
{
    public IActionResult Index()
    {
        return View();
    }
}