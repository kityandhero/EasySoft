using EasySoft.Core.Mvc.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Controllers;

public class ChatController : CustomControllerBase
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}