using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBlog.Domain.Abstract;
using MvcBlog.Domain.Entities;

namespace MvcBlog.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IPostsRepository _postsRepository;

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

        public ViewResult Edit(int postId)
        {
            Post post = _postsRepository.Posts
              .FirstOrDefault(p => p.PostID == postId);

            return View(post);
        }
    }
}
