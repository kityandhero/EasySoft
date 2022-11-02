using EasySoft.Core.Domain.Base.Entities.Implementations;
using EasySoft.Simple.WebLog.Domain.Enums;

namespace EasySoft.Simple.WebLog.Domain.Aggregates.AuthorAggregate;

public record BlogStatus : BaseValueRecord
{
    public BlogStatusCode Code { get; }

    public string? ChangesReason { get; }

    private BlogStatus()
    {
    }

    public BlogStatus(BlogStatusCode statusCode, string? reason = null)
    {
        Code = statusCode;
        ChangesReason = reason is null ? string.Empty : reason.Trim();
    }
}