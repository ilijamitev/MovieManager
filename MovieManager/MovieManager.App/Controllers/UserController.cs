using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManager.ServiceModels.UserServiceModels;
using MovieManager.Services.Interfaces;

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
            return StatusCode(StatusCodes.Status201Created, register);
        }
        catch (Exception ex)
        {
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
            return Ok(user);
        }
        catch (Exception ex)
        {
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
