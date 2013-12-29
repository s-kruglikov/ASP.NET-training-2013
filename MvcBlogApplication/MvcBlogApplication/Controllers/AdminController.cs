using System.IO;
using System.Linq;
using System.Web.Mvc;
using MvcBlog.Domain;
using MvcBlog.Domain.Entities;
using System;
using System.Web;
using System.Drawing.Imaging;
using System.Drawing;
using MvcBlog.WebUI.Concrete;
using MvcBlog.WebUI.Models;
using MvcBlog.WebUI.Tools;
using MvcBlog.WebUI.Abstract;


namespace MvcBlog.WebUI.Controllers
{
    public class AdminController : BaseController
    {
        #region Posts
        //
        // GET: /Admin/ManagePosts

        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public ActionResult ManagePosts()
        {
            ViewBag.CurrentEdit = "Posts";
            return View(Repository.Posts.OrderByDescending(p => p.PostID));
        }

        //
        // GET: /Admin/EditPost

        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public ViewResult EditPost(int postId)
        {
            ViewBag.CurrentEdit = "Posts";
            var post = Repository.Posts.FirstOrDefault(p => p.PostID == postId);

            if (post != null)
            {

                ViewBag.Title = string.Format("Edit post '{0}'", post.PostTitle);

                return View(post);
            }
            return View("_NotFound");
        }

        //
        // POST: /Admin/EditPost

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        [ValidateInput(false)]
        public ActionResult EditPost(Post post, HttpPostedFileBase postImage)
        {
            ViewBag.CurrentEdit = "Posts";
            if (ModelState.IsValid)
            {
                if (post.PostID == 0)
                {
                    post.PostCreatedBy = User.Identity.Name;
                    post.PostCreationDate = DateTime.Now;
                }

                post.PostLastModifiedBy = User.Identity.Name;
                post.PostLastModificationDate = DateTime.Now;

                if (postImage != null
                    && ImagesExtensions.SupportedFormat(postImage, ConfigService.AllowedImageTypes)
                    && ImagesExtensions.CheckSize(postImage, ConfigService.MaxImageSize))
                {
                    string prevImage = string.Empty;
                    if (!string.IsNullOrEmpty(post.ImageName))
                    {
                        // will be used to delete previous post images
                        prevImage = post.ImageName;
                    }

                    string imageExtension = System.IO.Path.GetExtension(postImage.FileName);
                    string imageName = string.Format("{0}_{1}{2}", post.PostID, DateTime.Now.Ticks, imageExtension);
                    string imageThumbSavePath = Path.Combine(Server.MapPath(Url.Content("~/Content/")), ConfigService.PostThumbPath, imageName);
                    string imageFeaturedSavePath = Path.Combine(Server.MapPath(Url.Content("~/Content/")), ConfigService.PostFeaturedPath, imageName);

                    //image parameters
                    post.ImageMimeType = postImage.ContentType;
                    post.ImageName = imageName;

                    //resize proportional thumbnail image
                    Image.FromStream(postImage.InputStream)
                        .ResizeProportional(new Size(ConfigService.PostThumbImageWidth, ConfigService.PostThumbImageHeight))
                        .SaveToFolder(imageThumbSavePath);

                    //crop and resize featured image
                    Image.FromStream(postImage.InputStream)
                        .ResizeMinimalSqueeze(new Size(ConfigService.PostFeaturedImageWidth, ConfigService.PostFeaturedImageHeight))
                        .SaveToFolder(imageFeaturedSavePath);

                    // delete previous thumbnail picture if exists
                    if (!string.IsNullOrEmpty(prevImage) && System.IO.File.Exists(imageThumbSavePath))
                    {
                        System.IO.File.Delete(Path.Combine(Server.MapPath(Url.Content("~/Content/")), ConfigService.PostThumbPath, prevImage));
                    }

                    // delete previous featured image if exists
                    if (!string.IsNullOrEmpty(prevImage) && System.IO.File.Exists(imageFeaturedSavePath))
                    {
                        System.IO.File.Delete(Path.Combine(Server.MapPath(Url.Content("~/Content/")), ConfigService.PostFeaturedPath, prevImage));
                    }
                }

                // save updated data into DB
                Repository.SavePost(post);

                TempData["message"] = string.Format("{0} has been saved", post.PostTitle);
                return RedirectToAction("ManagePosts");
            }

            // something wrong occured with the data values
            return View(post);
        }

        //
        // GET: /Admin/CreatePost
        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public ViewResult CreatePost()
        {
            ViewBag.Title = "Add new post";
            ViewBag.CurrentEdit = "Posts";
            return View("EditPost", new Post());
        }

        //
        // POST: /Admin/DeletePost

        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public ActionResult DeletePost(int postId)
        {
            Post postToDelete = Repository.DeletePost(postId);

            // delete all images related to th post
            DeletePostImages(postToDelete);

            TempData["message"] = string.Format("{0} has been deleted", postToDelete.PostTitle);
            return RedirectToAction("ManagePosts");
        }

        #endregion

        #region Comments
        //
        // GET: /Admin/ManageComments
        [Authorize(Roles = "Administrators")]
        public ActionResult ManageComments()
        {
            ViewBag.CurrentEdit = "Comments";
            return View(Repository.Comments.OrderByDescending(c => c.CommentID));
        }

        //
        // GET: /Admin/EditComment
        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public ViewResult EditComment(int commentId)
        {
            ViewBag.CurrentEdit = "Comments";
            var comment = Repository.Comments.FirstOrDefault(c => c.CommentID == commentId);
            return View(comment);
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public ActionResult EditComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CommentLastModifiedBy = User.Identity.Name;
                comment.CommentLastModificationDate = DateTime.Now;

                Repository.SaveComment(comment);

                TempData["message"] = string.Format("Comment (id = {0}) has been saved", comment.CommentID);
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
            Repository.DeleteComment(commentId);

            TempData["message"] = string.Format("Comment (id = {0}) has been deleted.", commentId);

            return RedirectToAction("ManageComments");
        }

        #endregion

        #region Users
        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public ActionResult ManageUsers()
        {
            ViewBag.CurrentEdit = "Users";
            return View();
        }
        #endregion

        #region SiteParameters

        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public ActionResult SiteConfig()
        {
            ViewBag.CurrentEdit = "Site";

            var configViewModel =
                (SiteConfigViewModel) ModelMapper.Map(ConfigService, typeof (ConfigService), typeof (SiteConfigViewModel));
            
            return View(configViewModel);
        }
        #endregion

        #region helpers

        private void DeletePostImages(Post post)
        {
            if (post.ImageName != null)
            {
                string currentThumbPath = Path.Combine(Server.MapPath(Url.Content("~/Content/")), ConfigService.PostThumbPath, post.ImageName);
                string currentFeaturedPath = Path.Combine(Server.MapPath(Url.Content("~/Content/")), ConfigService.PostFeaturedPath, post.ImageName);

                if (System.IO.File.Exists(currentThumbPath))
                {
                    System.IO.File.Delete(currentThumbPath);
                }

                if (System.IO.File.Exists(currentFeaturedPath))
                {
                    System.IO.File.Delete(currentFeaturedPath);
                }
            }
        }

        #endregion
    }
}
