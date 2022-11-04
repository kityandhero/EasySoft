using EasySoft.Core.Domain.Base.Entities.Implementations;

namespace EasySoft.Simple.WebLog.Domain.Aggregates.BlogAggregate;

public class Post : BaseAggregateOperatorRoot
{
    public string Title { get; set; }

    public long BlogId { get; set; }

    /// <summary>
    /// 博客标识
    /// </summary>
    public Blog? Blog { get; set; }

    public Post()
    {
        Title = "";
    }
}