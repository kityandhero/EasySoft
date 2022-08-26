using System.Collections.Concurrent;
using System.Diagnostics;
using EasySoft.Configuration.ChangeTokens;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace EasySoft.Configuration.Watchers;

public class ContentWatcher : IDisposable
{
    internal static TimeSpan DefaultPollingInterval = TimeSpan.FromSeconds(4);

    private string _content;

    private Timer? _timer;
    private bool _timerInitialized;
    private object _timerLock = new();
    private Func<Timer> _timerFactory;
    private bool _disposed;

    public ContentWatcher(
        string content,
        bool pollForChanges
    )
    {
        _content = content;

        PollForChanges = pollForChanges;

        PollingChangeTokens = new ConcurrentDictionary<IContentChangeToken, IContentChangeToken>();
        _timerFactory = () => NonCapturingTimer.Create(RaiseChangeEvents, state: PollingChangeTokens,
            dueTime: TimeSpan.Zero, period: DefaultPollingInterval);
    }

    public string GetOriginalContent()
    {
        return _content;
    }

    public void SetOriginalContent(string content)
    {
        _content = content;
    }

    internal bool PollForChanges { get; }

    internal bool UseActivePolling { get; set; }

    internal ConcurrentDictionary<IContentChangeToken, IContentChangeToken> PollingChangeTokens { get; }

    public IChangeToken CreateContentChangeToken(string originalContent, string content)
    {
        IChangeToken changeToken = GetOrAddChangeToken(originalContent, content);

        return changeToken;
    }

    private IChangeToken GetOrAddChangeToken(string originalContent, string content)
    {
        if (UseActivePolling)
        {
            LazyInitializer.EnsureInitialized(ref _timer, ref _timerInitialized, ref _timerLock, _timerFactory);
        }

        var changeToken = GetOrAddContentChangeToken(originalContent, content);

        return changeToken;
    }

    internal IChangeToken GetOrAddContentChangeToken(string originalContent, string content)
    {
        IChangeToken changeToken = NullChangeToken.Singleton;

        if (originalContent != content)
        {
            changeToken = new ContentChangeToken();
        }

        return changeToken;
    }

    /// <summary>
    /// Disposes the provider. Change tokens may not trigger after the provider is disposed.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes the provider.
    /// </summary>
    /// <param name="disposing"><c>true</c> is invoked from <see cref="IDisposable.Dispose"/>.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _timer?.Dispose();
            }

            _disposed = true;
        }
    }

    internal static void RaiseChangeEvents(object? state)
    {
        Debug.Assert(state != null);

        // Iterating over a concurrent bag gives us a point in time snapshot making it safe
        // to remove items from it.
        var changeTokens = (ConcurrentDictionary<IContentChangeToken, IContentChangeToken>)state;

        foreach (KeyValuePair<IContentChangeToken, IContentChangeToken> item in changeTokens)
        {
            IContentChangeToken token = item.Key;

            if (!token.HasChanged)
            {
                continue;
            }

            if (!changeTokens.TryRemove(token, out _))
            {
                // Move on if we couldn't remove the item.
                continue;
            }

            // We're already on a background thread, don't need to spawn a background Task to cancel the CTS
            try
            {
                token.CancellationTokenSource!.Cancel();
            }
            catch
            {
            }
        }
    }
}