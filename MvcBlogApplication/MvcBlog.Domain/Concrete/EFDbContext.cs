using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MvcBlog.Domain.Entities;

namespace MvcBlog.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<webpages_Membership> webpages_Memberships { get; set; }
    }
}
