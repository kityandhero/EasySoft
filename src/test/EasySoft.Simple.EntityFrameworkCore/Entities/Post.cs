using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasySoft.Simple.EntityFrameworkCore.Entities;

[Table("post")]
public class Post
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