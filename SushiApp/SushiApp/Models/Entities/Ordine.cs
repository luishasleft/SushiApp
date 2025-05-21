using System.ComponentModel.DataAnnotations;

namespace SushiApp.Models.Entities;

public class Ordine
{
    public int Id { get; set; }

    public DateTime DataOrdine { get; set; } = DateTime.Now;

    [Required]
    public string Stato { get; set; } = "Ricevuto"; 
    [Required]
    public string Tavolo { get; set; }  // Qui metti l'username
    // Relazione con OrdineDettaglio (piatti ordinati)
    public List<OrdineDettaglio> Dettagli { get; set; } = new();
}