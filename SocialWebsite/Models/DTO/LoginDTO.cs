using System.ComponentModel.DataAnnotations;

namespace SocialWebsite.Models.DTO;

public class LoginDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 6)]
    public string Password { get; set; }

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }

}
