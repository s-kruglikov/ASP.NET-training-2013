using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcBlog.Domain.Entities
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        [HiddenInput]
        [Display(Name = "Post ID")]
        public int PostID { get; set; }

        [Display(Name = "Post Title")]
        [Required(ErrorMessage = "Please enter post title")]
        public string PostTitle { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Post Description")]
        [Required(ErrorMessage = "Please enter post description")]
        public string PostDescription { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Post Content")]
        [Required(ErrorMessage = "Please enter post content")]
        public string PostContent { get; set; }

        [Display(Name = "Post Category")]
        [Required(ErrorMessage = "Please enter post category")]
        public string PostCategory { get; set; }

        [HiddenInput]
        [Display(Name = "Post Created By")]
        public string PostCreatedBy { get; set; }

        [HiddenInput]
        [Display(Name = "Post Creation Date")]
        public DateTime? PostCreationDate { get; set; }

        [HiddenInput]
        [Display(Name = "Post Last Modified By")]
        public string PostLastModifiedBy { get; set; }

        [HiddenInput]
        [Display(Name = "Post Last Modification Date")]
        public DateTime? PostLastModificationDate { get; set; }

        [Display(Name = "Post Visible?")]
        public bool PostIsVisible { get; set; }

        [Display(Name = "Comments Enabled?")]
        public bool PostCommentsEnabled { get; set; }

        [Display(Name = "Post Featured?")]
        public bool PostFeatured { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        public string ImageName { get; set; }
    }
}
