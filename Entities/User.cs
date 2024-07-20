using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Stackoverflow_Light.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required] 
    public string Username { get; set; }
    
    [Required]
    [JsonIgnore]
    public OidcUserMapping OidcUserMapping { get; set; }
    
    public ICollection<Question> Questions { get; set; } = new List<Question>(); 

}