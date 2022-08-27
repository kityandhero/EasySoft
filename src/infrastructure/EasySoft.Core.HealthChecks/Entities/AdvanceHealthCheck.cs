using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EasySoft.Core.HealthChecks.Entities;

public abstract class AdvanceHealthCheck : IAdvanceHealthCheck
{
    private readonly string _name;

    private readonly Func<CancellationToken, HealthCheckResult> _checkAction;

    private readonly IEnumerable<string>? _tags;

    private readonly TimeSpan? _timeout;

    protected AdvanceHealthCheck(
        string name,
        Func<CancellationToken, HealthCheckResult> checkAction,
        IEnumerable<string>? tags = null,
        TimeSpan? timeout = default
    )
    {
        _name = name;
        _checkAction = checkAction;
        _tags = tags;
        _timeout = timeout;
    }

    public string GetName()
    {
        return _name;
    }

    public IEnumerable<string>? GetTags()
    {
        return _tags;
    }

    public TimeSpan? GetTimeout()
    {
        return _timeout;
    }

    public Func<CancellationToken, HealthCheckResult> GetCheckAction()
    {
        return _checkAction;
    }
}