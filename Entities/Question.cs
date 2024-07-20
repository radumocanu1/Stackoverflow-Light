using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Stackoverflow_Light.Entities;

public class Question
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public string Content { get; set; }
    // variable to hold the ratio between upvotes and downvotes 
    public int Score { get; set; } = 0;
    // Foreign key to User
    public Guid UserId { get; set; }

    // Navigation property
    [JsonIgnore]
    public User User { get; set; }

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
}