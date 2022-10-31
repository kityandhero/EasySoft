using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasySoft.Simple.EntityFrameworkCore.Bases;

namespace EasySoft.Simple.EntityFrameworkCore.Entities;

[Table("author")]
public class Author : BaseEntity
{
    [Column("real_name")]
    public string RealName { get; set; }

    [Column("login_name")]
    public string LoginName { get; set; }

    [Column("password")]
    public string Password { get; set; }

    public Author()
    {
        RealName = "";
        LoginName = "";
        Password = "";
    }
}