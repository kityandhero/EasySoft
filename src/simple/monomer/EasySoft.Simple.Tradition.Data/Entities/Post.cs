using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasySoft.Simple.Tradition.Data.Entities.Bases;

namespace EasySoft.Simple.Tradition.Data.Entities;

[Table("post")]
public class Post : BaseEntity
{
    public int PostId { get; set; }

    [MaxLength(300)]
    public string Title { get; set; }

    public Author? Author { get; set; }

    /// <summary>
    /// 博客标识
    /// </summary>
    public Blog? Blog { get; set; }

    public Post()
    {
        Title = "";
    }
}