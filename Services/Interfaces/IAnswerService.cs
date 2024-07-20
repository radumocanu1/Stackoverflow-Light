using Stackoverflow_Light.Entities;
using Stackoverflow_Light.models;

namespace Stackoverflow_Light.Services;

public interface IAnswerService
{
    Task<Answer> CreateAnswerAsync(string token, Guid questionId , CreateAnswerRequest createAnswerRequest);

}