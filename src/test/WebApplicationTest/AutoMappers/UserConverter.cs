using AutoMapper;
using EasySoft.Core.AutoMapper.Interfaces;

namespace WebApplicationTest.AutoMappers;

public class UserConverter : IAutoConverter<UserEntity, UserOut>
{
    public UserOut Convert(UserEntity source, UserOut destination, ResolutionContext context)
    {
        return new UserOut
        {
            Name = source.Name,
            Age = source.Age
        };
    }
}