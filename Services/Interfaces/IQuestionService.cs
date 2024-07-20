using Stackoverflow_Light.Entities;
using Stackoverflow_Light.models;

namespace Stackoverflow_Light.Services;

public interface IQuestionService
{
    Task<Question> CreateQuestionAsync(string token, CreateQuestionRequest createQuestionRequest);
}