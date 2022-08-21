using System.ComponentModel;
using EasySoft.Core.Config.Utils;
using EasySoft.Core.IdentityVerification.Tokens;
using EasySoft.UtilityTools.Competence;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.Core.IdentityVerification.Operators;

public interface IOperator
{
    public object GetIdentity();

    public IToken GetToken();

    public bool IsAnonymous();
}