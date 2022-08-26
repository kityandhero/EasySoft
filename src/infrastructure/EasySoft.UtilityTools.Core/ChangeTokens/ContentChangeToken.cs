using System;
using System.Diagnostics;
using System.Threading;
using EasySoft.UtilityTools.Core.Watchers;
using Microsoft.Extensions.Primitives;

namespace EasySoft.UtilityTools.Core.ChangeTokens;

public class ContentChangeToken : IContentChangeToken
{
    private string _content;
    private DateTime _previousWriteTimeUtc;
    private DateTime _lastCheckedTimeUtc;
    private bool _hasChanged;
    private CancellationTokenSource _tokenSource;
    private CancellationChangeToken _changeToken;

    public ContentChangeToken(string content)
    {
        _content = content;
    }

    // Internal for unit testing
    internal static TimeSpan PollingInterval { get; set; } = ContentWatcher.DefaultPollingInterval;

    /// <summary>
    /// Always false.
    /// </summary>
    public bool ActiveChangeCallbacks { get; internal set; }

    internal CancellationTokenSource CancellationTokenSource
    {
        get => _tokenSource;
        set
        {
            Debug.Assert(_tokenSource == null, "We expect CancellationTokenSource to be initialized exactly once.");

            _tokenSource = value;
            _changeToken = new CancellationChangeToken(_tokenSource.Token);
        }
    }

    CancellationTokenSource IContentChangeToken.CancellationTokenSource => CancellationTokenSource;

    private DateTime GetLastWriteTimeUtc()
    {
        _fileInfo.Refresh();

        if (!_fileInfo.Exists)
        {
            return DateTime.MinValue;
        }

        return FileSystemInfoHelper.GetFileLinkTargetLastWriteTimeUtc(_fileInfo) ?? _fileInfo.LastWriteTimeUtc;
    }
    
    public bool HasChanged
    {
        get
        {
            if (_hasChanged)
            {
                return _hasChanged;
            }

            var currentTime = DateTime.UtcNow;

            if (currentTime - _lastCheckedTimeUtc < PollingInterval)
            {
                return _hasChanged;
            }

            DateTime lastWriteTimeUtc = GetLastWriteTimeUtc();

            if (_previousWriteTimeUtc != lastWriteTimeUtc)
            {
                _previousWriteTimeUtc = lastWriteTimeUtc;
                _hasChanged = true;
            }

            _lastCheckedTimeUtc = currentTime;
            return _hasChanged;
        }
    }

    /// <summary>
    /// Does not actually register callbacks.
    /// </summary>
    /// <param name="callback">This parameter is ignored</param>
    /// <param name="state">This parameter is ignored</param>
    /// <returns>A disposable object that noops when disposed</returns>
    public IDisposable RegisterChangeCallback(Action<object> callback, object state)
    {
        return _changeToken.RegisterChangeCallback(callback, state);
    }
}