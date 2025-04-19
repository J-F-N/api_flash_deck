using System.ComponentModel.DataAnnotations;

namespace api_flash_deck.Models;

public class User
{
    [Required]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
    
    [Required]
    public string Email { get; set; } = string.Empty;
}