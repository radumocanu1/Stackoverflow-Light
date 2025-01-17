using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stackoverflow_Light.Services;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(Summary = "Creates an oidc-user-mapping", Description = "Creates a mapping between the OIDC user and local representation based on the token received")]
    [SwaggerResponse(200, "Mapping created successfully")]
    [SwaggerResponse(400, "Claim Extraction Error")]
    public async Task<IActionResult> CreateMapping()
    {
        var token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
        var user = await _userService.CreateMappingAsync(token);
        return Ok(user);
    }
    
}

