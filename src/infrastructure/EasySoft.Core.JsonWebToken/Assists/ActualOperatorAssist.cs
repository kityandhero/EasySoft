namespace EasySoft.Core.JsonWebToken.Assists;

public static class ActualOperatorAssist
{
    public static ExecutiveResult<IActualOperator> ResolveActualOperator(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return new ExecutiveResult<IActualOperator>(ReturnCode.NoData);

        ClaimsPrincipal claimsPrincipal;

        try
        {
            claimsPrincipal = TokenAssist.ValidateToken(token);
        }
        catch (Exception e)
        {
            return new ExecutiveResult<IActualOperator>(
                ReturnCode.Exception.ToMessage($"token resolve fail, {e.Message}.")
            );
        }

        var first = claimsPrincipal.Claims.FirstOrDefault(o =>
            o.Type == JwtRegisteredClaimSpecialNames.EasySoftTokenIdentity
        );

        if (first == null)
            return new ExecutiveResult<IActualOperator>(
                ReturnCode.Exception.ToMessage("凭证无效或凭证已过期")
            );

        var identification = first.Value;

        var actualOperator = AutofacAssist.Instance.Resolve<IActualOperator>();

        actualOperator.SetToken(token);

        actualOperator.SetIdentification(identification);

        if (actualOperator.IsAnonymous())
            return new ExecutiveResult<IActualOperator>(
                ReturnCode.Exception.ToMessage("凭证无效或凭证已过期")
            );

        return new ExecutiveResult<IActualOperator>(ReturnCode.Ok)
        {
            Data = actualOperator
        };
    }
}