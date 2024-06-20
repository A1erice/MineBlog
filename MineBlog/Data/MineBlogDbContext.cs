using Microsoft.EntityFrameworkCore;
using MineBlog.Models;

namespace MineBlog.Data
{
    public class MineBlogDbContext : DbContext
    {
        public MineBlogDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Like> Like { get; set; }

    }
}
