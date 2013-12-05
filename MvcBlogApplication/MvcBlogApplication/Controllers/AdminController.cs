using System.Linq;
using System.Web.Mvc;
using MvcBlog.Domain.Abstract;
using MvcBlog.Domain.Entities;

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
            return View(_postsRepository.Posts.OrderByDescending(p => p.PostID));
        }

        //
        // GET: /Admin/EditPost

        [HttpGet]
        public ViewResult EditPost(int postId)
        {
            var post = _postsRepository.Posts.FirstOrDefault(p => p.PostID == postId);

            return View(post);
        }

        //
        // POST: /Admin/EditPost

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPost(Post post)
        {
            if (ModelState.IsValid)
            {
                _postsRepository.SavePost(post);
                TempData["message"] = string.Format("{0} has been saved", post.PostTitle);
                return RedirectToAction("ManagePosts");
            }
            else
            {
                // something wrong with the data values
                return View(post);
            }
        }

        //
        // GET: /Admin/CreatePost
        [HttpGet]
        public ViewResult CreatePost()
        {
            return View("EditPost", new Post());
        }

        //
        // POST: /Admin/DeletePost

        [HttpPost]
        public ActionResult DeletePost(int postId)
        {
            Post postToDelete = _postsRepository.DeletePost(postId);

            TempData["message"] = string.Format("{0} has been deleted", postToDelete.PostTitle);
            return RedirectToAction("ManagePosts");
        }

        //
        // GET: /Admin/ManageComments
        public ActionResult ManageComments()
        {
            return View(_commentsRepository.Comments.OrderByDescending(c => c.CommentID));
        }

        [HttpGet]
        public ViewResult EditComment(int commentId)
        {
            var comment = _commentsRepository.Comments.FirstOrDefault(c => c.CommentID == commentId);
            return View(comment);
        }
    }
}
