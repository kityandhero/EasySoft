using EasySoft.UtilityTools.Standard.Media.Image;

namespace EasySoft.Simple.Single.Application.Areas.ComponentTest.Controllers;

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