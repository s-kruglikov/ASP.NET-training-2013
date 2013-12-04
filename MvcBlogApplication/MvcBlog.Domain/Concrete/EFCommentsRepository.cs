using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcBlog.Domain.Abstract;
using MvcBlog.Domain.Entities;

namespace MvcBlog.Domain.Concrete
{
    public class EFCommentsRepository : ICommentsRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Entities.Comment> Comments
        {
            get { return context.Comments; }
        }

        public void SaveComment(Comment comment)
        {
            if (comment.CommentContent != null)
            {
                context.Comments.Add(comment);
                context.SaveChanges();
            }
        }
    }
}
