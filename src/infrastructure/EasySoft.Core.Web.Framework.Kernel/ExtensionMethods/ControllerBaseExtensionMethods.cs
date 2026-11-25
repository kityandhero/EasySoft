using EasySoft.Core.AuthenticationCore.ExtensionMethods;
using EasySoft.Core.AuthenticationCore.Operators;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.Web.Framework.Kernel.ExtensionMethods;

public static class ControllerBaseExtensionMethods
{
    public static ExecutiveResult<IActualOperator> GetActualOperator(this ControllerBase controller)
    {
        var token = controller.GetToken();

        if (FlagAssist.TokenMode == UtilityTools.Standard.ConstCollection.EasyToken)
        {
            return EasyToken.Assists.ActualOperatorAssist.ResolveActualOperator(token);
        }

        if (FlagAssist.TokenMode == UtilityTools.Standard.ConstCollection.JsonWebToken)
        {
            return JsonWebToken.Assists.ActualOperatorAssist.ResolveActualOperator(token);
        }

        throw new Exception("unknown token mode");
    }
}