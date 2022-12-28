namespace EasySoft.Core.AuthenticationCore.Operators;

/// <summary>
/// IActualOperator
/// </summary>
public interface IActualOperator
{
    /// <summary>
    /// SetIdentification
    /// </summary>
    /// <param name="identity"></param>
    void SetIdentity(string identity);

    /// <summary>
    /// SetToken
    /// </summary>
    /// <param name="token"></param>
    void SetToken(string token);

    /// <summary>
    /// GetIdentification
    /// </summary>
    /// <returns></returns>
    string GetIdentity();

    /// <summary>
    /// GetToken
    /// </summary>
    /// <returns></returns>
    string GetToken();

    /// <summary>
    /// IsAnonymous
    /// </summary>
    /// <returns></returns>
    bool IsAnonymous();
}