using System.ComponentModel.DataAnnotations;

namespace SushiApp.Models.Entities;

public class Piatto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Type { get; set; }
    
    public byte[]? Image { get; set; }

    
}