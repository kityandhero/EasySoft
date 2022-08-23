﻿using EasySoft.UtilityTools.Standard.Media.Image;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.UtilityTools.Core.ExtensionMethods;

public static class CaptchaExtensions
{
    public static IActionResult GetFileResult(this Captcha captcha)
    {
        var bytes = captcha.GetCaptcha();

        return new FileContentResult(bytes, "image/gif");
    }
}