using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Media.Image;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

/// <summary>
/// CaptchaController
/// </summary>
public class CaptchaController : AreaControllerCore
{
    /// <summary>
    /// Index
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return new Captcha().GetFileResult();
    }
}