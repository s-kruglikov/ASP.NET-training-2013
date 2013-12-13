using MvcBlog.Domain.Abstract;
using MvcBlog.Domain.Entities;
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

        public void SavePost(Entities.Post post)
        {
            //we adding new post
            if (post.PostID == 0)
            {
                context.Posts.Add(post);
            }

            //we updating post
            else
            {
                Post dbEntry = context.Posts.Find(post.PostID);
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
                    dbEntry.ImageData = post.ImageData;
                    dbEntry.ImageMimeType = post.ImageMimeType;
                    dbEntry.ImageName = post.ImageName;
                    dbEntry.ImageExtension = post.ImageExtension;
                }
            }

            context.SaveChanges();
        }

        public Post DeletePost(int postId)
        {
            Post dbEntry = context.Posts.Find(postId);

            if (dbEntry != null)
            {
                context.Posts.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }
    }
}
