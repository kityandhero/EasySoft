using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Service.DataTransferObjects.ApiParams;
using Mapster;

namespace EasySoft.Simple.Tradition.Service.ExtensionMethods;

/// <summary>
/// UserExtensions
/// </summary>
public static class UserExtensions
{
    /// <summary>
    /// ToUserDto
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
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