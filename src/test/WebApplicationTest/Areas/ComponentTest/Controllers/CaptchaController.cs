using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Media.Image;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

public class CaptchaController : AreaControllerCore
{
    public IActionResult Index()
    {
        return new Captcha().GetFileResult();
    }
}