using Microsoft.AspNetCore.Mvc;

namespace Stackoverflow_Light.Controllers;

[ApiController]
[Route("/[controller]")]
public class TestController
{
    [HttpGet]
    public string Test()
    {
        return "It works.";
    }
    
}