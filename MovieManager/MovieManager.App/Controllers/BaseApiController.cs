using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieManager.App.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    public int UserId { get => GetAuthorizedUserId(); }

    private int GetAuthorizedUserId()
    {
        string? id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        bool parsed = int.TryParse(id, out int userId);
        if (!parsed)
        {
            throw new Exception("Name Identifier does not exist!");
        }
        return userId;
    }
}
