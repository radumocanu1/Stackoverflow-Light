using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stackoverflow_Light.models;
using Stackoverflow_Light.Services;

namespace Stackoverflow_Light.Controllers;

[ApiController]
[Route("/answer")]
public class AnswerController : ControllerBase
{
    private readonly IAnswerService _answerService;

    public AnswerController(IAnswerService answerService)
    {
        _answerService = answerService;
    }

    [Authorize]
    [HttpPost("/{questionId}")]

public async Task<IActionResult> CreateAnswer(Guid questionId,[FromBody] CreateAnswerRequest createAnswerRequest)
    {
        var token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
        var answer = await _answerService.CreateAnswerAsync(token, questionId, createAnswerRequest);
        return Ok(answer);

    }
}