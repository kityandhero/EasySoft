using EasySoft.Core.HealthChecks.Entities;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebApplicationTest.HealthChecks;

public class HelloHealthCheck : AdvanceHealthCheck
{
    public HelloHealthCheck() : base(
        "hello",
        (CancellationToken _) => HealthCheckResult.Healthy("hello is health")
    )
    {
    }
}