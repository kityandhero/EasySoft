namespace EasySoft.Core.IdentityVerification.Operators;

/// <summary>
/// 实际有意义的 操作者
/// </summary>
public abstract class ActualOperator : IActualOperator
{
    private object? _identity;

    private string _token = "";

    /// <summary>
    /// 设置身份标识，仅能设置一次
    /// </summary>
    /// <param name="identity"></param>
    /// <exception cref="Exception"></exception>
    public void SetIdentity(object identity)
    {
        _identity = identity;
    }

    /// <summary>
    /// 获取身份标识, 若尚未进行设置将导致异常
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public object? GetIdentity()
    {
        return _identity;
    }

    /// <summary>
    /// 设置 token, 进行设置一次
    /// </summary>
    /// <param name="token"></param>
    /// <exception cref="Exception"></exception>
    public void SetToken(string token)
    {
        _token = token;
    }

    /// <summary>
    /// 获取 token, 若尚未进行设置将导致异常
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string GetToken()
    {
        return _token;
    }

    public bool IsAnonymous()
    {
        return _identity == null;
    }
}