using EasySoft.UtilityTools.Standard.Media.Image;

namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// CaptchaExtensions
/// </summary>
public static class CaptchaExtensions
{
    /// <summary>
    /// GetFileResult
    /// </summary>
    /// <param name="captcha"></param>
    /// <returns></returns>
    public static IActionResult GetFileResult(this Captcha captcha)
    {
        var bytes = captcha.GetCaptcha();

        return new FileContentResult(bytes, "image/gif");
    }
}