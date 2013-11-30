using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MvcBlog.Domain.Entities;

namespace MvcBlog.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; } 
    }
}
