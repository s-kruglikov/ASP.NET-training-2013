using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcBlog.WebUI.Tools;
using WebMatrix.WebData;
using MvcBlog.WebUI.Filters;
using MvcBlog.WebUI.Models;

namespace MvcBlog.WebUI.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("List", "Posts", 1);
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new { Email = model.Email });
                    WebSecurity.Login(model.UserName, model.Password);

                    return RedirectToAction("UpdateUserInfo");
                    //return RedirectToAction("List", "Posts", 1);
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public ViewResult UpdateUserInfo()
        {
            var usersContext = new UsersContext();
            var currentUserInfo =
                usersContext.UserProfiles.FirstOrDefault(user => user.UserName == User.Identity.Name);

            return View(currentUserInfo);

            //return RedirectToAction("List", "Posts", 1);
        }

        [HttpPost]
        public ActionResult UpdateUserInfo(UserProfile model, HttpPostedFileBase avatarImage)
        {
            var usersContext = new UsersContext();
            var currentUserInfo =
                usersContext.UserProfiles.FirstOrDefault(user => user.UserName == model.UserName);

            currentUserInfo.FirstName = model.FirstName;
            currentUserInfo.LastName = model.LastName;
            currentUserInfo.Email = model.Email;
            currentUserInfo.BirthDate = model.BirthDate;
            currentUserInfo.Avatar = model.Avatar;
            currentUserInfo.AvatarMimeType = model.AvatarMimeType;

            if (avatarImage != null)
            {
                string imageExtension = Path.GetExtension(avatarImage.FileName);
                string imageName = string.Format("{0}_{1}.{2}", currentUserInfo.UserId, DateTime.Now.Ticks, imageExtension);
                string imageAvatarSavePath = Path.Combine(Server.MapPath(Url.Content("~/Content/UsersAvatars/")), imageName);

                //save image parameters into DB
                currentUserInfo.AvatarMimeType = avatarImage.ContentType;
                currentUserInfo.Avatar = imageName;

                Image.FromStream(avatarImage.InputStream).ResizeProportional(new Size(100, 100)).SaveToFolder(imageAvatarSavePath);
            }


            usersContext.SaveChanges();

            return RedirectToAction("List", "Posts", 1);
        }


        //
        // GET: /Account/ChangePassword

        public ActionResult ChangePassword(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : "";

            ViewBag.ReturnUrl = Url.Action("ChangePassword");
            return View();
        }


        //
        // POST: /Account/ChangePassword

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(LocalPasswordModel model)
        {
            ViewBag.ReturnUrl = Url.Action("ChangePassword");

            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePassword", new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPassword

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        //POST: /Account/ForgotPassword

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string userName)
        {
            // check if user exists
            var user = Membership.GetUser(userName);
            if (user == null)
            {
                TempData["Message"] = "User Not Exist.";
            }
            else
            {
                // generate reset password token
                var token = WebSecurity.GeneratePasswordResetToken(userName);

                // create url with reset token
                var resetLink = "<a href='" + Url.Action("ResetPassword", "Account", new {username = userName, resettoken = token}, "http") + "'>Reset Password</a>";

                // retrieve user's email
                var db = new UsersContext();
                var email = db.UserProfiles.Where(x => x.UserName == userName).Select(x => x.Email).FirstOrDefault();

                if (email == null) throw new ArgumentNullException("email");
                else
                {
                    // Format email
                    string subject = "Password Reset for MVC Blog";
                    string body = "<b>Please find the Password Reset Token</b><br>" + resetLink;
                    try
                    {
                        SendEMail(email, subject, body);
                        TempData["Message"] = "Mail Sent.";
                    }
                    catch (Exception ex)
                    {
                        TempData["Message"] = "Error occured while sending email." + ex.Message;
                    }

                    TempData["Message"] = "Password reset link has been sent to your email: " + email;
                }
            }
            return View();
        }

        //
        //GET: /Account/ResetPassword

        [AllowAnonymous]
        public ActionResult ResetPassword(string username, string resettoken)
        {
            var db = new UsersContext();

            //search for user with defined name
            var userProfile = db.UserProfiles.FirstOrDefault(x => x.UserName.Equals(username));
            if (userProfile == null)
            {
                return RedirectToAction("BadLink");
            }

            // search for registration info by user id and password token
            var membership = db.webpages_Memberships.FirstOrDefault(x => x.UserId == userProfile.UserId && x.PasswordVerificationToken == resettoken);
            if (membership == null)
            {
                return RedirectToAction("BadLink");
            }

            // generate new password
            string newpassword = GenerateRandomPassword(6);

            // set new password
            if (!WebSecurity.ResetPassword(resettoken, newpassword))
            {
                return RedirectToAction("BadLink");
            }

            // send email with new password
            string subject = "New password for MVC blog";
            string body = string.Format("Dear {0},<br/>Please find new password for MVC blog account: <b>{1}</b><br/>", userProfile.UserName, newpassword);
            try
            {
                SendEMail(userProfile.Email, subject, body);
                ViewBag.Message = "A letter with new password sent to your Email";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error occured while sending email message. " +
                                                          ex.Message;
            }

            return View();
        }


        public FilePathResult GetUserAvatar(int userId)
        {
            var db = new UsersContext();

            UserProfile userInfo = db.UserProfiles.FirstOrDefault(user => user.UserId == userId);
            if (userInfo != null)
            {
                return File(Path.Combine(Server.MapPath(Url.Content("~/Content/UsersAvatars/")), userInfo.Avatar), userInfo.AvatarMimeType);
            }
            else
            {
                return null;
            }
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("List", "Posts");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        private string GenerateRandomPassword(int length)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyz" +
                                         "ABCDEFGHJKLMNOPQRSTUVWXYZ" +
                                         "0123456789!@$?_-*&#+";
            char[] chars = new char[length];
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            return new string(chars);
        }

        private void SendEMail(string emailid, string subject, string body)
        {
            SmtpClient client = new SmtpClient();
                    
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("stas.kruglikov@gmail.com");
            msg.To.Add(new MailAddress(emailid));

            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;

            client.Send(msg);
        }
        #endregion

        public ActionResult BadLink()
        {
            return View();
        }

        
    }
}
