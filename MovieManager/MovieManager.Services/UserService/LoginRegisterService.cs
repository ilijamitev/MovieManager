using Microsoft.Extensions.Options;
using MovieManager.DataAccess.Repositories.Interfaces;
using MovieManager.Domain.Models;
using MovieManager.Helpers.SettingsModels;
using MovieManager.ServiceModels.UserServiceModels;
using MovieManager.Services.Interfaces;

namespace MovieManager.Services.UserService;
public class LoginRegisterService : ILoginRegisterService
{
    private readonly IRepository<User> _userRepository;
    private readonly string _secret;

    public LoginRegisterService(IRepository<User> userRepository, IOptions<MovieManagerSettings> options)
    {
        _userRepository = userRepository;
        _secret = options.Value.MovieManagerSecret;
    }

    public void Register(RegisterUserDto user)
    {

    }

    public UserLoginDto Login(LoginModel user)
    {





        return new UserLoginDto
        {

        };
    }

}
