using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialWebsite.Model;

[Table("Post")]
public class Post
{
    [Key]
    public int PostID { get; set; }

    [ForeignKey("PostCategory")]
    public int CategoryID { get; set; }

    [ForeignKey("User")]
    public int AuthorID { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public bool PublishStatus { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; } 
    
    public DateTime UpdatedDate { get; set; }

    // Relationship
    public PostCategory PostCategory { get; set; }

    public User User { get; set; }
}
