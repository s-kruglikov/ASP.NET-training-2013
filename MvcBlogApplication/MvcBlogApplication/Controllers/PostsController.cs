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
    public class PostsController : BaseController
    {
        //
        // POST: /Posts/
        // List posts
        public ViewResult List(string category, int page = 1)
        {
            var model = new PostsListViewModel
            {
                Posts = Repository.Posts
                .Where(p => category == null && p.PostIsVisible == true
                    || p.PostCategory == category && p.PostIsVisible == true)
                .OrderByDescending(p => p.PostID)
                .Skip((page - 1) * ConfigService.PostsPerPage)
                .Take(ConfigService.PostsPerPage),

                PagingModel = new PagingModel
                {
                    CurrentPage = page,
                    ItemsPerPage = ConfigService.PostsPerPage,
                    TotalItems = category == null ? Repository.Posts.Count() : Repository.Posts.Count(p => p.PostCategory == category)
                }
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult SinglePost(int postId)
        {
            Post detailed = Repository.Posts.FirstOrDefault(p => p.PostID == postId && p.PostIsVisible == true);

            return View(detailed);
        }

        [HttpGet]
        public PartialViewResult CommentsList(int postId)
        {
            var commentsList = Repository.Comments
                .Where(c => c.PostID == postId)
                .OrderBy(p=>p.CommentID);

            return PartialView(commentsList);
        }

        [HttpGet]
        [Authorize]
        public PartialViewResult AddComment(int postId)
        {
            var comment = new Comment {PostID = postId};

            return PartialView(comment);
        }

        [Authorize]
        [ValidateInput(false)]
        public ActionResult AddComment(Comment comment)
        {
            comment.CommentCreatedBy = User.Identity.Name;
            comment.CommentCreationDate = DateTime.Now;
            comment.CommentLastModifiedBy = User.Identity.Name;
            comment.CommentLastModificationDate = DateTime.Now;
            comment.CommentIsVisible = true;

            Repository.SaveComment(comment);
            return RedirectToAction("CommentsList", routeValues: new { postId = comment.PostID });
        }

        // get thumbnail image for the post
        public FilePathResult GetPostThumbnail(int postId)
        {
            Post post = Repository.Posts.FirstOrDefault(p => p.PostID == postId);
            if (post != null)
            {
                return File(Path.Combine(Server.MapPath(Url.Content("~/Content/")), ConfigService.PostThumbPath ,post.ImageName), post.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        // get featured image for the post
        public FilePathResult GetPostFeatured(int postId)
        {
            Post post = Repository.Posts.FirstOrDefault(p => p.PostID == postId);
            if (post != null)
            {
                return File(Path.Combine(Server.MapPath(Url.Content("~/Content/")), ConfigService.PostFeaturedPath, post.ImageName), post.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
