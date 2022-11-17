namespace EasySoft.Core.Infrastructure.Startup;

/// <summary>
/// ExtraAction
/// </summary>
/// <typeparam name="T"></typeparam>
public class ExtraAction<T> : IExtraAction<T>
{
    private string _name;

    private Action<T>? _action;

    /// <summary>
    /// ExtraAction
    /// </summary>
    public ExtraAction()
    {
        _name = "";
    }

    /// <summary>
    /// SetName
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IExtraAction<T> SetName(string name)
    {
        _name = name;

        return this;
    }

    /// <summary>
    /// GetName
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
        return _name;
    }

    /// <summary>
    /// SetAction
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public IExtraAction<T> SetAction(Action<T> action)
    {
        _action = action;

        return this;
    }

    /// <summary>
    /// GetAction
    /// </summary>
    /// <returns></returns>
    public Action<T>? GetAction()
    {
        return _action;
    }
}