using System.ComponentModel.DataAnnotations.Schema;
using EasySoft.Simple.Tradition.Data.Entities.Bases;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.Tradition.Data.Entities;

[Table("blog")]
public class Blog : BaseEntity
{
    [Column("title")]
    public string Title { get; set; }

    /// <summary>
    /// 笔名
    /// </summary>
    [Column("pseudonym")]
    public string Pseudonym { get; set; }

    /// <summary>
    /// 座右铭
    /// </summary>
    [Column("motto")]
    public string Motto { get; set; }

    public long CustomerId { get; set; }

    public ICollection<Post>? Posts { get; set; }

    public Blog()
    {
        Title = string.Empty;
        Pseudonym = string.Empty;
        Motto = string.Empty;
    }
}