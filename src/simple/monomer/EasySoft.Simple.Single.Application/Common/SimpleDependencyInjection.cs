namespace EasySoft.Simple.Single.Application.Common;

/// <summary>
/// SimpleDependencyInjection
/// </summary>
public class SimpleDependencyInjection : ISimpleDependencyInjection
{
    private readonly int _value = 100;

    /// <summary>
    /// GetValue
    /// </summary>
    /// <returns></returns>
    public int GetValue()
    {
        return _value;
    }
}