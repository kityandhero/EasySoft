using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using EasySoft.UtilityTools.Core.ChangeTokens;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Primitives;

namespace EasySoft.UtilityTools.Core.Watchers;

public class ContentWatcher : IDisposable
{
    internal static TimeSpan DefaultPollingInterval = TimeSpan.FromSeconds(4);

    private readonly ConcurrentDictionary<string, ChangeTokenInfo> _contentTokenLookup =
        new(StringComparer.OrdinalIgnoreCase);

    private readonly string _content;

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

    internal bool PollForChanges { get; }

    internal bool UseActivePolling { get; set; }

    internal ConcurrentDictionary<IContentChangeToken, IContentChangeToken> PollingChangeTokens { get; }

    /// <summary>
    ///     <para>
    ///     Creates an instance of <see cref="IChangeToken" /> for all files and directories that match the
    ///     <paramref name="content" />
    ///     </para>
    ///     <para>
    ///     Globbing patterns are relative to the root directory given in the constructor
    ///     <seealso cref="PhysicalFilesWatcher(string, FileSystemWatcher, bool)" />. Globbing patterns
    ///     are interpreted by <seealso cref="Matcher" />.
    ///     </para>
    /// </summary>
    /// <param name="content">A globbing pattern for files and directories to watch</param>
    /// <returns>A change token for all files that match the filter</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="content" /> is null</exception>
    public IChangeToken CreateContentChangeToken(string content)
    {
        IChangeToken changeToken = GetOrAddChangeToken(content);

        return changeToken;
    }

    private IChangeToken GetOrAddChangeToken(string content)
    {
        if (UseActivePolling)
        {
            LazyInitializer.EnsureInitialized(ref _timer, ref _timerInitialized, ref _timerLock, _timerFactory);
        }

        var changeToken = GetOrAddContentChangeToken(content);

        return changeToken;
    }

    internal IChangeToken GetOrAddContentChangeToken(string content)
    {
        if (!_contentTokenLookup.TryGetValue(content, out ChangeTokenInfo tokenInfo))
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationChangeToken = new CancellationChangeToken(cancellationTokenSource.Token);

            tokenInfo = new ChangeTokenInfo(cancellationTokenSource, cancellationChangeToken);
            tokenInfo = _contentTokenLookup.GetOrAdd(content, tokenInfo);
        }

        IChangeToken changeToken = tokenInfo.ChangeToken;

        if (PollForChanges)
        {
            // The expiry of CancellationChangeToken is controlled by this type and consequently we can cache it.
            // PollingFileChangeToken on the other hand manages its own lifetime and consequently we cannot cache it.
            var pollingChangeToken = new ContentChangeToken(content);

            if (UseActivePolling)
            {
                pollingChangeToken.ActiveChangeCallbacks = true;
                pollingChangeToken.CancellationTokenSource = new CancellationTokenSource();
                PollingChangeTokens.TryAdd(pollingChangeToken, pollingChangeToken);
            }

            changeToken = new CompositeChangeToken(
                new[]
                {
                    changeToken,
                    pollingChangeToken,
                });
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

    private readonly struct ChangeTokenInfo
    {
        public ChangeTokenInfo(
            CancellationTokenSource tokenSource,
            CancellationChangeToken changeToken)
            : this(tokenSource, changeToken, matcher: null)
        {
        }

        public ChangeTokenInfo(
            CancellationTokenSource tokenSource,
            CancellationChangeToken changeToken,
            Matcher? matcher)
        {
            TokenSource = tokenSource;
            ChangeToken = changeToken;
            Matcher = matcher;
        }

        public CancellationTokenSource TokenSource { get; }

        public CancellationChangeToken ChangeToken { get; }

        public Matcher? Matcher { get; }
    }
}