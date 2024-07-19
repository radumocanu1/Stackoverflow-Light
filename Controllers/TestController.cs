using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Stackoverflow_Light.Controllers;

[ApiController]
[Route("/[controller]")]
public class TestController
{
    [HttpGet]
    [Authorize]
    public string Test()
    {
        return "It works.";
    }
    [HttpGet("admin")]
    [Authorize(Policy = "AdminOnly")]
    public string AdminTest()
    {
        return "Admin access granted.";
    }
    
}