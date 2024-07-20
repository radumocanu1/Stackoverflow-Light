using System.ComponentModel.DataAnnotations;

namespace Stackoverflow_Light.models;

public class CreateAnswerRequest
{
    [Required]
    [StringLength(200, MinimumLength = 5)]
    public string Content { get; set; }
}