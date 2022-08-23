namespace EasySoft.Core.AuthenticationCore.Operators;

public interface IActualOperator
{
    public void SetIdentity(object identity);

    public void SetToken(string token);

    public object? GetIdentity();

    public string GetToken();

    public bool IsAnonymous();
}