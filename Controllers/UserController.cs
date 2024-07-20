using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stackoverflow_Light.Services;

namespace Stackoverflow_Light.Controllers;

[ApiController]
[Route("/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [Authorize]
    [HttpPost("/create-mapping")]
    public async Task<IActionResult> CreateMapping()
    {
        var token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
        var user = await _userService.CreateMappingAsync(token);
        return Ok(user);
    }
    
}

