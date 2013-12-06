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
                // if we are adding new comment
                if (comment.CommentID == 0)
                {
                    context.Comments.Add(comment);
                }

                // if we are updating comment
                else
                {
                    Comment dbEntry = context.Comments.Find(comment.CommentID);
                    if (dbEntry != null)
                    {
                        dbEntry.CommentIsVisible = comment.CommentIsVisible;
                        dbEntry.CommentContent = comment.CommentContent;
                        dbEntry.CommentLastModifiedBy = comment.CommentLastModifiedBy;
                        dbEntry.CommentLastModificationDate = comment.CommentLastModificationDate;
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
