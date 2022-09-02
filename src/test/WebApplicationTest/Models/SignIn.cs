using System.ComponentModel.DataAnnotations;

namespace WebApplicationTest.Models;

public class SignIn
{
    [Required]
    public string LoginName { get; set; }

    public string Password { get; set; }

    public SignIn()
    {
        LoginName = "";
        Password = "";
    }
}