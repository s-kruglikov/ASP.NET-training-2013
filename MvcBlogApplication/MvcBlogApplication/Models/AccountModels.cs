using System.ComponentModel.DataAnnotations;

namespace MvcBlog.WebUI.Models
{
    public class LocalPasswordModel
    {
        [Required(ErrorMessage = "Please enter your current password")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Please enter your new password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your user name")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Please enter your user name")]
        [Display(Name = "User name")]
        [StringLength(200)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ForgotPasswordModel
    {
        [Required(ErrorMessage="Please enter your user name")]
        [Display(Name="User Name")]
        public string UserName { get; set; }
    }
}
