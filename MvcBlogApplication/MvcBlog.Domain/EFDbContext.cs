using System.Data.Entity;
using MvcBlog.Domain.Entities;

namespace MvcBlog.Domain
{
    public class EFDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<webpages_Membership> webpages_Memberships { get; set; }
    }
}
