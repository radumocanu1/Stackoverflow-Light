using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stackoverflow_Light.models;
using Stackoverflow_Light.Services;

namespace Stackoverflow_Light.Controllers;

[ApiController]
[Route("/question")]
public class QuestionController : ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest createQuestionRequest)
    {
        var token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
        var question = await _questionService.CreateQuestionAsync(token, createQuestionRequest);
        return Ok(question);

    }
}