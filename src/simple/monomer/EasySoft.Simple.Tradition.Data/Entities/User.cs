using EasySoft.Simple.Tradition.Data.Entities.Bases;

namespace EasySoft.Simple.Tradition.Data.Entities;

public class User : BaseEntity
{
    public string Alias { get; set; }

    public string RealName { get; set; }

    public string LoginName { get; set; }

    public string Password { get; set; }

    public User()
    {
        Alias = "";
        RealName = "";
        LoginName = "";
        Password = "";
    }
}