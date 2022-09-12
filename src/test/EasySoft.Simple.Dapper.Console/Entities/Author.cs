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

    public override object GetKeyValue()
    {
        return Id;
    }

    public override object GetPrimaryKeyValue()
    {
        return Id;
    }

    public override void SetPrimaryKeyValue(object value)
    {
        Id = Convert.ToInt32(value);
    }

    public override Expression<Func<Author, object>> GetPrimaryKeyLambda()
    {
        return o => o.Id;
    }
}