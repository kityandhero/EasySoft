namespace EasySoft.Core.Web.Framework.ExtensionMethods;

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