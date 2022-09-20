using EasySoft.Core.EasyToken.AccessControl;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Simple.Single.Application.Areas.AuthTest.Controllers;

/// <summary>
/// TokenSecretController
/// </summary>
public class TokenSecretController : AreaControllerCore
{
    private readonly ITokenSecret _tokenSecret;

    /// <summary>
    /// TokenSecretController
    /// </summary>
    /// <param name="tokenSecret"></param>
    public TokenSecretController(ITokenSecret tokenSecret)
    {
        _tokenSecret = tokenSecret;
    }

    /// <summary>
    /// Encrypt
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public IActionResult Encrypt(string value)
    {
        var v = _tokenSecret.Encrypt(value);

        return this.Success(v);
    }

    /// <summary>
    /// EncryptWithExpirationTime
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public IActionResult EncryptWithExpirationTime(string value)
    {
        var v = _tokenSecret.EncryptWithExpirationTime(value, TimeSpan.FromHours(8));

        return this.Success(v);
    }

    /// <summary>
    /// Decrypt
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public IActionResult Decrypt(string value)
    {
        var v = _tokenSecret.Decrypt(value);

        return this.Success(new
        {
            value = v
        });
    }

    /// <summary>
    /// DecryptWithExpirationTime
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public IActionResult DecryptWithExpirationTime(string value)
    {
        var v = _tokenSecret.DecryptWithExpirationTime(value, out var expired);

        return this.Success(new
        {
            value = v,
            expired
        });
    }
}