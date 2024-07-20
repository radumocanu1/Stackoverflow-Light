using Stackoverflow_Light.Entities;

namespace Stackoverflow_Light.Repositories;

public interface IAnswerRepository
{
    Task<Answer> CreateAnswerAsync(Answer answer);
}