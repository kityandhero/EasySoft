using System.ComponentModel.DataAnnotations;

namespace WebApplicationTest.Models;

/// <summary>
/// SignIn
/// </summary>
public class SignIn
{
    /// <summary>
    /// LoginName
    /// </summary>
    [Required]
    public string LoginName { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// SignIn
    /// </summary>
    public SignIn()
    {
        LoginName = "";
        Password = "";
    }
}