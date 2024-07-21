using Stackoverflow_Light.Configurations;
using Stackoverflow_Light.Entities;
using Stackoverflow_Light.Exceptions;
using Stackoverflow_Light.models;
using Stackoverflow_Light.Repositories;
using Stackoverflow_Light.Utils;
using Stackoverflow_Light.Utils.Interfaces;

namespace Stackoverflow_Light.Services;

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _answerRepository;
    private readonly ITokenClaimsExtractor _tokenClaimsExtractor;
    private readonly IUserService _userService;

    public AnswerService(IAnswerRepository answerRepository, ITokenClaimsExtractor tokenClaimsExtractor, IUserService userService)
    {
        _answerRepository = answerRepository;
        _tokenClaimsExtractor = tokenClaimsExtractor;
        _userService = userService;
    }
    public async Task<Answer> CreateAnswerAsync(string token, Guid questionId , AnswerRequest answerRequest)
    {
        var subClaim = _tokenClaimsExtractor.ExtractClaim(token, "sub");
        var userId = await _userService.GetUserIdFromSubClaimAsync(subClaim);
        var answer = new Answer
        {
            Content = answerRequest.Content,
            UserId = userId,
            QuestionId = questionId
        };
        return await _answerRepository.CreateAnswerAsync(answer);
    }
    public async Task DeleteAnswerAsync(string token,Guid answerId)
    {
        if (!await IsCurrentUserAnswerAuthor(token, answerId))
            throw new OperationNotAllowed(ApplicationConstants.OPERATION_NOT_ALLOWED_MESSAGE);
        await _answerRepository.DeleteAnswerAsync(answerId);
    }
    public async Task<Answer> EditAnswerAsync(string token, Guid answerId, AnswerRequest answerRequest)
    {
        if (!await IsCurrentUserAnswerAuthor(token, answerId))
            throw new OperationNotAllowed(ApplicationConstants.OPERATION_NOT_ALLOWED_MESSAGE);
        return await _answerRepository.EditAnswerAsync(answerId, answerRequest);
    }
    public async Task DeleteAnswerAdminAsync(Guid answerId)
    {
        await _answerRepository.DeleteAnswerAsync(answerId);
    }
    private async Task<bool> IsCurrentUserAnswerAuthor(string token, Guid answerId)
    {
        var subClaim = _tokenClaimsExtractor.ExtractClaim(token,"sub");
        var userId = await _userService.GetUserIdFromSubClaimAsync(subClaim);
        return userId == await _answerRepository.GetAuthorIdFromAnswerIdAsync(answerId);
    }
}