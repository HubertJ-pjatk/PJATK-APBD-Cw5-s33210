using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DTOs;

public class UpdateAnimalDto
{
    [Required]
    [MaxLength(10)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(10)]
    public string Species { get; set; } = string.Empty;
    
    [Required]
    public double Weight { get; set; }
}