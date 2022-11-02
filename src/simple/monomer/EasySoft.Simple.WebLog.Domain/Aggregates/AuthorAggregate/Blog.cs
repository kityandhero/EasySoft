using EasySoft.Core.Domain.Base.Entities.Implementations;

namespace EasySoft.Simple.WebLog.Domain.Aggregates.AuthorAggregate;

public class Blog : BaseAggregateOperatorRoot
{
    public string Title { get; set; }

    public Author Author { get; set; }

    public BlogStatus Status { get; private set; }

    /// <summary>
    /// 帖子集合
    /// </summary>
    public virtual ICollection<Post> Posts { get; private set; }

    public Blog()
    {
        Title = "";
    }
}