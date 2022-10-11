using System.ComponentModel.DataAnnotations;

namespace DEVinCar.Domain.DTOs;

public class LoginDTO
{
    [Required(ErrorMessage = "The email is required")]
    [MaxLength(50)]
    public string Email { get; set; }

    [Required(ErrorMessage = "The password is required")]
    [MaxLength(50)]
    public string Password { get; set; }
}
