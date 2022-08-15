using Microsoft.AspNetCore.Mvc;

namespace FrameworkTestApp.Controllers;

public class Area2Controller : Controller
{
    // GET
    public IActionResult Index()
    {
        return Content("success");
    }
}