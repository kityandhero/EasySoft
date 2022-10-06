using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasySoft.Simple.EntityFrameworkCore.Bases;

namespace EasySoft.Simple.EntityFrameworkCore.Entities;

[Table("blog")]
public class Blog : BaseEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BlogId { get; set; }

    [Column("title")]
    public string Title { get; set; }

    public int AuthorId { get; set; }

    [Column("create_time")]
    public DateTime CreateTime { get; set; }

    public Blog()
    {
        Title = "";
    }
}