namespace EasySoft.UtilityTools.Core.ChangeTokens;

/// <summary>
/// ContentChangeToken
/// </summary>
public class ContentChangeToken : IContentChangeToken
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    /// <summary>
    /// ActiveChangeCallbacks
    /// </summary>
    public bool ActiveChangeCallbacks => true;

    /// <summary>
    /// HasChanged
    /// </summary>
    public bool HasChanged => _cancellationTokenSource.IsCancellationRequested;

    /// <summary>
    /// RegisterChangeCallback
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public IDisposable RegisterChangeCallback(Action<object> callback, object state)
    {
        return _cancellationTokenSource.Token.Register(callback!, state);
    }

    /// <summary>
    /// PrepareRefresh
    /// </summary>
    public void PrepareRefresh()
    {
        _cancellationTokenSource.Cancel();
    }
}