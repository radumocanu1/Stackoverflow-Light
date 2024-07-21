using Stackoverflow_Light.Entities;
using Stackoverflow_Light.models;

namespace Stackoverflow_Light.Repositories;

public interface IAnswerRepository
{
    Task<Answer> CreateAnswerAsync(Answer answer);
    Task<Answer> EditAnswerAsync(Guid answerId,AnswerRequest answerRequest);

    Task DeleteAnswerAsync(Guid answerId);
    Task<Guid> GetAuthorIdFromAnswerIdAsync(Guid questionId);

}