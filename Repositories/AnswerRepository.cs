using Stackoverflow_Light.Configurations;
using Stackoverflow_Light.Entities;

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
    
}