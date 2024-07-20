using Stackoverflow_Light.Entities;

namespace Stackoverflow_Light.Repositories;

public interface IQuestionRepository
{
    Task<Question> CreateQuestionAsync(Question question);
    Task<Question> GetQuestionAsync(Guid questionId);
}