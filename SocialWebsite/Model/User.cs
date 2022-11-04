using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialWebsite.Model;

/* The Entity Framework Core IndexAttribute was introduced in .NET 5 
    * and is used to create a database index on the column mapped to the specified entity property
    */
[Table("User")]
[Index(nameof(Email), IsUnique = true, Name = "Ix_User_Email")]
public class User
{
    [Key]
    public int UserID { get; set; }

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

    // Relationship
    public ICollection<Post> Posts { get; set; }

}