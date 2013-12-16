using System.Linq;
using MvcBlog.Domain.Entities;

namespace MvcBlog.Domain
{
    public partial class SqlRepository
    {
        public IQueryable<Comment> Comments
        {
            get { return _context.Comments; }
        }

        public void SaveComment(Comment comment)
        {

            if (comment.CommentContent != null)
            {
                // if we are adding new comment
                if (comment.CommentID == 0)
                {
                    _context.Comments.Add(comment);
                }

                // if we are updating comment
                else
                {
                    Comment dbEntry = _context.Comments.Find(comment.CommentID);
                    if (dbEntry != null)
                    {
                        dbEntry.CommentIsVisible = comment.CommentIsVisible;
                        dbEntry.CommentContent = comment.CommentContent;
                        dbEntry.CommentLastModifiedBy = comment.CommentLastModifiedBy;
                        dbEntry.CommentLastModificationDate = comment.CommentLastModificationDate;
                    }
                }

                _context.SaveChanges();
            }
        }

        public Comment DeleteComment(int commentId)
        {
            Comment dbEntry = _context.Comments.Find(commentId);

            if (dbEntry != null)
            {
                _context.Comments.Remove(dbEntry);
                _context.SaveChanges();
            }

            return dbEntry;
        }
    }
}
