using Stackoverflow_Light.Entities;
using Stackoverflow_Light.models;

namespace Stackoverflow_Light.Services;

public interface IQuestionService
{
    Task<IEnumerable<Question>> GetQuestionsAsync(int offset, int size);
    Task<Question> CreateQuestionAsync(string token, QuestionRequest questionRequest);
    Task<Question> GetQuestionAsync(string token, Guid questionId);

    Task DeleteQuestionAsync(string token,Guid questionId);
    Task DeleteQuestionAdminAsync(Guid questionId);

    Task<Question> EditQuestionAsync(string token, Guid questionId, QuestionRequest questionRequest);

}