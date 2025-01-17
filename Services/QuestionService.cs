using Microsoft.Extensions.Caching.Memory;
using Stackoverflow_Light.Configurations;
using Stackoverflow_Light.Entities;
using Stackoverflow_Light.Exceptions;
using Stackoverflow_Light.models;
using Stackoverflow_Light.Repositories;
using Stackoverflow_Light.Utils;
using Stackoverflow_Light.Utils.Interfaces;

namespace Stackoverflow_Light.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly ITokenClaimsExtractor _tokenClaimsExtractor;
    private readonly IUserService _userService;
    private readonly IMemoryCache _memoryCache; 
    private readonly ILogger<QuestionService> _logger;    
    private readonly int _batchSize;
    

    public QuestionService(IQuestionRepository questionRepository, ITokenClaimsExtractor tokenClaimsExtractor, IUserService userService, ILogger<QuestionService> logger, IMemoryCache memoryCache, IConfiguration configuration)
    {
        _questionRepository = questionRepository;
        _tokenClaimsExtractor = tokenClaimsExtractor;
        _userService = userService;
        _logger = logger;
        _batchSize = configuration.GetValue<int>("DbBatchSize");
        _memoryCache = memoryCache;

    }
    
    public async Task<IEnumerable<QuestionDto>> GetQuestionsAsync(int offset, int size)
    {
        string cacheKey = $"questions_batch_{(offset + size) / _batchSize}";
        if (!_memoryCache.TryGetValue(cacheKey, out List<Question> cachedQuestions))
        {
            cachedQuestions = await _questionRepository.GetQuestionsBatchAsync(offset, _batchSize);
            var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10));

            _memoryCache.Set(cacheKey, cachedQuestions, cacheOptions);
        }

        var selectedQuestions = cachedQuestions.Skip(offset % _batchSize).Take(size);
    
        // Map questions to QuestionDto
        var questionDtos = selectedQuestions.Select(q => new QuestionDto
        {
            Score = q.Score,
            ViewCount = q.ViewsCount,
            Content = q.Content,
            AuthorId = q.UserId,
            QuestionId = q.Id
        });

        return questionDtos;    
    }


    public async Task<Question> CreateQuestionAsync(string token, QuestionRequest questionRequest)
    {
        var subClaim = _tokenClaimsExtractor.ExtractClaim(token, "sub");
        var userId = _userService.GetUserIdFromSubClaimAsync(subClaim);
        var question = new Question
        {
            Content = questionRequest.Content,
            UserId = userId.Result
        };
        return await _questionRepository.CreateQuestionAsync(question);
    }

    public async Task<Question> GetQuestionAsync(string token, Guid questionId)
    {
        var question =  await _questionRepository.GetQuestionAsync(questionId);
        var userId = await _userService.GetUserIdFromSubClaimAsync(_tokenClaimsExtractor.ExtractClaim(token, "sub"));
        await _questionRepository.TryToIncrementViewQuestionCount(question, userId);
        return question;
    }

    public async Task DeleteQuestionAsync(string token,Guid questionId)
    {
        if (!await IsCurrentUserQuestionAuthor(token, questionId))
            throw new OperationNotAllowed(ApplicationConstants.OPERATION_NOT_ALLOWED_MESSAGE);
        await _questionRepository.DeleteQuestionAsync(questionId);
    }

    public async Task DeleteQuestionAdminAsync(Guid questionId)
    {
        await _questionRepository.DeleteQuestionAsync(questionId);
    }

    public async Task<Question> EditQuestionAsync(string token, Guid questionId, QuestionRequest questionRequest)
    {
        if (!await IsCurrentUserQuestionAuthor(token, questionId))
            throw new OperationNotAllowed(ApplicationConstants.OPERATION_NOT_ALLOWED_MESSAGE);
        return await _questionRepository.EditQuestionAsync(questionId, questionRequest);
    }

    private async Task<bool> IsCurrentUserQuestionAuthor(string token, Guid questionId)
    {
        var subClaim = _tokenClaimsExtractor.ExtractClaim(token,"sub");
        var userId = await _userService.GetUserIdFromSubClaimAsync(subClaim);
        return userId == await _questionRepository.GetAuthorIdFromQuestionIdAsync(questionId);
    }


}