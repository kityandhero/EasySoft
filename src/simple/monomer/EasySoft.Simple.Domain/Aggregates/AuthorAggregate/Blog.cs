using EasySoft.Core.Domain.Base.Entities.Implementations;

namespace EasySoft.Simple.Domain.Aggregates.AuthorAggregate;

// [Table("blog")]
public class Blog : BaseAggregateOperatorRoot
{
    // [Column("title")]
    public string Title { get; set; }

    // [Column("author_id]
    public int AuthorId { get; set; }

    public Blog()
    {
        Title = "";
    }
}