using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MvcBlog.Domain.Entities
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [HiddenInput]
        [Display(Name = "User Name")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "EMail")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:dd-MMMM-yyyy}")]
        [Localizable(false)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Avatar")]
        public string Avatar { get; set; }

        public string AvatarMimeType { get; set; }
    }
}
