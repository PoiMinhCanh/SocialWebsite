using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialWebsite.Models.DTO;

public class CreatePostDTO
{

    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    [DisplayName("Publish")]
    public bool PublishStatus { get; set; } = true;

    [Required]
    public int CategoryID { get; set; }

}

