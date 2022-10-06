using EasySoft.Core.HealthChecks.Entities;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EasySoft.Simple.Single.Application.HealthChecks;

/// <summary>
/// HelloHealthCheck
/// </summary>
public class HelloHealthCheck : AdvanceHealthCheck
{
    /// <summary>
    /// HelloHealthCheck
    /// </summary>
    public HelloHealthCheck() : base(
        "hello",
        (CancellationToken _) => HealthCheckResult.Healthy("hello is health")
    )
    {
    }
}