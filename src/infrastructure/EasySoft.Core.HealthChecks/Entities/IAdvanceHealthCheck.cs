namespace EasySoft.Core.HealthChecks.Entities;

public interface IAdvanceHealthCheck
{
    public string GetName();

    public IEnumerable<string>? GetTags();

    public TimeSpan? GetTimeout();

    public Func<CancellationToken, HealthCheckResult> GetCheckAction();
}