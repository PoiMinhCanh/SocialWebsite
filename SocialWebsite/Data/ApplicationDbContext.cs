using Microsoft.EntityFrameworkCore;
using SocialWebsite.Model;

namespace SocialWebsite.Data;   

public class ApplicationDbContext : DbContext
{

    #region DbSet
    public DbSet<User> Users { get; set; }
    
    public DbSet<PostCategory> PostCategories { get; set; }
    
    public DbSet<Post> Posts { get; set; }
    #endregion

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // create first data for db
        modelBuilder.Entity<PostCategory>().HasData(
                    new PostCategory { CategoryID = 1, CategoryName = "Documentation", Description = "Posts for documentation purposes" },
                    new PostCategory { CategoryID = 2, CategoryName = "Entertaiment", Description = "Posts for entertainment purposes" },
                    new PostCategory { CategoryID = 3, CategoryName = "18+", Description = "Posts for 18+ purposes" }
                );
    }
  
}