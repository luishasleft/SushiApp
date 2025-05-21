using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiApp.Models.Entities;

[Table("users_accounts")]

public class UserAccount
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("user_name")]
    [MaxLength(100)]
    
    [Required]
    public string? UserName { get; set; }
    
    [Column("password")]
    [MaxLength(100)]
    
    [Required]
    public string? Password { get; set; }
    
    [Column("role")]
    [MaxLength(20)]
    
    public string? Role { get; set; }
    
}
