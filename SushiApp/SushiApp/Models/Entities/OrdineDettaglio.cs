using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiApp.Models.Entities;

public class OrdineDettaglio
{
    public int Id { get; set; }

    [Required]
    public int OrdineId { get; set; }  // Foreign key verso Ordine

    [ForeignKey("OrdineId")]
    public Ordine Ordine { get; set; }

    [Required]
    public int PiattoId { get; set; }  // Foreign key verso Piatto

    [ForeignKey("PiattoId")]
    public Piatto Piatto { get; set; }

    [Required]
    public int Quantita { get; set; } = 1;
}