using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasySoft.Core.Infrastructure.Repositories.Entities.Implementations;

namespace EasySoft.Simple.Tradition.Data.Entities.Bases;

public abstract class BaseEntity : Entity
{
    // [Key]
    // [Column("id")]
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    // public override long Id { get; set; }
}