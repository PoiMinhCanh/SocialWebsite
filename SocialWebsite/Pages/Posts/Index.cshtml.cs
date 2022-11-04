using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Models.Pagination;
using SocialWebsite.Data;
using SocialWebsite.Models;
using SocialWebsite.Services.ManageState;
using System.Configuration;

namespace SocialWebsite.Pages.Posts;

public class IndexModel : StateModel
{
    private readonly IConfiguration Configuration;

    public IndexModel(ApplicationDbContext db, IConfiguration configuration) : base(db)
    {
        Configuration = configuration;
    }

    public List<PostCategory> Categories { get; set; }
    
    public PaginatedList<Post> Posts { get;set; } = default!;
    public int? ActiveCategoryId { get; set; } = null;

    public string TextSearch { get; set; }

    public async Task<IActionResult> OnGet(string q, int? categoryId, int? pageIndex)
    {
        TextSearch = (q == null) ? "" : q.Trim().ToLower();
        ActiveCategoryId = categoryId ?? null;

        // get list category
        Categories = _db.PostCategories.ToList();

        // the method uses LINQ to Entities to specify the column to sort by 
        // get lish product based on categoryId/ActiveCategoryId
        // and search text
        IQueryable<Post> postsIQ = _db.Posts
           .Include(p => p.PostCategory)
           .Include(p => p.User)
           .Where(post => (ActiveCategoryId == null ? true : post.CategoryID == ActiveCategoryId)
                              && ((IsAuthenticated && MyUser.UserID == post.AuthorID) || post.PublishStatus == true)
                              && (post.Title.Contains(TextSearch)
                              || post.Content.Contains(TextSearch)
                              || post.User.Fullname.Contains(TextSearch)))
           .OrderByDescending(p => p.UpdatedDate);

        var pageSize = Configuration.GetValue("PageSize", 4);

        Posts = await PaginatedList<Post>.CreateAsync(postsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

        return Page();
    }   
}
