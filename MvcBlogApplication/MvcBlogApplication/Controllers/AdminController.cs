﻿using System.Linq;
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

        public ActionResult ManagePosts()
        {
            return View(_postsRepository.Posts);
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
        // GET: /Admin/ManageComments
        public ActionResult ManageComments()
        {
            return View(_commentsRepository.Comments);
        }
    }
}
