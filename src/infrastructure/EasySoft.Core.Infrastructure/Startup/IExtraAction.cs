namespace EasySoft.Core.Infrastructure.Startup;

/// <summary>
/// IExtraAction
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IExtraAction<T>
{
    /// <summary>
    /// SetName
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IExtraAction<T> SetName(string name);

    /// <summary>
    /// GetName
    /// </summary>
    /// <returns></returns>
    public string GetName();

    /// <summary>
    /// SetAction
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public IExtraAction<T> SetAction(Action<T> action);

    /// <summary>
    /// GetAction
    /// </summary>
    /// <returns></returns>
    public Action<T>? GetAction();
}