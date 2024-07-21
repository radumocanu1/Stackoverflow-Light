namespace Stackoverflow_Light.models;

public class QuestionDto
{
    // variable to hold the ratio between upvotes and downvotes 
    public int Score { get; set; }
    public int ViewCount { get; set; }
    public string Content { get; set; }
    public Guid AuthorId { get; set; }
}