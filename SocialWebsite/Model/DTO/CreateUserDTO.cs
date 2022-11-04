using System.ComponentModel.DataAnnotations;

namespace SocialWebsite.Model.DTO;

public class CreateUserDTO
{
    [Required]
    public string Fullname { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }


    [Required]
    [StringLength(30, MinimumLength = 6)]
    public string Password { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 6)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

}