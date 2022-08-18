using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Controllers;

public class ChatController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}