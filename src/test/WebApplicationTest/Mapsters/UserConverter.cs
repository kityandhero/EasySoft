using AutoMapper;

namespace WebApplicationTest.Mapsters;

public class UserConverter : ITypeConverter<UserEntity, UserDto>
{
    public UserDto Convert(UserEntity source, UserDto destination, ResolutionContext context)
    {
        return new UserDto
        {
            Name = source.Name,
            Age = source.Age
        };
    }
}