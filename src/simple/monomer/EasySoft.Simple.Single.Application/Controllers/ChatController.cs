using EasySoft.Simple.Single.Application.Common;

namespace EasySoft.Simple.Single.Application.Controllers;

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