using System.Linq;
using MvcBlog.Domain.Entities;

namespace MvcBlog.Domain
{
    public partial class SqlRepository
    {
        #region Posts

        public IQueryable<Post> Posts
        {
            get
            {
                return _context.Posts;
            }
        }

        public void SavePost(Post post)
        {
            //we adding new post
            if (post.PostID == 0)
            {
                _context.Posts.Add(post);
            }

            //we updating post
            else
            {
                Post dbEntry = _context.Posts.Find(post.PostID);
                if (dbEntry != null)
                {
                    dbEntry.PostTitle = post.PostTitle;
                    dbEntry.PostDescription = post.PostDescription;
                    dbEntry.PostContent = post.PostContent;
                    dbEntry.PostCategory = post.PostCategory;
                    dbEntry.PostCreatedBy = post.PostCreatedBy;
                    dbEntry.PostCreationDate = post.PostCreationDate;
                    dbEntry.PostIsVisible = post.PostIsVisible;
                    dbEntry.PostCommentsEnabled = post.PostCommentsEnabled;
                    dbEntry.PostFeatured = post.PostFeatured;
                    dbEntry.ImageMimeType = post.ImageMimeType;
                    dbEntry.ImageName = post.ImageName;
                }
            }

            _context.SaveChanges();
        }

        public Post DeletePost(int postId)
        {
            Post dbEntry = _context.Posts.Find(postId);

            if (dbEntry != null)
            {
                _context.Posts.Remove(dbEntry);
                _context.SaveChanges();
            }

            return dbEntry;
        }
        #endregion
    }
}
