using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.Common;

namespace WebApplicationTest.Controllers;

public class ChatController : ControllerCore
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}