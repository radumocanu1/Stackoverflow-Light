using Microsoft.EntityFrameworkCore;
using Stackoverflow_Light.Configurations;
using Stackoverflow_Light.Entities;
using Stackoverflow_Light.Exceptions;
using Stackoverflow_Light.models;

namespace Stackoverflow_Light.Repositories;

public class AnswerRepository : IAnswerRepository
{
    private readonly ApplicationDbContext _context;

    public AnswerRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Answer> CreateAnswerAsync(Answer answer)
    {
        _context.Answers.Add(answer);
        await _context.SaveChangesAsync();
        return answer;
    }

    public async Task<Answer> EditAnswerAsync(Guid answerId, AnswerRequest answerRequest)
    {
        var answer = await FindAnswerAsyncById(answerId);
        answer.Content = answerRequest.Content;
        await _context.SaveChangesAsync();
        return answer;
    }
        
    public async Task DeleteAnswerAsync(Guid answerId)
    {
        var answer = await FindAnswerAsyncById(answerId);
        _context.Answers.Remove(answer);
        await _context.SaveChangesAsync();
    }

    public async Task<Guid> GetAuthorIdFromAnswerIdAsync(Guid answerId)
    {
        var userId = await _context.Answers
            .Where(a => a.Id == answerId)
            .Select(a => a.UserId)
            .FirstOrDefaultAsync();

        if (userId == Guid.Empty)
            throw new EntityNotFound(string.Format(ApplicationConstants.ANSWER_NOT_FOUND_MESSAGE, answerId.ToString()));

        return userId; 
    }

    private async Task<Answer> FindAnswerAsyncById(Guid answerId)
    {
        var answer = await _context.Answers.FindAsync(answerId);
        if (answer == null)
            throw new EntityNotFound(string.Format(ApplicationConstants.ANSWER_NOT_FOUND_MESSAGE,answerId.ToString()));
        return answer;
    }


    
}