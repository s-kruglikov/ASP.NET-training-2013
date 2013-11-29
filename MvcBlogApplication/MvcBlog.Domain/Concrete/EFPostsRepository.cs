using MvcBlog.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcBlog.Domain.Concrete
{
    public class EFPostsRepository : IPostsRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Entities.Post> Posts
        {
            get
            {
                return context.Posts;
            }
        }
    }
}
