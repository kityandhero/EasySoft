using EasySoft.Core.IdentityVerification.Tokens;

namespace EasySoft.Core.IdentityVerification.Operators;

public interface IActualOperator : IOperator
{
    public void SetIdentity(object identity);

    public void SetToken(IToken token);

  
}