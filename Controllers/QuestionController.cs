using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stackoverflow_Light.Configurations;
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
    public async Task<IActionResult> CreateQuestion([FromBody] QuestionRequest questionRequest)
    {
        var token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
        var question = await _questionService.CreateQuestionAsync(token, questionRequest);
        return Ok(question);

    }

    [HttpGet("{questionId}")]
    public async Task<IActionResult> GetQuestion(Guid questionId)
    {
        var token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
        return Ok(await _questionService.GetQuestionAsync(token,questionId));
    }

    [Authorize]
    [HttpDelete]
    [Route("{questionId}")]
    public async Task<IActionResult> DeleteQuestion(Guid questionId)
    {
        var token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
        await _questionService.DeleteQuestionAsync(token,questionId);
        return Ok(ApplicationConstants.QUESTION_SUCCESSFULLY_DELETED);
    }
    [Authorize(Policy = "AdminOnly")]
    [HttpDelete]
    [Route("admin/{questionId}")]
    public async Task<IActionResult> DeleteQuestionAdmin(Guid questionId)
    {
        await _questionService.DeleteQuestionAdminAsync(questionId);
        return Ok(ApplicationConstants.QUESTION_SUCCESSFULLY_DELETED);
    }

    [Authorize]
    [HttpPut]
    [Route("{questionId}")]
    public async Task<IActionResult> EditQuestion(Guid questionId, [FromBody] QuestionRequest questionRequest)
    {
        var token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
        return Ok(await _questionService.EditQuestionAsync(token, questionId, questionRequest));
    }
    [HttpGet]
    public async Task<IActionResult> GetQuestions([Required][FromQuery] int offset, [Required][FromQuery] int size)
    {
        var questions = await _questionService.GetQuestionsAsync(offset, size);
        return Ok(questions);
    }
}