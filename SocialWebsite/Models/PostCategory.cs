using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialWebsite.Models;

[Table("PostCategory")]
public class PostCategory
{
    [Key]
    public int CategoryID { get; set; }

    [Required]
    public string CategoryName { get; set; }

    [Required]
    public string Description { get; set; }

    // Relationship
    public ICollection<Post> Posts { get; set; }
}
