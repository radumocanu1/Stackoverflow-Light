namespace Stackoverflow_Light.models;

public class QuestionDto
{
    public List<AnswerDto> AnswerDtos { get; set; }
    // variable to hold the ratio between upvotes and downvotes 
    public int Score { get; set; }
    public Guid AuthorId { get; set; }
}