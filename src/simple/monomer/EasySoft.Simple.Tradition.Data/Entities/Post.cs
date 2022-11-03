using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasySoft.Simple.Tradition.Data.Entities.Bases;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.Tradition.Data.Entities;

public class Post : BaseEntity
{
    public string Title { get; set; }

    public long BlogId { get; set; }

    public Post()
    {
        Title = "";
    }
}