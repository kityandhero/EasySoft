using System.ComponentModel.DataAnnotations.Schema;
using EasySoft.Simple.Tradition.Data.Entities.Bases;

namespace EasySoft.Simple.Tradition.Data.Entities;

[Table("customer")]
public class Customer : BaseEntity
{
    [Column("alias")]
    public string Alias { get; set; }

    [Column("real_name")]
    public string RealName { get; set; }

    [Column("login_name")]
    public string LoginName { get; set; }

    [Column("password")]
    public string Password { get; set; }

    public Customer()
    {
        Alias = "";
        RealName = "";
        LoginName = "";
        Password = "";
    }
}