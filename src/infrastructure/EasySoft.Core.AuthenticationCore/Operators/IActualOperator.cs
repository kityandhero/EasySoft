namespace EasySoft.Core.AuthenticationCore.Operators;

public interface IActualOperator
{
    public void SetIdentification(object identification);

    public void SetToken(string token);

    public object? GetIdentification();

    public string GetToken();

    public bool IsAnonymous();
}