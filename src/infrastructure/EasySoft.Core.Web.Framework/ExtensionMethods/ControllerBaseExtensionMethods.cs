namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class ControllerBaseExtensionMethods
{
    public static async Task<ExecutiveResult<IActualOperator>> GetActualOperator(this ControllerBase controller)
    {
        var token = await controller.GetTokenAsync();

        if (FlagAssist.TokenMode == UtilityTools.Standard.ConstCollection.EasyToken)
            return EasyToken.Assists.ActualOperatorAssist.ResolveActualOperator(token);

        if (FlagAssist.TokenMode == UtilityTools.Standard.ConstCollection.JsonWebToken)
            return JsonWebToken.Assists.ActualOperatorAssist.ResolveActualOperator(token);

        throw new Exception("unknown token mode");
    }
}