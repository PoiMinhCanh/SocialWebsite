using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialWebsite.Data;
using SocialWebsite.Hubs;
using SocialWebsite.Models;
using SocialWebsite.Services.ManageState;

namespace SocialWebsite.Pages.Posts;

public class EditModel : StateModel
{
    private readonly IHubContext<PostHub> PostHub;

    public EditModel(ApplicationDbContext db, IHubContext<PostHub> PostHub) : base(db)
    {
        this.PostHub = PostHub;
    }

    [BindProperty]
    public Post Post { get; set; } = default!;

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

        var post =  await _db.Posts.FirstOrDefaultAsync(m => m.PostID == id);
        if (post == null)
        {
            return NotFound();
        }

        if (!MyUser.UserID.Equals(post.AuthorID))
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        Post = post;
        
        ViewData["PostCategories"] = new SelectList(_db.PostCategories, "CategoryID", "CategoryName");
        
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (!PostExists(Post.PostID))
        {
            return NotFound();
        }

        if (!MyUser.UserID.Equals(Post.AuthorID))
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        Post.UpdatedDate = DateTime.Now;

        _db.Update(Post);
        _db.SaveChanges();

        if (Post.PublishStatus)
        {
            await PostHub.Clients.All.SendAsync("Notification", MyUser.Fullname, Post.Title, "update");
        }

        return RedirectToPage("./Index");
    }

    private bool PostExists(int id)
    {
        return _db.Posts.Any(e => e.PostID == id);
    }
}
