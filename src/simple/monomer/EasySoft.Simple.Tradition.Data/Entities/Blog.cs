using System.ComponentModel.DataAnnotations;
using EasySoft.Simple.Tradition.Data.Entities.Bases;

namespace EasySoft.Simple.Tradition.Data.Entities;

public class Blog : BaseEntity
{
    [MaxLength(200)]
    public string Name { get; set; }

    /// <summary>
    /// 笔名
    /// </summary>
    public string Pseudonym { get; set; }

    /// <summary>
    /// 座右铭
    /// </summary>
    public string Motto { get; set; }

    public long CustomerId { get; set; }

    public ICollection<Post>? Posts { get; set; }

    public Blog()
    {
        Name = string.Empty;
        Pseudonym = string.Empty;
        Motto = string.Empty;
    }
}