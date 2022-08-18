using EasySoft.Core.Web.Framework.AccessControl;
using EasySoft.Core.Web.Framework.Controllers;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Controllers;

public class TokenSecretController : CustomControllerBase
{
    private readonly ITokenSecret _tokenSecret;

    public TokenSecretController(ITokenSecret tokenSecret)
    {
        _tokenSecret = tokenSecret;
    }

    public IActionResult Encrypt(string value)
    {
        var v = _tokenSecret.Encrypt(value);

        return ControllerExtensions.Success(this, v);
    }

    public IActionResult EncryptWithExpirationTime(string value)
    {
        var v = _tokenSecret.EncryptWithExpirationTime(value, TimeSpan.FromHours(8));

        return ControllerExtensions.Success(this, v);
    }

    public IActionResult Decrypt(string value)
    {
        var v = _tokenSecret.Decrypt(value);

        return ControllerExtensions.Success(this, new
        {
            value = v
        });
    }

    public IActionResult DecryptWithExpirationTime(string value)
    {
        var v = _tokenSecret.DecryptWithExpirationTime(value, out var expired);

        return ControllerExtensions.Success(this, new
        {
            value = v,
            expired
        });
    }
}