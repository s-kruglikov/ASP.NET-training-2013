using System.Linq;
using System.Web.Mvc;
using MvcBlog.Domain.Abstract;
using MvcBlog.Domain.Entities;
using System;
using System.Web;

namespace MvcBlog.WebUI.Controllers
{
    public class AdminController : Controller
    {
        readonly IPostsRepository _postsRepository;
        readonly ICommentsRepository _commentsRepository;

        public AdminController(IPostsRepository posts, ICommentsRepository comments)
        {
            _postsRepository = posts;
            _commentsRepository = comments;
        }

        //
        // GET: /Admin/ManagePosts

        [Authorize(Roles = "Administrators")]
        public ActionResult ManagePosts()
        {
            ViewBag.CurrentEdit = "Posts";
            return View(_postsRepository.Posts.OrderByDescending(p => p.PostID));
        }

        //
        // GET: /Admin/EditPost

        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public ViewResult EditPost(int postId)
        {
            ViewBag.CurrentEdit = "Posts";
            var post = _postsRepository.Posts.FirstOrDefault(p => p.PostID == postId);

            ViewBag.Title = string.Format("MVC Blog: Edit post '{0}'", post.PostTitle);

            return View(post);
        }

        //
        // POST: /Admin/EditPost

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        [ValidateInput(false)]
        public ActionResult EditPost(Post post, HttpPostedFileBase image)
        {
            ViewBag.CurrentEdit = "Posts";
            if (ModelState.IsValid)
            {
                if (post.PostID == 0)
                {
                    post.PostCreatedBy = User.Identity.Name;
                    post.PostCreationDate = DateTime.Now;
                }

                if (image != null)
                {
                    post.ImageMimeType = image.ContentType;
                    post.Imagedata = new byte[image.ContentLength];
                    image.InputStream.Read(post.Imagedata, 0, image.ContentLength);
                }


                post.PostLastModifiedBy = User.Identity.Name;
                post.PostLastModificationDate = DateTime.Now;

                _postsRepository.SavePost(post);
                TempData["message"] = string.Format("{0} has been saved", post.PostTitle);
                return RedirectToAction("ManagePosts");
            }
            else
            {
                // something wrong occured with the data values
                return View(post);
            }
        }

        //
        // GET: /Admin/CreatePost
        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public ViewResult CreatePost()
        {
            ViewBag.Title = "MVC Blog: Add new post";
            ViewBag.CurrentEdit = "Posts";
            return View("EditPost", new Post());
        }

        //
        // POST: /Admin/DeletePost

        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public ActionResult DeletePost(int postId)
        {
            Post postToDelete = _postsRepository.DeletePost(postId);

            TempData["message"] = string.Format("{0} has been deleted", postToDelete.PostTitle);
            return RedirectToAction("ManagePosts");
        }

        //
        // GET: /Admin/ManageComments
        [Authorize(Roles = "Administrators")]
        public ActionResult ManageComments()
        {
            ViewBag.CurrentEdit = "Comments";
            return View(_commentsRepository.Comments.OrderByDescending(c => c.CommentID));
        }

        [HttpGet]
        public ViewResult EditComment(int commentId)
        {
            ViewBag.CurrentEdit = "Comments";
            var comment = _commentsRepository.Comments.FirstOrDefault(c => c.CommentID == commentId);
            return View(comment);
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        [ValidateInput(false)]
        public ActionResult EditComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CommentLastModifiedBy = User.Identity.Name;
                comment.CommentLastModificationDate = DateTime.Now;

                _commentsRepository.SaveComment(comment);

                TempData["message"] = string.Format("Comment {0} has been saved", comment.CommentID);
                return RedirectToAction("ManageComments");
            }
            else
            {
                // something wrong with the data values
                return View(comment);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public ActionResult DeleteComment(int commentId)
        {
            return RedirectToAction("ManageComments");
        }
    }
}
