using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialWebsite.Data;
using SocialWebsite.Models;
using SocialWebsite.Services.ManageState;
using System.Security.Principal;

namespace SocialWebsite.Pages.Posts;

public class DeleteModel : StateModel
{
    public DeleteModel(ApplicationDbContext db) : base(db)
    {
    }

    [BindProperty]
  public Post Post { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (!IsAuthenticated)
        {
            return Redirect("/Login/Index");
        }


        if (id == null || _db.Posts == null)
        {
            return NotFound();
        }

        var post = await _db.Posts
                            .Include(p => p.PostCategory)
                            .FirstOrDefaultAsync(m => m.PostID == id);

        if (post == null)
        {
            return NotFound();
        }

        if (!MyUser.UserID.Equals(post.AuthorID))
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        Post = post;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
      
        if (id == null || _db.Posts == null)
        {
            return NotFound();
        }
        var post = await _db.Posts
                            .FirstOrDefaultAsync(m => m.PostID == id);

        if (post != null)
        {
            if (!MyUser.UserID.Equals(post.AuthorID))
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            Post = post;
            _db.Posts.Remove(Post);
            await _db.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
