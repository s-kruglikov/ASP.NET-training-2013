using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using MvcBlog.Domain.Entities;
using System;
using System.Web;
using System.Drawing;
using MvcBlog.WebUI.Concrete;
using MvcBlog.WebUI.Infrastructure;
using MvcBlog.WebUI.Models;
using MvcBlog.WebUI.Tools;


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
                    && ImagesExtensions.SupportedFormat(postImage, SiteConfigService.AllowedImageTypes)
                    && ImagesExtensions.CheckSize(postImage, SiteConfigService.MaxImageSize))
                {
                    string prevImage = string.Empty;
                    if (!string.IsNullOrEmpty(post.ImageName))
                    {
                        // will be used to delete previous post images
                        prevImage = post.ImageName;
                    }

                    string imageExtension = Path.GetExtension(postImage.FileName);
                    string imageName = string.Format("{0}_{1}{2}", post.PostID, DateTime.Now.Ticks, imageExtension);
                    string imageThumbSavePath = Path.Combine(Server.MapPath(Url.Content("~/Content/")), SiteConfigService.PostThumbPath, imageName);
                    string imageFeaturedSavePath = Path.Combine(Server.MapPath(Url.Content("~/Content/")), SiteConfigService.PostFeaturedPath, imageName);

                    //image parameters
                    post.ImageMimeType = postImage.ContentType;
                    post.ImageName = imageName;

                    //resize proportional thumbnail image
                    Image.FromStream(postImage.InputStream)
                        .ResizeProportional(new Size(SiteConfigService.PostThumbImageWidth, SiteConfigService.PostThumbImageHeight))
                        .SaveToFolder(imageThumbSavePath);

                    //crop and resize featured image
                    Image.FromStream(postImage.InputStream)
                        .ResizeMinimalSqueeze(new Size(SiteConfigService.PostFeaturedImageWidth, SiteConfigService.PostFeaturedImageHeight))
                        .SaveToFolder(imageFeaturedSavePath);

                    // delete previous thumbnail picture if exists
                    if (!string.IsNullOrEmpty(prevImage) && System.IO.File.Exists(imageThumbSavePath))
                    {
                        System.IO.File.Delete(Path.Combine(Server.MapPath(Url.Content("~/Content/")), SiteConfigService.PostThumbPath, prevImage));
                    }

                    // delete previous featured image if exists
                    if (!string.IsNullOrEmpty(prevImage) && System.IO.File.Exists(imageFeaturedSavePath))
                    {
                        System.IO.File.Delete(Path.Combine(Server.MapPath(Url.Content("~/Content/")), SiteConfigService.PostFeaturedPath, prevImage));
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

            // something wrong with the data values
            return View(comment);
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

            var usList = new List<UserInRoleViewModel>();

            foreach (var user in Repository.UserProfiles)
            {
                usList.Add(new UserInRoleViewModel
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    UserEmail = user.Email,
                    UserRole = Roles.GetRolesForUser(user.UserName).FirstOrDefault()
                });
            }

            return View(usList);
        }

        public ActionResult EditUser(int userId)
        {
            ViewBag.CurrentEdit = "Users";

            var user = new UserInRoleViewModel();
            var dbRow = Repository.UserProfiles.FirstOrDefault(u => u.UserId == userId);

            if (dbRow != null)
            {
                user.UserId = userId;
                user.UserName = dbRow.UserName;
                user.UserEmail = dbRow.Email;
                user.UserRole = Roles.GetRolesForUser(dbRow.UserName).FirstOrDefault();

                var roles = new List<string>();

                foreach (var role in Roles.GetAllRoles())
                {
                    roles.Add(role);
                }

                user.AvailableRoles = roles;
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(UserInRoleViewModel user)
        {
            ViewBag.CurrentEdit = "Users";

            if (Roles.GetRolesForUser(user.UserName).Any())
            {
                Roles.RemoveUserFromRoles(user.UserName, Roles.GetRolesForUser(user.UserName));
            }

            if (user.UserRole != "None")
            {

                Roles.AddUserToRole(user.UserName, user.UserRole);
            }

            return RedirectToAction("ManageUsers");
        }

        #endregion

        #region roles
        public ActionResult ManageRoles()
        {
            ViewBag.CurrentEdit = "Roles";

            var roles = new List<RoleViewModel>();

            foreach (string s in Roles.GetAllRoles())
            {
                roles.Add(new RoleViewModel { RoleName = s });
            }

            return View(roles);
        }

        public ActionResult DeleteRole(string roleName)
        {
            if (Roles.RoleExists(roleName))
            {
                Roles.RemoveUsersFromRole(Roles.GetUsersInRole(roleName), roleName);
                Roles.DeleteRole(roleName);
                TempData["message"] = string.Format("Role \"{0}\" has been deleted", roleName);
            }

            return RedirectToAction("ManageRoles");
        }

        public ActionResult CreateRole(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                if (!Roles.RoleExists(roleName))
                {
                    Roles.CreateRole(roleName);
                    TempData["message"] = string.Format("Role \"{0}\" has been added", roleName);
                }
                else if (Roles.RoleExists(roleName))
                {
                    TempData["message"] = string.Format("Role \"{0}\" already exists", roleName);
                }
            }
            return RedirectToAction("ManageRoles");
        }
        #endregion

        #region SiteParameters

        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public ActionResult SiteConfig()
        {
            ViewBag.CurrentEdit = "Site";

            var configViewModel =
                (SiteConfigViewModel)ModelMapper.Map(SiteConfigService, typeof(ConfigService), typeof(SiteConfigViewModel));

            return View(configViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public ActionResult SiteConfig(SiteConfigViewModel viewModel)
        {
            ViewBag.CurrentEdit = "Site";
            if (ModelState.IsValid)
            {
                SiteConfigService =
                    (ConfigService)ModelMapper.Map(viewModel, typeof(SiteConfigViewModel), typeof(ConfigService));

                TempData["message"] = "Your changes have been saved";
                return RedirectToAction("SiteConfig");
            }

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public ActionResult ResetToDefaults()
        {
            var defaultModel = new SiteConfigViewModel
            {
                PostsPerPage = DefaultConfig.DefaultPostsPerPage,
                AllowedImageTypes = DefaultConfig.DefaultAllowedTypes,
                MaxImageSize = DefaultConfig.DefaultMaxSize,
                PostThumbImageHeight = DefaultConfig.DefaultThumbHeight,
                PostThumbImageWidth = DefaultConfig.DefaultThumbWidth,
                PostThumbPath = DefaultConfig.DefaultThumbPath,
                PostFeaturedImageHeight = DefaultConfig.DefaultFeaturedHeight,
                PostFeaturedImageWidth = DefaultConfig.DefaultFeaturedWidth,
                PostFeaturedPath = DefaultConfig.DefaultFeaturedPath,
                AvatarImageHeight = DefaultConfig.DefaultAvatarHeight,
                AvatarImageWidth = DefaultConfig.DefaultAvatarWidth,
                AvatarImagePath = DefaultConfig.DefaultAvatarPath
            };

            SiteConfigService =
                    (ConfigService)ModelMapper.Map(defaultModel, typeof(SiteConfigViewModel), typeof(ConfigService));

            TempData["message"] = "Defaults successfully applied";

            return RedirectToAction("SiteConfig");
        }
        #endregion

        #region helpers

        private void DeletePostImages(Post post)
        {
            if (post.ImageName != null)
            {
                string currentThumbPath = Path.Combine(Server.MapPath(Url.Content("~/Content/")), SiteConfigService.PostThumbPath, post.ImageName);
                string currentFeaturedPath = Path.Combine(Server.MapPath(Url.Content("~/Content/")), SiteConfigService.PostFeaturedPath, post.ImageName);

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
