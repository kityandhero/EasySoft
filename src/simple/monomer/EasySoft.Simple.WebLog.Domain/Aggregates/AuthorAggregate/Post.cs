using EasySoft.Core.Domain.Base.Entities.Implementations;

namespace EasySoft.Simple.WebLog.Domain.Aggregates.AuthorAggregate;

public class Post : BaseAggregateOperatorRoot
{
    public int PostId { get; set; }

    public string Title { get; set; }

    public int AuthorId { get; set; }

    public int BlogId { get; set; }

    public Post()
    {
        Title = "";
    }
}