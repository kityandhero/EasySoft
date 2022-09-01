namespace EasySoft.Core.Infrastructure.Startup;

public class ExtraAction<T> : IExtraAction<T>
{
    private string _name;

    private Action<T>? _action;

    public ExtraAction()
    {
        _name = "";
    }

    public IExtraAction<T> SetName(string name)
    {
        _name = name;

        return this;
    }

    public string GetName()
    {
        return _name;
    }

    public IExtraAction<T> SetAction(Action<T> action)
    {
        _action = action;

        return this;
    }

    public Action<T>? GetAction()
    {
        return _action;
    }
}