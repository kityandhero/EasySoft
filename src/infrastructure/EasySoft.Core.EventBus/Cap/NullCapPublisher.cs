using DotNetCore.CAP;

namespace EasySoft.Core.EventBus.Cap;

public class NullCapPublisher : ICapPublisher
{
    public IServiceProvider ServiceProvider { get; } = default!;

    public AsyncLocal<ICapTransaction> Transaction { get; } = default!;

    public void Publish<T>(string name, T? contentObj, string? callbackName = null)
    {
        // Method intentionally left empty.
    }

    public void Publish<T>(string name, T? contentObj, IDictionary<string, string?> headers)
    {
        // Method intentionally left empty.
    }

    public Task PublishAsync<T>(string name, T? contentObj, string? callbackName = null,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task PublishAsync<T>(string name, T? contentObj, IDictionary<string, string?> headers,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}