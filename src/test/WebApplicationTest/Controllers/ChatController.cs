using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.Common;

namespace WebApplicationTest.Controllers;

/// <summary>
/// ChatController
/// </summary>
public class ChatController : ControllerCore
{
    // GET
    /// <summary>
    /// Index
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return View();
    }
}