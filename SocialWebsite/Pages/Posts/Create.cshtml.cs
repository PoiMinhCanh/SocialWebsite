using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SocialWebsite.Data;
using SocialWebsite.Models;
using SocialWebsite.Models.DTO;
using SocialWebsite.Services.ManageState;

namespace SocialWebsite.Pages.Posts;

public class CreateModel : StateModel
{
    private readonly IMapper _mapper;

    [BindProperty]
    public CreatePostDTO CreatePostDTO { get; set; }

    public CreateModel(ApplicationDbContext db, IMapper mapper) : base(db)
    {
        _mapper = mapper;
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
    
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var post = _mapper.Map<CreatePostDTO, Post>(CreatePostDTO);
        post.AuthorID = MyUser.UserID;

        _db.Posts.Add(post);
        _db.SaveChanges();

        return Redirect("/");
    }
}
