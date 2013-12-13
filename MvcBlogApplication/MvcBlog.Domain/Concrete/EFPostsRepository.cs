using MvcBlog.Domain.Abstract;
using MvcBlog.Domain.Entities;
using System.Linq;

namespace MvcBlog.Domain.Concrete
{
    public class EFPostsRepository : IPostsRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

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
                    dbEntry.PostLastModifiedBy = post.PostLastModifiedBy;
                    dbEntry.PostLastModificationDate = post.PostLastModificationDate;
                    dbEntry.PostIsVisible = post.PostIsVisible;
                    dbEntry.PostCommentsEnabled = post.PostCommentsEnabled;
                    dbEntry.PostFeatured = post.PostFeatured;
                    dbEntry.ImageMimeType = post.ImageMimeType;
                    dbEntry.ImageName = post.ImageName;
                    dbEntry.ImageExtension = post.ImageExtension;
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
    }
}
