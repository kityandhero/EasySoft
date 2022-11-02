using EasySoft.Core.Domain.Base.Entities.Implementations;

namespace EasySoft.Simple.CustomerCenter.Domain.Aggregates.CustomerAggregate;

public class Customer : BaseAggregateOperatorRoot
{
    public string Alias { get; set; }

    public string RealName { get; set; }

    public string LoginName { get; set; }

    public string Password { get; set; }

    public Customer()
    {
        Alias = "";
        RealName = "";
        LoginName = "";
        Password = "";
    }
}