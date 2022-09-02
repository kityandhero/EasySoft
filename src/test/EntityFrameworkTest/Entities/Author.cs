using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkTest.Bases;

namespace EntityFrameworkTest.Entities;

public class Author : BaseEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AuthorId { get; set; }

    [Column("real_name")]
    public string RealName { get; set; }

    [Column("login_name")]
    public string LoginName { get; set; }

    [Column("password")]
    public string Password { get; set; }

    public Author()
    {
        RealName = "";
        LoginName = "";
        Password = "";
    }
}