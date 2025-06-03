using System.ComponentModel.DataAnnotations;

namespace SushiApp.Models.Entities
{
    public class Prenotazione
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(100, ErrorMessage = "Il nome non può superare i 100 caratteri")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "L'email è obbligatoria")]
        [EmailAddress(ErrorMessage = "Inserire un'email valida")]
        [StringLength(150, ErrorMessage = "L'email non può superare i 150 caratteri")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "La data è obbligatoria")]
        public DateTime DataPrenotazione { get; set; }
        
        [Required(ErrorMessage = "L'orario è obbligatorio")]
        public TimeSpan OrarioPrenotazione { get; set; }
        
        public DateTime DataCreazione { get; set; } = DateTime.Now;
        

        
        [Range(1, 20, ErrorMessage = "Il numero di persone deve essere tra 1 e 20")]
        public int NumeroPersone { get; set; } = 2;
        
        public string Stato { get; set; } = "Confermata";
    }
}