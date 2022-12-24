using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using EasySoft.Core.Dapper.Base;

namespace EasySoft.Simple.Dapper.Console.Entities;

[Table("author")]
public class Author : AbstractFunctionEntity<Author>
{
    public Author()
    {
        Id = 0;
        RealName = "";
        LoginName = "";
        Password = "";
    }

    [Column("id")]
    public int Id { get; set; }

    [Column("real_name")]
    public string RealName { get; set; }

    [Column("login_name")]
    public string LoginName { get; set; }

    [Column("password")]
    public string Password { get; set; }
}