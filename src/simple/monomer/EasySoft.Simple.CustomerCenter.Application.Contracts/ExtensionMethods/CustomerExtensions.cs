using EasySoft.Simple.CustomerCenter.Application.Contracts.DataTransferObjects;
using EasySoft.Simple.CustomerCenter.Domain.Aggregates.CustomerAggregate;
using Mapster;

namespace EasySoft.Simple.CustomerCenter.Application.Contracts.ExtensionMethods;

public static class CustomerExtensions
{
    public static CustomerDto ToAuthorDto(this Customer customer)
    {
        var customerDto = customer.Adapt<CustomerDto>();

        customer.Adapt(customerDto);

        return customerDto;
    }
}