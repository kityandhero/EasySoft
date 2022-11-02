using System.ComponentModel.DataAnnotations.Schema;
using EasySoft.Core.Domain.Base.Entities.Implementations;

namespace EasySoft.Simple.Domain.Aggregates.AuthorAggregate;

// [Table("author")]
public class Author : BaseAggregateOperatorRoot
{
    // [Column("real_name")]
    public string RealName { get; set; }

    // [Column("login_name")]
    public string LoginName { get; set; }

    // [Column("password")]
    public string Password { get; set; }

    public Author()
    {
        RealName = "";
        LoginName = "";
        Password = "";
    }
}