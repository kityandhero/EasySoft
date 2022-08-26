namespace EasySoft.Configuration.ChangeTokens;

public class ContentChangeToken : IContentChangeToken
{
    public readonly CancellationTokenSource Cts = new();

    public bool ActiveChangeCallbacks => true;

    public bool HasChanged => Cts.IsCancellationRequested;

    public IDisposable RegisterChangeCallback(Action<object> callback, object state)
    {
        return Cts.Token.Register(callback!, state);
    }

    public CancellationTokenSource CancellationTokenSource => Cts;
}