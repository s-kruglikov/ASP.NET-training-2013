using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using MvcBlog.Domain;
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
        private readonly IRepository _repository;
        private readonly IConfigService _blogConfig;

        public PostsController(IRepository repository, IConfigService blogConfig)
        {
            _repository = repository;
            _blogConfig = blogConfig;
        }

        //
        // POST: /Posts/
        // List posts
        public ViewResult List(string category, int page = 1)
        {
            var model = new PostsListViewModel
            {
                Posts = _repository.Posts
                .Where(p => category == null && p.PostIsVisible == true
                    || p.PostCategory == category && p.PostIsVisible == true)
                .OrderByDescending(p => p.PostID)
                .Skip((page - 1) * _blogConfig.PostsPerPage)
                .Take(_blogConfig.PostsPerPage),

                PagingModel = new PagingModel
                {
                    CurrentPage = page,
                    ItemsPerPage = _blogConfig.PostsPerPage,
                    TotalItems = category == null ? _repository.Posts.Count() : _repository.Posts.Count(p => p.PostCategory == category)
                }
            };

            return View(model);
        }

        //
        // GET: 
        public ActionResult SinglePost(string category, int postId)
        {
            var commentsList = new List<CommentViewModel>();

            var comments =
                _repository.Comments.Join(_repository.UserProfiles, c => c.CommentCreatedBy, u => u.UserName,
                    (c, u) => new {c, u}).Where(@t => @t.c.PostID == postId && @t.c.CommentIsVisible == true);
            

            foreach (var comment in comments)
            {
                 commentsList.Add(new CommentViewModel()
                 {
                     Avatar = comment.u.Avatar,
                     CommentContent = comment.c.CommentContent,
                     CommentCreatedBy = comment.c.CommentCreatedBy,
                     CommentCreationDate = comment.c.CommentCreationDate,
                     CommentID = comment.c.CommentID,
                     PostID = comment.c.PostID
                 });
            }

            var model = new PostDetailedModel()
            {
                PostDetailed = _repository.Posts.FirstOrDefault(p => p.PostID == postId && p.PostIsVisible==true),

                CommentsList = commentsList,

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

            _repository.SaveComment(postDetailed.NewComment);

            return RedirectToAction("SinglePost",routeValues: new {postId = postId, category = category});
        }

        // get thumbnail image for the post
        public FilePathResult GetPostThumbnail(int postId)
        {
            Post post = _repository.Posts.FirstOrDefault(p => p.PostID == postId);
            if (post != null)
            {
                return File(Path.Combine(Server.MapPath(Url.Content("~/Content/")), _blogConfig.PostThumbPath ,post.ImageName), post.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        // get featured image for the post
        public FilePathResult GetPostFeatured(int postId)
        {
            Post post = _repository.Posts.FirstOrDefault(p => p.PostID == postId);
            if (post != null)
            {
                return File(Path.Combine(Server.MapPath(Url.Content("~/Content/")), _blogConfig.PostFeaturedPath, post.ImageName), post.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
