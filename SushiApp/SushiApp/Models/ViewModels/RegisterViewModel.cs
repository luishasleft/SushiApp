using System.ComponentModel.DataAnnotations;

namespace SushiApp.Models.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Username obbligatorio")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password obbligatoria")]
    public string Password { get; set; } = string.Empty;
}