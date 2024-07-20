using System.ComponentModel.DataAnnotations;
using Stackoverflow_Light.Entities;

namespace Stackoverflow_Light.models;

public class AnswerDto
{
    public string Content { get; set; }
    public User User { get; set; }
    public int Score { get; set; }
    public Guid AuthorId { get; set; }
}