using Stackoverflow_Light.Entities;
using Stackoverflow_Light.models;
using Stackoverflow_Light.Repositories;
using Stackoverflow_Light.Utils;

namespace Stackoverflow_Light.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly TokenClaimsExtractor _tokenClaimsExtractor;
    private readonly IUserService _userService;

    public QuestionService(IQuestionRepository questionRepository, TokenClaimsExtractor tokenClaimsExtractor, IUserService userService)
    {
        _questionRepository = questionRepository;
        _tokenClaimsExtractor = tokenClaimsExtractor;
        _userService = userService;
    }


    public async Task<Question> CreateQuestionAsync(string token, CreateQuestionRequest createQuestionRequest)
    {
        var subClaim = _tokenClaimsExtractor.ExtractClaim(token, "sub");
        var user = _userService.GetUserFromSubClaim(subClaim);
        var question = new Question
        {
            Content = createQuestionRequest.Content,
            User = user.Result
        };
        return await _questionRepository.CreateQuestionAsync(question);
    }

}