using System.Linq;
using MvcBlog.Domain.Entities;

namespace MvcBlog.Domain
{
    public interface IRepository
    {
        #region Posts

        IQueryable<Post> Posts { get; }

        void SavePost(Post post);

        Post DeletePost(int postId);

        #endregion

        #region Comments

        IQueryable<Comment> Comments { get; }

        void SaveComment(Comment comment);

        Comment DeleteComment(int commentId);

        #endregion

        #region

        IQueryable<UserProfile> UserProfiles { get; }

        #endregion

        #region

        IQueryable<webpages_Membership> WebpagesMemberships { get; } 
        #endregion
    }
}
