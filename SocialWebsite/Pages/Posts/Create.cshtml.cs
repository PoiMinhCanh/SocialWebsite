using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using SocialWebsite.Data;
using SocialWebsite.Hubs;
using SocialWebsite.Models;
using SocialWebsite.Models.DTO;
using SocialWebsite.Services.ManageState;

namespace SocialWebsite.Pages.Posts;

public class CreateModel : StateModel
{
    private readonly IMapper _mapper;
    private readonly IHubContext<PostHub> PostHub;


    [BindProperty]
    public CreatePostDTO CreatePostDTO { get; set; }

    public CreateModel(ApplicationDbContext db, IMapper mapper, IHubContext<PostHub> PostHub) : base(db)
    {
        _mapper = mapper;
        this.PostHub = PostHub;
    }

    public IActionResult OnGet()
    {
        if (!IsAuthenticated)
        {
            return Redirect("/Login/Index");
        }

        // get post categories
        ViewData["PostCategories"] = new SelectList(_db.PostCategories, "CategoryID", "CategoryName");

        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var post = _mapper.Map<CreatePostDTO, Post>(CreatePostDTO);
        post.AuthorID = MyUser.UserID;

        _db.Posts.Add(post);
        _db.SaveChanges();

        if (post.PublishStatus)
        {
            await PostHub.Clients.All.SendAsync("Notification", MyUser.Fullname, post.Title, "create");
        }

        return Redirect("/");
    }
}
