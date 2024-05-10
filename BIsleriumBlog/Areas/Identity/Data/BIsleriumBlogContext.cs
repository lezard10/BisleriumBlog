using BIsleriumBlog.Areas.Identity.Data;
using BIsleriumBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace BIsleriumBlog.Data;

public class BIsleriumBlogContext : IdentityDbContext<IdentityUser>
{
    public BIsleriumBlogContext(DbContextOptions<BIsleriumBlogContext> options)
        : base(options)
    {
    }

  
    public DbSet<Blogs> Blogs { get; set; }
    public DbSet<Vote> Votes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}