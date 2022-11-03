using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;

namespace EasySoft.Simple.Tradition.Data.Entities.Bases;

public abstract class BaseEntity : IEntity<long>
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
}