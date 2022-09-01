namespace EasySoft.Core.Infrastructure.Startup;

public interface IExtraAction<T>
{
    public IExtraAction<T> SetName(string name);

    public string GetName();

    public IExtraAction<T> SetAction(Action<T> action);

    public Action<T>? GetAction();
}