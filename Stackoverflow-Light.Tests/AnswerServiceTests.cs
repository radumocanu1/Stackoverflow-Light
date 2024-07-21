using NSubstitute;
using Stackoverflow_Light.Entities;
using Stackoverflow_Light.models;
using Stackoverflow_Light.Repositories;
using Stackoverflow_Light.Services;
using Stackoverflow_Light.Utils.Interfaces;


namespace Stackoverflow_Light.Stackoverflow_Light.Tests;

[TestFixture]
public class AnswerServiceTests
{
    private IAnswerRepository _answerRepository;
    private ITokenClaimsExtractor _tokenClaimsExtractor;
    private IUserService _userService;
    private AnswerService _answerService;

    [SetUp]
    public void Setup()
    {
        _answerRepository = Substitute.For<IAnswerRepository>();
        _tokenClaimsExtractor = Substitute.For<ITokenClaimsExtractor>();
        _userService = Substitute.For<IUserService>();
        _answerService = new AnswerService(_answerRepository, _tokenClaimsExtractor, _userService);
    }

    [Test]
    public async Task CreateAnswerAsync_ShouldReturnAnswer_WhenValidRequest()
    {
        var token = "token";
        var questionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var answerRequest = new AnswerRequest() { Content = "Test Content" };

        _tokenClaimsExtractor.ExtractClaim(token, "sub").Returns("subClaim");
        _userService.GetUserIdFromSubClaimAsync("subClaim").Returns(Task.FromResult(userId));

        var answer = new Answer
        {
            Content = answerRequest.Content,
            UserId = userId,
            QuestionId = questionId
        };

        _answerRepository.CreateAnswerAsync(Arg.Any<Answer>()).Returns(Task.FromResult(answer));

        // calling the function to be tested
        var result = await _answerService.CreateAnswerAsync(token, questionId, answerRequest);

        Assert.That(result, Is.Not.Null); 
        Assert.That(result.Content, Is.EqualTo(answerRequest.Content)); 
        Assert.That(result.UserId, Is.EqualTo(userId)); 
        Assert.That(result.QuestionId, Is.EqualTo(questionId)); 
    }

    // Adaugă alte teste pentru DeleteAnswerAsync, EditAnswerAsync etc.
}