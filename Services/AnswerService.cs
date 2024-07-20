using Stackoverflow_Light.Entities;
using Stackoverflow_Light.models;
using Stackoverflow_Light.Repositories;
using Stackoverflow_Light.Utils;

namespace Stackoverflow_Light.Services;

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _answerRepository;
    private readonly TokenClaimsExtractor _tokenClaimsExtractor;
    private readonly IUserService _userService;

    public AnswerService(IAnswerRepository answerRepository, TokenClaimsExtractor tokenClaimsExtractor, IUserService userService)
    {
        _answerRepository = answerRepository;
        _tokenClaimsExtractor = tokenClaimsExtractor;
        _userService = userService;
    }
    public async Task<Answer> CreateAnswerAsync(string token, Guid questionId , CreateAnswerRequest createAnswerRequest)
    {
        var subClaim = _tokenClaimsExtractor.ExtractClaim(token, "sub");
        var userId = _userService.GetUserIdFromSubClaimAsync(subClaim);
        var answer = new Answer
        {
            Content = createAnswerRequest.Content,
            UserId = userId.Result,
            QuestionId = questionId
        };
        return await _answerRepository.CreateAnswerAsync(answer);
    }
}