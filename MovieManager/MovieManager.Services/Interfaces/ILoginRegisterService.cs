using MovieManager.ServiceModels.UserServiceModels;

namespace MovieManager.Services.Interfaces;
public interface ILoginRegisterService
{
    void Register(RegisterUserDto user);
    UserLoginDto Login(LoginModel user);
}
