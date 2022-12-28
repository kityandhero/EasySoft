using EasySoft.Core.Infrastructure;
using EasySoft.Core.UserLogicCore.Entities.implements;
using EasySoft.Core.UserLogicCore.Entities.Interfaces;

namespace EasySoft.Core.UserLogicCore.Assists;

/// <summary>
/// SuperUserAssist
/// </summary>
public static class SuperAdministratorAssist
{
    /// <summary>
    /// SignIn
    /// </summary>
    /// <param name="account">账户</param>
    /// <param name="authorities">权限集合</param>
    /// <returns></returns>
    public static ExecutiveResult<IConsumer> SuperSignIn(IAccount account, out IEnumerable<string> authorities)
    {
        if (account.AccountName != SuperConstCollection.SuperAdministrator ||
            account.Password != SuperConfigAssist.GetPassword())
        {
            authorities = new List<string>();

            return new ExecutiveResult<IConsumer>(ReturnCode.ParamError.ToMessage("超级用户名或密码错误"));
        }

        authorities = new List<string>
        {
            ConstCollection.SuperRoleGuidTag,
            ConstCollection.SelfGuidTag
        };

        return new ExecutiveResult<IConsumer>(ReturnCode.Ok)
        {
            Data = new Consumer().SetWhetherSuper(Whether.Yes)
        };
    }
}