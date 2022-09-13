using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManager.ServiceModels.UserServiceModels;
using MovieManager.Services.Interfaces;
using Serilog;

namespace MovieManager.App.Controllers;


public class UserController : BaseApiController
{
    private readonly ILoginRegisterService _loginRegisterService;
    private readonly IUserService _userService;
    private readonly IMovieService _movieService;

    public UserController(ILoginRegisterService loginRegisterService, IUserService userService, IMovieService movieService)
    {
        _loginRegisterService = loginRegisterService;
        _userService = userService;
        _movieService = movieService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public ActionResult RegisterUser([FromBody] RegisterUserDto register)
    {
        try
        {
            _loginRegisterService.Register(register);
            Log.Information("Successfully registered user", register);
            return StatusCode(StatusCodes.Status201Created, register);
        }
        catch (Exception ex)
        {
            Log.Error($"Failed to register new user!\n{ex.Message}");
            return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<UserLoginDto> Login([FromBody] LoginModel request)
    {
        try
        {
            var user = _loginRegisterService.Login(request);
            Log.Information($"User {request.Username} loged in successfully.");
            return Ok(user);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
        }
    }

    [HttpGet("greetUser")]
    public ActionResult SayHello()
    {
        return Ok($"Hello user with id {UserId}");
    }

    //[Authorize(Roles = "Admin")]
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}

}
