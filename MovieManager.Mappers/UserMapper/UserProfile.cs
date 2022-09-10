using AutoMapper;
using MovieManager.Domain.Models;
using MovieManager.ServiceModels.UserServiceModels;

namespace MovieManager.Mappers.UserMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserDto, User>();
        CreateMap<User, UserLoginDto>();
    }
}
