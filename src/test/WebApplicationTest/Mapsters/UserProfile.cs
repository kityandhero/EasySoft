using AutoMapper;

namespace WebApplicationTest.Mapsters;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserDto>();
    }
}