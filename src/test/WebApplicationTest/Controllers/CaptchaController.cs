using EasySoft.Core.Web.Framework.Controllers;
using EasySoft.UtilityTools.Media.Image;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Controllers;

public class CaptchaController : CustomControllerBase
{
    public IActionResult Index()
    {
        return new Captcha().GetFileResult();
    }
}