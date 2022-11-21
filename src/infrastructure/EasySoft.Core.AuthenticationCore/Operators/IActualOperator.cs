namespace EasySoft.Core.AuthenticationCore.Operators;

/// <summary>
/// IActualOperator
/// </summary>
public interface IActualOperator
{
    /// <summary>
    /// SetIdentification
    /// </summary>
    /// <param name="identification"></param>
    public void SetIdentification(object identification);

    /// <summary>
    /// SetToken
    /// </summary>
    /// <param name="token"></param>
    public void SetToken(string token);

    /// <summary>
    /// GetIdentification
    /// </summary>
    /// <returns></returns>
    public object? GetIdentification();

    /// <summary>
    /// GetToken
    /// </summary>
    /// <returns></returns>
    public string GetToken();

    /// <summary>
    /// IsAnonymous
    /// </summary>
    /// <returns></returns>
    public bool IsAnonymous();
}