using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stackoverflow_Light.Configurations;
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
    [HttpPost]

    public async Task<IActionResult> CreateAnswer([Required][FromQuery] Guid questionId,[FromBody] AnswerRequest answerRequest)
    {
        var token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
        var answer = await _answerService.CreateAnswerAsync(token, questionId, answerRequest);
        return Ok(answer);

    }
    [Authorize]
    [HttpPut]
    [Route("{answerId}")]
    public async Task<IActionResult> EditAnswer(Guid answerId, [FromBody] AnswerRequest answerRequest)
    {
        var token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
        return Ok(await _answerService.EditAnswerAsync(token, answerId, answerRequest));
    }
    [Authorize(Policy = "AdminOnly")]
    [HttpDelete]
    [Route("admin/{answerId}")]
    public async Task<IActionResult> DeleteAnswerAdmin(Guid answerId)
    {
        await _answerService.DeleteAnswerAdminAsync(answerId);
        return Ok(ApplicationConstants.QUESTION_SUCCESSFULLY_DELETED);
    }
    [Authorize]
    [HttpDelete]
    [Route("{answerId}")]
    public async Task<IActionResult> DeleteAnswer(Guid answerId)
    {
        var token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
        await _answerService.DeleteAnswerAsync(token,answerId);
        return Ok(ApplicationConstants.QUESTION_SUCCESSFULLY_DELETED);
    }
}