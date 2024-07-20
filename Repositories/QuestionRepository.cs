using Microsoft.EntityFrameworkCore;
using Stackoverflow_Light.Configurations;
using Stackoverflow_Light.Entities;

namespace Stackoverflow_Light.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly ApplicationDbContext _context;

    public QuestionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Question> CreateQuestionAsync(Question question)
    {
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
        return question;
    }

    public async Task<Question> GetQuestionAsync(Guid questionId)
    {
        return await _context.Questions
            .Include(q => q.User)
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == questionId);
    }

}