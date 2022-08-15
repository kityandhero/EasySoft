using Microsoft.AspNetCore.Mvc;

namespace AppTest3.Controllers;

public class Area2Controller : Controller
{
    // GET
    public IActionResult Index()
    {
        return Content("success");
    }
}