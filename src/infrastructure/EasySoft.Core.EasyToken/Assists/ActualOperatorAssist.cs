using EasySoft.Core.EasyToken.AccessControl;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.EasyToken.Assists;

public static class ActualOperatorAssist
{
    public static ExecutiveResult<IActualOperator> ResolveActualOperator(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return new ExecutiveResult<IActualOperator>(ReturnCode.NoData);

        AutofacAssist.Instance.Resolve<ITokenSecretOptions>();

        var tokenSecret = AutofacAssist.Instance.Resolve<ITokenSecret>();

        try
        {
            string identity;
            bool expired;

            try
            {
                identity = tokenSecret.DecryptWithExpirationTime(token, out expired);
            }
            catch (Exception e)
            {
                return new ExecutiveResult<IActualOperator>(
                    ReturnCode.Exception.ToMessage($"token resolve fail, {e.Message}.")
                );
            }

            if (expired)
                return new ExecutiveResult<IActualOperator>(
                    ReturnCode.Exception.ToMessage("凭证无效或凭证已过期")
                );

            var actualOperator = AutofacAssist.Instance.Resolve<IActualOperator>();

            actualOperator.SetToken(token);

            actualOperator.SetIdentity(identity);

            if (actualOperator.IsAnonymous())
                return new ExecutiveResult<IActualOperator>(
                    ReturnCode.Exception.ToMessage("凭证无效或凭证已过期")
                );

            return new ExecutiveResult<IActualOperator>(ReturnCode.Ok)
            {
                Data = actualOperator
            };
        }
        catch (Exception e)
        {
            return new ExecutiveResult<IActualOperator>(
                ReturnCode.Exception.ToMessage($"token resolve fail, {e.Message}.")
            );
        }
    }
}