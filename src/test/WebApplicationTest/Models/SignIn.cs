namespace WebApplicationTest.Models;

public class SignIn
{
    public string UserName { get; set; }

    public string Password { get; set; }

    public SignIn()
    {
        UserName = "";
        Password = "";
    }
}