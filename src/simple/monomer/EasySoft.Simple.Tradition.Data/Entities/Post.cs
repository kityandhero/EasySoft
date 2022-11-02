using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasySoft.Simple.Tradition.Data.Bases;

namespace EasySoft.Simple.Tradition.Data.Entities;

[Table("post")]
public class Post : BaseEntity
{
    public int PostId { get; set; }

    [MaxLength(300)]
    public string Title { get; set; }

    public Author? Author { get; set; }

    public Post()
    {
        Title = "";
    }
}