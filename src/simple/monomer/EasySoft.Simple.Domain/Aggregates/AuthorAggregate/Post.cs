using EasySoft.Core.Domain.Base.Entities.Implementations;

namespace EasySoft.Simple.Domain.Aggregates.AuthorAggregate;

// [Table("post")]
public class Post : BaseAggregateOperatorRoot
{
    public int PostId { get; set; }

    // [MaxLength(300)]
    public string Title { get; set; }

    // [Column("author_id]
    public int AuthorId { get; set; }

    public Post()
    {
        Title = "";
    }
}