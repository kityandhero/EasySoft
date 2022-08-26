using System;
using System.Diagnostics;
using System.Threading;
using EasySoft.UtilityTools.Core.Watchers;
using Microsoft.Extensions.Primitives;

namespace EasySoft.UtilityTools.Core.Providers;

public class ContentProvider : IContentProvider, IDisposable
{
    private readonly Func<ContentWatcher> _contentWatcherFactory;
    private ContentWatcher? _contentWatcher;
    private bool _contentWatcherInitialized;
    private object _contentWatcherLock = new();

    private bool? _usePollingFileWatcher;
    private bool? _useActivePolling;
    private bool _disposed;

    public ContentProvider(string content)
    {
        Content = content;

        _contentWatcherFactory = CreateContentWatcher;
    }

    public bool UseActivePolling
    {
        get => _useActivePolling != null && _useActivePolling.Value;

        set => _useActivePolling = value;
    }

    public bool UsePollingFileWatcher
    {
        get => _usePollingFileWatcher ?? false;
        set => _usePollingFileWatcher = value;
    }

    internal ContentWatcher ContentWatcher
    {
        get
        {
            return LazyInitializer.EnsureInitialized(
                ref _contentWatcher,
                ref _contentWatcherInitialized,
                ref _contentWatcherLock,
                _contentWatcherFactory)!;
        }
        set
        {
            Debug.Assert(!_contentWatcherInitialized);

            _contentWatcherInitialized = true;
            _contentWatcher = value;
        }
    }

    internal ContentWatcher CreateContentWatcher()
    {
        var content = Content;

        return new ContentWatcher(content, UsePollingFileWatcher)
        {
            UseActivePolling = UseActivePolling,
        };
    }

    /// <summary>
    /// Disposes the provider. Change tokens may not trigger after the provider is disposed.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _contentWatcher?.Dispose();
            }

            _disposed = true;
        }
    }

    /// <summary>
    /// The root directory for this instance.
    /// </summary>
    public string Content { get; }

    public IChangeToken Watch(string content)
    {
        return ContentWatcher.CreateContentChangeToken(content);
    }
}