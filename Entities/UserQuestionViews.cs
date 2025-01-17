using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Stackoverflow_Light.Entities;

public class UserQuestionView
{

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid QuestionId { get; set; }

    [JsonIgnore]
    public User User { get; set; }

    [JsonIgnore]
    public Question Question { get; set; }
}
