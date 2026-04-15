using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Reservation : IValidatableObject
{
    public int Id { get; set; }
    
    public int RoomId { get; set; }

    [Required(ErrorMessage = "OrganizerName is required.")]
    public string OrganizerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Topic is required.")]
    public string Topic { get; set; } = string.Empty;

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    [Required]
    public TimeSpan EndTime { get; set; }

    public string Status { get; set; } = "planned";
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult(
                "EndTime must be after StartTime and EndTime.",
                new[] { nameof(EndTime) }
            );
        }
    }
}