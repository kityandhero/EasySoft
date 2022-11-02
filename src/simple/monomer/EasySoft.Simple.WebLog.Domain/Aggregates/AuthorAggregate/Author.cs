using EasySoft.Core.Domain.Base.Entities.Implementations;

namespace EasySoft.Simple.WebLog.Domain.Aggregates.AuthorAggregate;

public class Author : BaseAggregateOperatorRoot
{
    public long CustomerId { get; set; }

    /// <summary>
    /// 笔名
    /// </summary>
    public string Pseudonym { get; set; }

    /// <summary>
    /// 座右铭
    /// </summary>
    public string Motto { get; set; }

    /// <summary>
    /// 博客标识
    /// </summary>
    public Blog? Blog { get; set; }

    public int Status { get; set; }

    public Author()
    {
        CustomerId = 0;
        Pseudonym = "";
        Motto = "";
        Status = 0;
    }
}