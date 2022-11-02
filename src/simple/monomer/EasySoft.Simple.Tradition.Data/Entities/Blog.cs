using System.ComponentModel.DataAnnotations.Schema;
using EasySoft.Simple.Tradition.Data.Bases;

namespace EasySoft.Simple.Tradition.Data.Entities;

[Table("blog")]
public class Blog : BaseEntity
{
    [Column("title")]
    public string Title { get; set; }

    [Column("author_id")]
    public int AuthorId { get; set; }

    public Blog()
    {
        Title = "";
    }
}