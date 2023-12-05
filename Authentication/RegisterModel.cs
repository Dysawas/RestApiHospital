using System.ComponentModel.DataAnnotations;

public class RegisterModel
{
    [Required(ErrorMessage = "Login is required")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}