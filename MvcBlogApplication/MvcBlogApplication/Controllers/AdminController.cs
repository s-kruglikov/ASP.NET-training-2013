using System.Linq;
using System.Web.Mvc;
using MvcBlog.Domain.Abstract;
using MvcBlog.Domain.Entities;

namespace MvcBlog.WebUI.Controllers
{
    public class AdminController : Controller
    {
        readonly IPostsRepository _postsRepository;

        public AdminController(IPostsRepository posts)
        {
            _postsRepository = posts;
        }

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View(_postsRepository.Posts);
        }

        public ViewResult EditPost(int postId)
        {
            var post = _postsRepository.Posts.FirstOrDefault(p => p.PostID == postId);

            return View(post);
        }
    }
}
