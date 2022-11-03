using System.ComponentModel.DataAnnotations.Schema;
using EasySoft.Simple.Tradition.Data.Entities.Bases;

namespace EasySoft.Simple.Tradition.Data.Entities;

[Table("author")]
public class Author : BaseEntity
{
    [Column("customer_id")]
    public long CustomerId { get; set; }

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

    /// <summary>
    /// 博客标识
    /// </summary>
    public Blog? Blog { get; set; }

    public int Status { get; set; }

    public Author()
    {
        CustomerId = 0;
        Pseudonym = "";
        Motto = "";
        Status = 0;
    }
}