using MvcBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcBlog.Domain.Abstract
{
    public interface IPostsRepository
    {
        IQueryable<Post> Posts { get; }

        void SavePost(Post post);
    }
}
