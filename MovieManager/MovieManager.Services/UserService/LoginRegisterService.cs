using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieManager.DataAccess.Repositories.Interfaces;
using MovieManager.Domain.Models;
using MovieManager.Helpers.SettingsModels;
using MovieManager.ServiceModels.UserServiceModels;
using MovieManager.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MovieManager.Services.UserService;
public class LoginRegisterService : ILoginRegisterService
{
    private readonly IRepository<User> _userRepository;
    private readonly string _secret;
    private readonly IValidator<RegisterUserDto> _registerUserValidator;

    public LoginRegisterService(IRepository<User> userRepository, IOptions<MovieManagerSettings> options, IValidator<RegisterUserDto> registerUserValidator)
    {
        _userRepository = userRepository;
        _secret = options.Value.MovieManagerSecret;
        _registerUserValidator = registerUserValidator;
    }

    public void Register(RegisterUserDto registerUser)
    {
        var users = _userRepository.Filter(x => x.Username.Equals(registerUser.Username, StringComparison.InvariantCultureIgnoreCase)).Any();
        if (users)
        {
            throw new Exception("User with that username already exists!");
        }
        _registerUserValidator.ValidateAndThrow(registerUser);
        string hashedPassword = HashPassword(registerUser.Password);

        var newUser = new User
        {
            FirstName = registerUser.FirstName,
            LastName = registerUser.LastName,
            Username = registerUser.Username,
            Password = hashedPassword,
        };

        _userRepository.Insert(newUser);
    }

    public UserLoginDto Login(LoginModel user)
    {
        var userFromDb = _userRepository.Filter(x => x.Username.Equals(user.Username, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();
        if (userFromDb == null)
        {
            throw new Exception("Invalid username!");
        }
        var hashedPassword = HashPassword(user.Password);
        if (userFromDb.Password != hashedPassword)
        {
            throw new Exception("Invalid password!");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Name, $"{userFromDb.FirstName} {userFromDb.LastName}"),
                    new Claim(ClaimTypes.NameIdentifier, userFromDb.Id.ToString()),
                    new Claim(ClaimTypes.Role, userFromDb.Role.ToString()),
                    //new Claim("Id", userFromDb.Id.ToString())
                }),
            Expires = DateTime.UtcNow.AddDays(3),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var loggedUser = new UserLoginDto
        {
            FirstName = userFromDb.FirstName,
            LastName = userFromDb.LastName,
            Id = userFromDb.Id,
            Token = tokenHandler.WriteToken(token),
        };

        return loggedUser;
    }

    private static string HashPassword(string password)
    {
        var md5 = new MD5CryptoServiceProvider();
        var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
        var hashedPassword = Encoding.ASCII.GetString(md5data);
        return hashedPassword;
    }
}
