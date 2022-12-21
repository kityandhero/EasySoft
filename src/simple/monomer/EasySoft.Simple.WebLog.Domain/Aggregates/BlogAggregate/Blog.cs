using EasySoft.Core.Domain.Base.Entities.Implements;

namespace EasySoft.Simple.WebLog.Domain.Aggregates.BlogAggregate;

public class Blog : BaseAggregateOperatorRoot
{
    public string Name { get; set; }

    /// <summary>
    /// 笔名
    /// </summary>
    public string Pseudonym { get; set; }

    /// <summary>
    /// 座右铭
    /// </summary>
    public string Motto { get; set; }

    public long CustomerId { get; set; }

    /// <summary>
    /// 帖子集合
    /// </summary>
    public virtual ICollection<Post> Posts { get; private set; }

    public Blog()
    {
        Name = string.Empty;
        Pseudonym = string.Empty;
        Motto = string.Empty;

        Posts = new List<Post>();
    }
}