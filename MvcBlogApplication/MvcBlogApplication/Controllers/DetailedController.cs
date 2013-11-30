using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBlog.Domain.Abstract;
using MvcBlog.WebUI.Models;

namespace MvcBlog.WebUI.Controllers
{
    public class DetailedController : Controller
    {
        private ICommentsRepository _comments;
        private IPostsRepository _posts;

        public DetailedController(ICommentsRepository comments, IPostsRepository posts)
        {
            _comments = comments;
            _posts = posts;
        }

        //
        // GET: /Detailed/PostView

        
        public ActionResult SinglePost(int postId)
        {
            PostDetailedModel model = new PostDetailedModel()
            {
                PostDetailed = _posts.Posts.First(p => p.PostID == postId),

                Comments = _comments.Comments
                    .OrderBy(c => c.CommentID)
                    .Where(c => c.PostID == postId)
            };

            return View(model);
        }

        
    }
}
