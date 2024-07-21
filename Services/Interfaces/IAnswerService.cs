using Stackoverflow_Light.Entities;
using Stackoverflow_Light.models;

namespace Stackoverflow_Light.Services;

public interface IAnswerService
{
    Task<Answer> CreateAnswerAsync(string token, Guid questionId , AnswerRequest answerRequest);
    Task DeleteAnswerAsync(string token, Guid answerId);
    Task<Answer> EditAnswerAsync(string token, Guid tokenId, AnswerRequest answerRequest);
    Task DeleteAnswerAdminAsync(Guid answerId);

}