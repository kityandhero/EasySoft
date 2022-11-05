using EasySoft.Simple.AccountCenter.Domain.Aggregates.AccountAggregate;
using EasySoft.Simple.DomainDrivenDesign.Application.Contracts.DataTransferObjects;
using Mapster;

namespace EasySoft.Simple.AccountCenter.Application.Contracts.ExtensionMethods;

public static class UserExtensions
{
    public static UserDto ToUserDto(this User customer)
    {
        var typeAdapterConfig = new TypeAdapterConfig();

        typeAdapterConfig.ForType<User, UserDto>()
            .Map(dest => dest.UserId, src => src.Id);

        var userDto = customer.Adapt<UserDto>();

        customer.Adapt(userDto);

        return userDto;
    }
}