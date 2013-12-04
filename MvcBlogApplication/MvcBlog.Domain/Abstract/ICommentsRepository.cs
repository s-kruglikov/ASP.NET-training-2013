using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcBlog.Domain.Entities;

namespace MvcBlog.Domain.Abstract
{
    public interface ICommentsRepository
    {
        IQueryable<Comment> Comments { get; }

        void SaveComment(Comment comment);
    }
}
