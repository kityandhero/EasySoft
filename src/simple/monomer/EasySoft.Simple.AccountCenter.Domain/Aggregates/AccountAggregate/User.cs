using EasySoft.Core.Domain.Base.Entities.Implementations;

namespace EasySoft.Simple.AccountCenter.Domain.Aggregates.AccountAggregate;

public class User : BaseAggregateOperatorRoot
{
    public string Alias { get; set; }

    public string RealName { get; set; }

    public string LoginName { get; set; }

    public string Password { get; set; }

    public User()
    {
        Alias = "";
        RealName = "";
        LoginName = "";
        Password = "";
    }
}