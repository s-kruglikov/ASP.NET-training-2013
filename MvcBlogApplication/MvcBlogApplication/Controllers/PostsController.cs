using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using MvcBlog.Domain.Abstract;
using MvcBlog.Domain.Entities;
using MvcBlog.WebUI.Models;
using System.Configuration;
using System;
using System.IO;
using MvcBlog.WebUI.Concrete;
using MvcBlog.WebUI.Abstract;

namespace MvcBlog.WebUI.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostsRepository _postsRepository;
        private ICommentsRepository _commentsRepository;
        private IConfigService _blogConfig;

        //public int PageSize { get; private set; } 

        public PostsController(IPostsRepository postsRepository, IConfigService blogConfig)
        {
            _postsRepository = postsRepository;
            //PageSize = int.Parse(ConfigurationManager.AppSettings["PostsPerPage"]);
            _blogConfig = blogConfig;
        }

        public PostsController(ICommentsRepository commentsRepository, IPostsRepository postsRepository, IConfigService blogConfig)
        {
            _commentsRepository = commentsRepository;
            _postsRepository = postsRepository;
            //PageSize = int.Parse(ConfigurationManager.AppSettings["PostsPerPage"]);
            _blogConfig = blogConfig;
        }

        //
        // POST: /Posts/
        // List posts
        public ViewResult List(string category, int page = 1)
        {
            var model = new PostsListViewModel
            {
                Posts = _postsRepository.Posts
                .Where(p => category == null && p.PostIsVisible == true
                    || p.PostCategory == category && p.PostIsVisible == true)
                .OrderByDescending(p => p.PostID)
                .Skip((page - 1) * _blogConfig.PostsPerPage)
                .Take(_blogConfig.PostsPerPage),

                PagingModel = new PagingModel
                {
                    CurrentPage = page,
                    ItemsPerPage = _blogConfig.PostsPerPage,
                    TotalItems = category == null ? _postsRepository.Posts.Count() : _postsRepository.Posts.Count(p => p.PostCategory == category)
                }
            };

            return View(model);
        }

        //
        // GET: 
        public ActionResult SinglePost(string category, int postId)
        {
            var model = new PostDetailedModel()
            {
                PostDetailed = _postsRepository.Posts.FirstOrDefault(p => p.PostID == postId && p.PostIsVisible==true),

                Comments = _commentsRepository.Comments
                    .OrderBy(c => c.CommentID)
                    .Where(c => c.PostID == postId),

                NewComment = new Comment()
            };

            if (model.PostDetailed == null)
                return View("_NotFound");

           return View(model);
        }

        //
        // POST:
        [Authorize]
        [ValidateInput(false)]
        public ActionResult AddComment(PostDetailedModel postDetailed, int postId, string category)
        {
            postDetailed.NewComment.CommentCreatedBy = User.Identity.Name;
            postDetailed.NewComment.CommentCreationDate = DateTime.Now;
            postDetailed.NewComment.CommentLastModifiedBy = User.Identity.Name;
            postDetailed.NewComment.CommentLastModificationDate = DateTime.Now;
            postDetailed.NewComment.CommentIsVisible = true;
            
            postDetailed.NewComment.PostID = postId;

            _commentsRepository.SaveComment(postDetailed.NewComment);

            return RedirectToAction("SinglePost",routeValues: new {postId = postId, category = category});
        }

        public FilePathResult GetPostThumbnail(int postId)
        {
            Post post = _postsRepository.Posts.FirstOrDefault(p => p.PostID == postId);
            if (post != null)
            {
                return File(System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content/PostsThumbs")),post.ImageName), post.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
