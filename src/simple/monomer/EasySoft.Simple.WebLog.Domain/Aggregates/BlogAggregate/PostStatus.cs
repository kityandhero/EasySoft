using EasySoft.Core.Domain.Base.Entities.Implements;
using EasySoft.Simple.WebLog.Domain.Enums;

namespace EasySoft.Simple.WebLog.Domain.Aggregates.BlogAggregate;

public record PostStatus : BaseValueRecord
{
    public BlogStatusCode Code { get; }

    public string? ChangesReason { get; }

    private PostStatus()
    {
    }

    public PostStatus(BlogStatusCode statusCode, string? reason = null)
    {
        Code = statusCode;
        ChangesReason = reason is null ? string.Empty : reason.Trim();
    }
}