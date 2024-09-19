using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class EfContext : DbContext
    {
        
        public EfContext(DbContextOptions<EfContext> options) : base(options)
        {
            OnModelCreating(new ModelBuilder());
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }
        
        public DbSet<Like> Likes { get; set; }
        
        public DbSet<Post> Posts { get; set; }
        
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
