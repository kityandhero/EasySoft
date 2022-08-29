using EasySoft.Core.AutoMapper.Profiles;

namespace WebApplicationTest.AutoMappers;

public class UserProfile : ProfileCore
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserOut>()
            .ConvertUsing<UserConverter>();
    }
}