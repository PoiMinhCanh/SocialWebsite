using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SocialWebsite.Data;
using SocialWebsite.Models;

namespace SocialWebsite.Pages.Posts
{
    public class DetailsModel : PageModel
    {
        private readonly SocialWebsite.Data.ApplicationDbContext _context;

        public DetailsModel(SocialWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FirstOrDefaultAsync(m => m.PostID == id);
            if (post == null)
            {
                return NotFound();
            }
            else 
            {
                Post = post;
            }
            return Page();
        }
    }
}
