namespace EasySoft.Configuration.ChangeTokens;

public class ContentChangeToken : IContentChangeToken
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public bool ActiveChangeCallbacks => true;

    public bool HasChanged => _cancellationTokenSource.IsCancellationRequested;

    public IDisposable RegisterChangeCallback(Action<object> callback, object state)
    {
        return _cancellationTokenSource.Token.Register(callback!, state);
    }

    public void PrepareRefresh()
    {
        _cancellationTokenSource.Cancel();
    }
}